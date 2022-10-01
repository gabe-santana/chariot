using CGS.Infra.Provider.Interfaces;
using CGS.Utils.Enums;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CGS.Infra.Provider
{
    public class CacheProvider<TEntity> : ICacheProvider<TEntity>
    {
        private readonly IConnectionMultiplexer _redisDBConnection;
        public CacheProvider(IConnectionMultiplexer _connection)
        {
            this._redisDBConnection = _connection;
        }

        public async Task DeleteAsync(RedisDBEnum db, string key)
        {
            var _redisDB = _redisDBConnection.GetDatabase((int) db);
            await _redisDB.KeyDeleteAsync(key);
        }

        public async Task<TEntity> GetAsync(RedisDBEnum db, string key)
        {
            var _redisDB = _redisDBConnection.GetDatabase((int) db);
            var result = await _redisDB.StringGetAsync(key);

            if(result.HasValue)
                return JsonConvert.DeserializeObject<TEntity>(result);
            return default(TEntity);
        }

        public async Task SetAsync(RedisDBEnum db, string key, TEntity entity)
        {
            var _redisDB = _redisDBConnection.GetDatabase((int) db);
            await _redisDB.StringSetAsync(key, JsonConvert.SerializeObject(entity));
        }

    }
}
