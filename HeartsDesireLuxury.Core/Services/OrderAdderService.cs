using Entities;
using HeartsDesireLuxury.Core.Domain.RepositoryContracts;
using HeartsDesireLuxury.Core.DTO;
using HeartsDesireLuxury.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsDesireLuxury.Core.Services
{
    public class OrderAdderService : IOrderGetService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderAdderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task AddOrder(OrderRequest orderRequest)
        {
            var order = new Orders
            {
                ProductID = orderRequest.ProductID,
                CustomerID = orderRequest.CustomerID,
                Quantity = orderRequest.Quantity,
                CustomerName = orderRequest.CustomerName,
                Email = orderRequest.Email,
                MobileNumber =  orderRequest.MobileNumber,
                Address = orderRequest.Address,
                State =     orderRequest.State,
                City = orderRequest.City,
                Pincode = orderRequest.Pincode,
                OrderDate = DateTime.Now
            };

            await _orderRepository.InsertOrder(order);
        }

        public async Task<List<Orders>> GetOrdersByCustomerID(int customerID)
        {
            return await _orderRepository.GetOrdersByCustomerId(customerID);
        }
    }
}
