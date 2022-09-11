using CGS.Infra.Provider.Interfaces;
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

        public async Task DeleteAsync(int db, string key)
        {
            var _redisDB = _redisDBConnection.GetDatabase(db);
            await _redisDB.KeyDeleteAsync(key);
        }

        public async Task<TEntity> GetAsync(int db, string key)
        {
            var _redisDB = _redisDBConnection.GetDatabase(db);
            var result = await _redisDB.StringGetAsync(key);


            return JsonConvert.DeserializeObject<TEntity>(result);
        }

        public async Task SetAsync(int db, string key, TEntity entity)
        {
            var _redisDB = _redisDBConnection.GetDatabase(db);
            await _redisDB.StringSetAsync(key, JsonConvert.SerializeObject(entity));
        }

    }
}
