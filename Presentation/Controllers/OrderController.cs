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
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager) : ApiBaseController
    {
        
        [HttpPost]
        public async Task<ActionResult<OrderToReturn>> CreateOrderAsync(OrderDto orderDto)
        {
            var Order = await _serviceManager.OrderServicecs.CreateOrder(orderDto, GetEmailFromToken());
            return Ok(Order);
        }

        //Get Delivery Methods
        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodsDtos>>> GetAllDeliveryMethods()
        {
            var DeliveryMethods=await _serviceManager.OrderServicecs.GetDeliveryMethodsAsync();
            return Ok(DeliveryMethods);
        }

        //Get All Order By Email
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturn>>> GetAllOrders()
        {
            var Orders = await _serviceManager.OrderServicecs.GetAllOrdesrsAsync(GetEmailFromToken());
            return Ok(Orders);
        }
        //Get Order By Id


        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<OrderToReturn>> GetAllOrderById(Guid Id)
        {
            var Order = await _serviceManager.OrderServicecs.GetOrderByIdAsync(Id);
            return Ok(Order);
        }
    }
}
