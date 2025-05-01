using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.BasketModule;
using Shared.DataTransferObjects.BasketModuleDtos;

namespace Services.MappingProfilies
{
    class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket,BasketDto>().ReverseMap();
            CreateMap<BasketItem,BasketItemsDto>().ReverseMap();
        }
    }
}
