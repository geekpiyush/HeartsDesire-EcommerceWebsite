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
    [AllowAnonymous]
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
                if (int.TryParse(userId, out int customerId))
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

        [Authorize] // Ensure only logged-in users can view order history
        public async Task<IActionResult> OrderHistory()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account"); // or return Unauthorized
            }

            string? userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userId, out int customerId))
            {
                return Unauthorized(); // or show error
            }

            var userOrders = await _orderGetService.GetOrdersByCustomerID(customerId);
            return View(userOrders); // You will create this view next
        }

    }
}
