using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    [Route("report")]
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            var isLogin = HttpContext.Session.GetString("userId") != null;
            var isAdmin = HttpContext.Session.GetString("userEmail") == "admin@estore.com";

            if (!isLogin && !isAdmin) return Redirect("/");

            return View();
        }
    }
}
