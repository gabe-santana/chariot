using CGS.Utils.Enums;

namespace CGS.Infra.Provider.Interfaces
{
    public interface ICacheProvider<TEntity>
    {
        Task DeleteAsync(RedisDBEnum db, string key);

        Task<TEntity> GetAsync(RedisDBEnum db, string key);

        Task SetAsync(RedisDBEnum db, string key, TEntity entity);
    }
}
