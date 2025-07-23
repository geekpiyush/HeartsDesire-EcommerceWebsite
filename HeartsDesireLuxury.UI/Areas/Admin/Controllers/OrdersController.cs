using Entities;
using HeartsDesireLuxury.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeartsDesireLuxury.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderGetService _orderService;
        public OrdersController(IOrderGetService orderGetService)
        {
            _orderService = orderGetService;
        }

        public async Task<IActionResult> OrdersAsync()
        {
            var orders = await _orderService.GetAllOrders();
            return View(orders);
        }
    }
}
