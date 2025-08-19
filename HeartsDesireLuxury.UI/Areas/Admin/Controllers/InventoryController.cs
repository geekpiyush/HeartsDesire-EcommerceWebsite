using HeartsDesireLuxury.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace HeartsDesireLuxury.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class InventoryController : Controller
    {
        private readonly IInventoryAdderService _inventoryAdderService; 

        public InventoryController(IInventoryAdderService inventoryAdderService) 
        {
            _inventoryAdderService = inventoryAdderService;
        }

        [HttpGet]
        public IActionResult Inventory()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddInventory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddInventory(InventoryAddRequest inventoryAddRequest)
        {
            return View(inventoryAddRequest);
        }

    }
}
