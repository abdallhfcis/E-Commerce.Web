using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.OrderDtos;

namespace Presentation.Controllers
{
    public class OrderController(IServiceManager _serviceManager):ApiBaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturn>> CreateOrderAsync (OrderDto orderDto)
        {
            var Order = await _serviceManager.OrderServicecs.CreateOrder(orderDto, GetEmailFromToken());
            return Ok(Order);
        }
    }
}
