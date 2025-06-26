using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsDesireLuxury.Core.DTO
{
    public class InventoryResponse
    {
        public int? BarcodeNumber { get; set; }
        public string? ProductName { get; set; }
        public int? Stock { get; set; }
        public double? Price { get; set; }
    }

    public static class InventoryExtensions
    {
        public static InventoryResponse ToInventoryResponse(this Inventory inventory)
        {
            return new InventoryResponse { BarcodeNumber = inventory.BarcodeNumber, ProductName = inventory.ProductName, Stock = inventory.Stock, Price = inventory.Price };
        }
    }
}