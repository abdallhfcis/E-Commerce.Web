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
    }
}
