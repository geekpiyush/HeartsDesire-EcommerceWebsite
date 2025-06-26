using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsDesireLuxury.Core.DTO
{
    public class InventoryAddRequest
    {
        [Required(ErrorMessage = "Barcode number is required")]
        public int? BarcodeNumber { get; set; }

        [Required(ErrorMessage ="Product name is required")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage ="Stock is required")]
        public int? Stock {  get; set; }

        [Required(ErrorMessage ="Price is required")]
        public double? Price { get; set; }


        public Inventory ToInventory()
        {
            return new Inventory() { BarcodeNumber = BarcodeNumber, ProductName = ProductName, Stock = Stock, Price = Price };
        }
    }
}
