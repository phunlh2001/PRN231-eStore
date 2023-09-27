using BusinessObjects.Objects;
using Client_eStore.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    [Route("user")]
    public class UsersController : Controller
    {
        private readonly HttpClient client;
        private string api;

        public UsersController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            api = "https://localhost:44345/api/user/";
        }

        public async Task<IActionResult> Index()
        {
            var isLogin = HttpContext.Session.GetString("userId") != null;
            var isAdmin = HttpContext.Session.GetString("userEmail") == "admin@estore.com";

            if (!isLogin && !isAdmin) return Redirect("/");

            var list = await client.GetApi<IEnumerable<Member>>(api + "all");
            return View(list);
        }
    }
}
