using Entities;
using HeartsDesireLuxury.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsDesireLuxury.Core.ServiceContracts
{
    public interface IOrderGetService
    {
        Task AddOrder(OrderRequest orderRequest);
        Task<List<Orders>> GetOrdersByCustomerID(Guid customerID);

        Task<List<Orders>> GetAllOrders();
    }
}
