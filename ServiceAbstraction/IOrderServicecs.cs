using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.OrderDtos;

namespace ServiceAbstraction
{
    public interface IOrderServicecs
    {
        Task<OrderToReturn> CreateOrder(OrderDto orderDto, string Email);

        Task<IEnumerable<DeliveryMethodsDtos>> GetDeliveryMethodsAsync();
        Task<IEnumerable<OrderToReturn>> GetAllOrdesrsAsync(string Email);
        Task<OrderToReturn> GetOrderByIdAsync(Guid Id);
    }
}
