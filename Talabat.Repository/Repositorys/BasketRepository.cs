//using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entityes;
using Talabat.Core.I_Repository;

namespace Talabat.Repository.Repositorys
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _redis;

        public BasketRepository(IConnectionMultiplexer Redis)
        {
            _redis = Redis.GetDatabase();
        }
        public async Task<Baskets?> CreateOrUpdateAcync(Baskets baskets)
        {
            var Jsonbasket = JsonSerializer.Serialize(baskets);

            var CreateOrUpdate = await _redis.StringSetAsync(baskets.Id, Jsonbasket, TimeSpan.FromDays(1));

            if (!CreateOrUpdate)
                return null;
            else
                return await GetBasketAcync(baskets.Id);

        }

        public async Task<bool> DeleteBasketAcync(string id)
        {
            return await _redis.KeyDeleteAsync(id);
        }

        public async Task<Baskets?> GetBasketAcync(string IdBasket)
        {
            var basket = await _redis.StringGetAsync(IdBasket);
            if (basket.IsNull)
                return null;
            else
                return JsonSerializer.Deserialize<Baskets>(basket);
        }
    }
}
