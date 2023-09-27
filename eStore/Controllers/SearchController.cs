using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
