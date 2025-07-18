using Entities;
using Entities.DB;
using HeartsDesireLuxury.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsDesireLuxury.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }

        //public Task<List<Orders>> GetOrdersByCustomerId(Guid customerID)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<List<Orders>> GetOrdersByCustomerId(Guid customerID)
        {
            return await _db.Orders
                .Include(o => o.Product)
                .Where(o => o.CustomerID == customerID)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task InsertOrder(Orders orders)
        {
            _db.Orders.Add(orders);
            await _db.SaveChangesAsync();
        }
    }
}
