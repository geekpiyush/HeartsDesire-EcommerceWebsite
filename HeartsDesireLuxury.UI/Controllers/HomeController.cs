using HeartsDesireLuxury.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System.Diagnostics;

namespace HeartsDesireLuxury.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IProductGetterServices _productServices;

        public HomeController( IProductGetterServices productService)
        {
            _productServices = productService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            List<ProductResponse> products = await _productServices.GetTopProducts(4);

            return View(products);
        }
       

    }
}
