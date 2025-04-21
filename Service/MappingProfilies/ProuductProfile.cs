using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models;
using Shared.DataTransferObjects;

namespace Services.MappingProfilies
{
    public class ProuductProfile:Profile
    {
        public ProuductProfile()
        {
            CreateMap<Product, ProductDto>()
                    .ForMember(dest => dest.BrandName, Option => Option.MapFrom(Src => Src.ProductBrand.Name))
                    .ForMember(dest => dest.TypeName, Option => Option.MapFrom(Src => Src.ProductType.Name));

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType,TypeDto>();
        }
    }
}
