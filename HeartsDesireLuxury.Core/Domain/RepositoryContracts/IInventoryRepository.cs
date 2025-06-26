using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsDesireLuxury.Core.Domain.RepositoryContracts
{
    public interface IInventoryRepository
    {
        public Task<Inventory> AddInventory(Inventory inventory);

        //public Task<List<Inventory>> GetInventory(Inventory inventory);
    }
}
