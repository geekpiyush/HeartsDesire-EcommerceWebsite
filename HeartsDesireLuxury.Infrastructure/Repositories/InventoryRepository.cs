using Entities.DB;
using Entities;
using HeartsDesireLuxury.Core.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsDesireLuxury.Infrastructure.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _db;
        public InventoryRepository(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }
        public async Task<Inventory> AddInventory(Inventory inventory)
        {
           _db.Add(inventory);
           await _db.SaveChangesAsync();
            return inventory;
        }

        //public Task<List<Inventory>> GetInventory(Inventory inventory)
        //{

        //}
    }
}
