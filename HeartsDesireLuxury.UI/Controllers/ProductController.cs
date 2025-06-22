using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using System.Threading.Tasks;

namespace HeartsDesirePerfume.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private readonly IProductGetterServices _productServices;

        public ProductController(IProductGetterServices productService)
        {
            _productServices = productService;
        }
        public async Task<IActionResult> AllProducts()
        {
           List<ProductResponse> products = await _productServices.GetAllProducts();
            return View(products);
        }
        public async Task<IActionResult> Women()
        {
            List<ProductResponse> products = await _productServices.GetProductsByCategoryID(2);
            return View(products);
        }
        public async Task<IActionResult> Men()
        {
            List<ProductResponse> products = await _productServices.GetProductsByCategoryID(1);
            return View(products);
        }
        public async Task<IActionResult> Luxury()
        {
            List<ProductResponse> products = await _productServices.GetProductsByCategoryID(4);
            return View(products);
        }
        public async Task<IActionResult> Skincare()
        {
            List<ProductResponse> products = await _productServices.GetProductsByCategoryID(4);
            return View(products);
        }

        //fetch product accoridng to there ID
        public async Task<IActionResult> ProductByProductID(int productID)
        {
           var products = await _productServices.GetProductByProductID(productID);   

            return View(products);
        }

    }
}
