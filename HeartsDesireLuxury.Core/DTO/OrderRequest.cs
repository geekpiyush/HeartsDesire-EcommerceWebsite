using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsDesireLuxury.Core.DTO
{
    public class OrderRequest
    {
        public int ProductID {  get; set; }
        public int CustomerID { get; set; }
        public int Quantity { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
    }
}
