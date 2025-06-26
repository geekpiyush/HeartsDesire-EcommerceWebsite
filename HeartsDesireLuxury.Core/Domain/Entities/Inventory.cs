using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Inventory
    {
        [Key]
        public int? BarcodeNumber {  get; set; }
        public string? ProductName {  get; set; }
        public int? Stock {  get; set; }
        public double? Price { get; set; }
    }
}
