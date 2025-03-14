using Microsoft.AspNetCore.Mvc;

namespace Yurukcu.Web.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
