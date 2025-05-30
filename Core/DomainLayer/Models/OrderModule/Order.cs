﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    public class Order:BaseEntity<Guid>
    {
        public Order() 
        { 
        
        }
        public Order(string userEmail, OrderAddress address, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            buyerEmail = userEmail;
            shipToAddress = address;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string buyerEmail { get; set; } = default!;
        public OrderAddress shipToAddress { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal { get; set; }
        public OrderStatus Status { get; set; }
        public int DeliveryMethodId { get; set; } 

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public decimal GetTotal => SubTotal + DeliveryMethod.Price;


    }
}
