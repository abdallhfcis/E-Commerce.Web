using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Identity;

namespace Shared.DataTransferObjects.OrderDtos
{
    public class OrderToReturn
    {
        public Guid Id { get; set; }
        public string buyerEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public string Status { get; set; }
        public AddressDto shipToAddress { get; set; } = default!;
        public decimal deliveryCost { get; set; }
        public string DeliveryMethod { get; set; } = default!;
        
        public ICollection<OrderItemDto> Items { get; set; } = [];

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }
    }
}
