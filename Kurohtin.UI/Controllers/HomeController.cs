using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppLB1.Controllers
{
    public class HomeController : Controller
    {
        private List<ListDemo> _listData = new List<ListDemo>()
        {
            new ListDemo() {Id= 1, Name= "Item1"},
            new ListDemo() {Id= 2, Name= "Item2"},
            new ListDemo() {Id= 3, Name= "Item3"},
        };
        public IActionResult Index()
        {
            ViewData["text"] = "Лабораторная работа №6";
            ViewData["List"] = new SelectList(_listData, "Id", "Name");

            return View();
        }
        public class ListDemo
        {
            public int Id { get; set; }
            public string Name { get; set; }

        }
    }
}


