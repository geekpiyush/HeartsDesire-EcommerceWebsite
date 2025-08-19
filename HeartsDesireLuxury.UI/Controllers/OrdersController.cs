using HeartsDesireLuxury.Core.DTO;
using HeartsDesireLuxury.Core.ServiceContracts;
using HeartsDesireLuxury.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System.Diagnostics;

namespace HeartsDesireLuxury.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IProductGetterServices _productServices;
        private readonly IOrderGetService _orderGetService;

        public OrdersController(IProductGetterServices productService, IOrderGetService orderGetService)
        {
            _productServices = productService;
            _orderGetService = orderGetService;
        }

        public async Task<IActionResult> PlaceOrders(int productID, int quantity = 1)
        {
            var product = await _productServices.GetProductByProductID(productID);
            ViewBag.Quantity = quantity;
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrders(OrderRequest orderRequest)
        {
            if (!ModelState.IsValid)
            {
                var product = await _productServices.GetProductByProductID(orderRequest.ProductID);
                ViewBag.Quantity = orderRequest.Quantity;
                return View("Orders", product);
            }

            if (User.Identity.IsAuthenticated)
            {
                string? userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (Guid.TryParse(userId, out Guid customerId))
                {
                    orderRequest.CustomerID = customerId;
                }
            }

            await _orderGetService.AddOrder(orderRequest);
            return RedirectToAction("OrderSuccess");
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }

        [Authorize] 
        public async Task<IActionResult> OrderHistory()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account"); 
            }

            string? userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userId, out Guid customerId))
            {
                return Unauthorized(); 
            }

            var userOrders = await _orderGetService.GetOrdersByCustomerID(customerId);
            return View(userOrders); 
        }

    }
}
