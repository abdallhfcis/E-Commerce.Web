﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.BasketModule
{
    public class CustomerBasket
    {
        public string Id { get; set; }//GUID:Created from client
        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
