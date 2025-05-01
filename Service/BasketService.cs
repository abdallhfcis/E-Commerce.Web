using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDtos;

namespace Services
{
    public class BasketService(IBasketRepository _basketRepository,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateorUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket=_mapper.Map<BasketDto,CustomerBasket>(basket);
            var CreateOrUpdateBasket = _basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);
            if (CreateOrUpdateBasket != null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Can Not Update Or Create Basket Now ,Try Again Later");
        }

        public async Task<bool> DeleteBasketAsync(string key) => await _basketRepository.DeleteBasketAsync(key);
        public async Task<BasketDto> GetBasketAsync(string key)
        {
            var Basket=await _basketRepository.GetBasketAsync(key);
            if (Basket != null)
                return _mapper.Map<CustomerBasket,BasketDto>(Basket);
            else
                throw new BasketNotFoundException(key);

        }
    }
}
