﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstraction;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork,IMapper _mapper,IBasketRepository _basketRepository) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService=new Lazy<IProductService>(() => new ProductService(_unitOfWork,_mapper));
        private readonly Lazy<IBasketService> _LazeBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository,_mapper));
        public IProductService ProductService => _LazyProductService.Value;

        public IBasketService BasketService => _LazeBasketService.Value;
    }
}
