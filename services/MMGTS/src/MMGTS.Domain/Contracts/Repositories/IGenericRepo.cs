using System.Linq.Expressions;

namespace MMGTS.Domain.Contracts.Repositories
{
    public interface IGenericRepo<TEntity>
    {
        Task<TEntity> Add<TEntityCreationDTO>(TEntityCreationDTO entityCreation);
        Task Update<TEntityDTO>(TEntityDTO entityCreation);
        Task UpdatePatch<TEntityDTO>(TEntityDTO entityCreation, Expression<Func<TEntity, dynamic>> predicate);
        Task<IEnumerable<TEntityDTO>> List<TEntityDTO>();
        Task<IEnumerable<TEntityDTO>> OrderedList<TEntityDTO>(Expression<Func<TEntity, dynamic>> orderByPredicate);
        Task<TEntityDTO> FilterById<TEntityDTO>(params object[] ids);
        Task<IEnumerable<TEntityDTO>> Filter<TEntityDTO>(Expression<Func<TEntity, bool>> predicate);
        Task Delete<TEntityDTO>(TEntityDTO entity);
        Task<IEnumerable<dynamic>> ListField(Func<TEntity, dynamic> predicate, Func<TEntity, bool> predicateCondition = null);
    }
}
