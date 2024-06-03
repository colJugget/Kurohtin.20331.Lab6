using Microsoft.AspNetCore.Mvc;

namespace Kurohtin.UI.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
