using BusinessObjects.DTOs;
using BusinessObjects.Objects;
using Client_eStore.Helper;
using eStore.Constant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    [Route("order")]
    public class OrdersController : Controller
    {
        private readonly HttpClient client;
        private string api;

        public OrdersController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            api = "https://localhost:44345/api/";
        }

        public async Task<IActionResult> Index()
        {
            if (!IsLogin())
            {
                return Redirect("/");
            }

            var userId = HttpContext.Session.GetString("userId");
            var list = await client.GetApi<IEnumerable<Order>>(api + $"order/all/{userId}");
            return View(list);
        }

        [HttpGet("create/{productId}", Name = "create")]
        public async Task<IActionResult> Create(int productId)
        {
            if (IsLogin())
            {
                var product = await client.GetApi<Product>(api + $"product/{productId}");
                ViewData["productName"] = product.ProductName;

                SetListFreight();
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost("create/{id}")]
        public async Task<IActionResult> Create(int id, OrderInfo info)
        {
            var product = await client.GetApi<Product>(api + $"product/{id}");
            if (ModelState.IsValid)
            {
                if (info.Quantity > product.UnitslnStock)
                {
                    ViewData["productName"] = product.ProductName;
                    SetListFreight();
                    ViewData["error"] = "Out of stock, try again!";
                    return View();
                }

                var userId = HttpContext.Session.GetString("userId");

                Order order = new Order();
                order.Freight = info.Freight;
                order.MemberId = int.Parse(userId);

                HttpResponseMessage orderRes =
                    await client.PostOrPutApi(order, api + "order/add", method: "POST");
                if (orderRes.IsSuccessStatusCode)
                {
                    var data = await orderRes.Content.ReadAsStringAsync();
                    var orderId = JsonConvert.DeserializeObject<Order>(data).OrderId;

                    decimal discount = CalculateDiscount(info.Quantity);
                    OrderDetail detail = new OrderDetail
                    {
                        OrderId = orderId,
                        ProductId = product.ProductId,
                        Quantity = info.Quantity,
                        UnitPrice = (product.UnitPrice * info.Quantity + info.Freight) * (1 - discount),
                        Discount = discount
                    };

                    HttpResponseMessage detailRes =
                        await client.PostOrPutApi(detail, api + "orderDetail/add", method: "POST");
                    if (detailRes.IsSuccessStatusCode)
                    {
                        return Redirect("/");
                    }
                }
            }
            else
            {
                ViewData["error"] = "Quantity is required";
            }
            ViewData["productName"] = product.ProductName;
            SetListFreight();
            return View();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            if (IsLogin())
            {
                var order = await client.GetApi<Order>(api + $"order/{id}");
                return View(order);
            }
            return Redirect("/");
        }

        [HttpPost("cancel")]
        public async Task Cancel(int id)
        {
            await client.DeleteAsync(api + $"order/{id}");
        }

        private bool IsLogin()
        {
            return HttpContext.Session.GetString("userId") != null;
        }

        private void SetListFreight()
        {
            var freights = new List<SelectListItem>
                {
                    new SelectListItem { Value = Freight.StandardFreight.ToString(), Text = "Standard" },
                    new SelectListItem { Value = Freight.ExpressFreight.ToString(), Text = "Express" },
                    new SelectListItem { Value = Freight.OvernightFreight.ToString(), Text = "Overnight" },
                };
            ViewData["freight"] = new SelectList(freights, "Value", "Text");
        }

        private decimal CalculateDiscount(int quantity)
        {
            if (quantity >= 10 && quantity < 20) return 0.25m;
            else if (quantity >= 20 && quantity < 50) return 0.4m;
            else if (quantity >= 50) return 0.6m;
            else return 0;
        }
    }
}
