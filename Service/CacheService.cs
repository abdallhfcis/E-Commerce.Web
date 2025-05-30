using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using ServiceAbstraction;

namespace Services
{
    class CacheService (ICacheRepository _cacheRepository): ICacheService
    {
        public async Task<string?> GetAsync(string CacheKey) => await _cacheRepository.GetAsync(CacheKey);

        public async Task SetAsync(string CacheKey, object cacheValue, TimeSpan TimeToLive)
        {
            var value=JsonSerializer.Serialize(cacheValue);

            await _cacheRepository.SetAsync(CacheKey, value, TimeToLive);
        }
    }
}
