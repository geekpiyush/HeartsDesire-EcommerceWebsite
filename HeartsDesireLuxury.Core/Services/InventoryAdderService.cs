using Entities;
using HeartsDesireLuxury.Core.Domain.RepositoryContracts;
using HeartsDesireLuxury.Core.DTO;
using ServiceContracts;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class InventoryAdderService : IInventoryAdderService
    {
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryAdderService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }
        public async Task<InventoryResponse> AddInventory(InventoryAddRequest inventoryAddRequest)
        {
            //check add request is null or not
            if(inventoryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(inventoryAddRequest));
            }
            //Check model validation (correct form input or not)
            ValidationHelper.ModelValidation(inventoryAddRequest);

            Inventory inventory =  inventoryAddRequest.ToInventory();

           InventoryResponse inventoryResponse = inventory.ToInventoryResponse();

           await _inventoryRepository.AddInventory(inventory);

            return inventory.ToInventoryResponse();
        }
    }
}
