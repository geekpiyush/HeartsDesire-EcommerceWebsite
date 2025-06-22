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
    public class OrdersController : Controller
    {
        private readonly IProductGetterServices _productServices;

        public OrdersController(IProductGetterServices productService)
        {
            _productServices = productService;
        }

        public IActionResult Orders(int productID)
        {
           var product =  _productServices.GetProductByProductID(productID);
            return View(product);
        }


    }
}
