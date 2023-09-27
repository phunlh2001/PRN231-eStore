using BusinessObjects.Objects;
using Client_eStore.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    [Route("product")]
    public class ProductsController : Controller
    {
        private readonly HttpClient client;
        private string api;

        public ProductsController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            api = "https://localhost:44345/api/";
        }

        public IActionResult Index()
        {
            if (!IsAdmin())
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var p = await client.GetApi<Product>(api + $"product/{id}");
            return View(p);
        }

        [HttpGet("add", Name = "add")]
        public async Task<IActionResult> Create()
        {
            if (!IsAdmin())
            {
                return Redirect("/");
            }
            await SetCategoryList();
            return View();
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage res = await client.PostOrPutApi(product, api + "product/add", method: "POST");
                if (res.IsSuccessStatusCode)
                {
                    return Redirect("/");
                }
            }
            await SetCategoryList();
            return View(product);

        }

        [HttpGet("update/{id}", Name = "update")]
        public async Task<IActionResult> Update(int id)
        {
            if (!IsAdmin())
            {
                return Redirect("/");
            }
            var prod = await client.GetApi<Product>(api + $"product/{id}");
            await SetCategoryList();
            return View(prod);
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage res = await client.PostOrPutApi(product, api + $"product/{id}", method: "PUT");
                if (res.IsSuccessStatusCode)
                {
                    return Redirect("/");
                }
            }
            await SetCategoryList();
            return View();
        }

        [HttpGet("delete/{id}", Name = "delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin())
            {
                return Redirect("/");
            }
            var prod = await client.GetApi<Product>(api + $"product/{id}");
            return View(prod);
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            HttpResponseMessage res = await client.DeleteAsync(api + $"product/{id}");
            if (res.IsSuccessStatusCode)
            {
                return Redirect("/");
            }
            var prod = await client.GetApi<Product>(api + $"product/{id}");
            return View(prod);
        }

        private async Task SetCategoryList()
        {
            var cates = await client.GetApi<IEnumerable<Category>>(api + "category/all");
            ViewData["cate"] = new SelectList(cates, "CategoryId", "CategoryName");
        }

        private bool IsAdmin()
        {
            var isLogin = HttpContext.Session.GetString("userId") != null;
            var isAdmin = HttpContext.Session.GetString("userEmail") == "admin@estore.com";

            return isLogin && isAdmin;
        }
    }
}
