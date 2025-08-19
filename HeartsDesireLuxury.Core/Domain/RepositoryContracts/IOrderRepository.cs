using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsDesireLuxury.Core.Domain.RepositoryContracts
{
    public interface IOrderRepository
    {
        Task InsertOrder(Orders orders);
        Task<List<Orders>> GetOrdersByCustomerId(Guid customerID);

        Task<List<Orders>> GetAllOrders();

    }
}
