namespace CGS.Infra.Provider.Interfaces
{
    public interface ICacheProvider<TEntity>
    {
        Task DeleteAsync(int db, string key);

        Task<TEntity> GetAsync(int db, string key);

        Task SetAsync(int db, string key, TEntity entity);
    }
}
