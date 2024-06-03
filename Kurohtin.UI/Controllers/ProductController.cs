using Kurohtin.UI.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace Kurohtin.UI.Controllers
{
    public class ProductController(IProductService categoryService,IProductService productService) : Controller
    {
        [Route("Catalog")]
        [Route("Catalog/{category}")]
        public async Task<IActionResult> Index(string? category,int pageNo = 1)
        {
            var productResponse =
            await productService.GetProductListAsync(null);
            if (!productResponse.Success)
            return NotFound(productResponse.ErrorMessage);
            return View(productResponse.Data);
        }
    }
}
