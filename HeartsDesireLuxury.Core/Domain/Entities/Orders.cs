using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public Guid CustomerID { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [ForeignKey("ProductID")]
        public Products? Product { get; set; }
    }
}
