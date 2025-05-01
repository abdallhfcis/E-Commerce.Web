﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Models.BasketModule;
using StackExchange.Redis;

namespace Presistence.Repositories
{
    public class BasketRepository (IConnectionMultiplexer connection ): IBasketRepository
    {
        private readonly IDatabase _database=connection.GetDatabase();



        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            throw new NotImplementedException();

            //var JsonBasket=JsonSerializer.Serialize<CustomerBasket>(basket);
            ////var IsCreatedOrUpdated = await _database.StringGetSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromHours(72));
            //var IsCreatedOrUpdated = await _database.StringGetSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(30));

            //if (IsCreatedOrUpdated)
            //    return await GetBasketAsync(basket.Id);
            //else 
            //    return null;

        }

        public async Task<bool> DeleteBasketAsync(string id) =>  await _database.KeyDeleteAsync(id);

        public async Task<CustomerBasket> GetBasketAsync(string key)
        {
            var Basket=  await _database.StringGetAsync(key);
            if (Basket.IsNullOrEmpty)
                return null;
            else
                return  JsonSerializer.Deserialize<CustomerBasket>(Basket);

        }
    }
}
