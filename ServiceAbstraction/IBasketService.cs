using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.BasketModuleDtos;

namespace ServiceAbstraction
{
    public interface IBasketService
    {
        public Task<BasketDto> GetBasketAsync(string key);
        Task<BasketDto> CreateorUpdateBasketAsync(BasketDto basket);
        Task<bool> DeleteBasketAsync(string key);
    }
}
