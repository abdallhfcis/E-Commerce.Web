using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.OrderModule;
using Shared.DataTransferObjects.OrderDtos;
using Shared.Identity;

namespace Services.MappingProfilies
{
    public class OrderProfile:Profile
    {
        public OrderProfile() 
        {
            CreateMap<AddressDto, OrderAddress>();
            CreateMap<Order,OrderToReturn>().ReverseMap();  
        }
    }
}
