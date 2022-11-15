using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MMGTS.Domain.Contracts.Repositories;
using MMGTS.Infra.EF.Context;
using System.Linq.Expressions;

namespace MMGTS.Server.Repositories
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity>
         where TEntity : class
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public GenericRepo(IMapper _mapper, DataContext _context)
        {
            this._mapper = _mapper;
            this._context = _context;
        }

        public async Task<TEntity> Add<TEntityCreationDTO>(TEntityCreationDTO entityCreation)
        {
            var entity = _mapper.Map<TEntity>(entityCreation);
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<TEntity>(entity);
        }

        public async Task AddRange<TEntityCreationDTO>(IEnumerable<TEntityCreationDTO> entitiesCreation)
        {
            var entities =
                _mapper.Map<IEnumerable<TEntityCreationDTO>, IEnumerable<TEntity>>(entitiesCreation);

            _context.Set<TEntity>().AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Delete<TEntityDTO>(TEntityDTO entitydto)
        {
            var entity = _mapper.Map<TEntityDTO, TEntity>(entitydto);

            var createdAccount = _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntityDTO>> Filter<TEntityDTO>(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await _context.Set<TEntity>().Where(predicate).ToListAsync();

            return _mapper.Map<IEnumerable<TEntityDTO>>(entity);
        }

        public async Task<TEntityDTO> FilterById<TEntityDTO>(params object[] ids)
        {
            var entity = await _context.Set<TEntity>().FindAsync(ids);
            return _mapper.Map<TEntityDTO>(entity);

        }
        public async Task<IEnumerable<TEntityDTO>> List<TEntityDTO>()
        {
            var accounts = await _context.Set<TEntity>().ToListAsync();
            return _mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityDTO>>(accounts);
        }

        public async Task<IEnumerable<TEntityDTO>> OrderedList<TEntityDTO>(Expression<Func<TEntity, dynamic>> orderByPredicate)
        {
            var accounts = await _context.Set<TEntity>().OrderBy(orderByPredicate).ToListAsync();
            return _mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityDTO>>(accounts);
        }


        public async Task Update<TEntityDTO>(TEntityDTO entityCreation)
        {
            var account = _mapper.Map<TEntityDTO, TEntity>(entityCreation);
            _context.Set<TEntity>().Update(account);
            await _context.SaveChangesAsync();
        }


        public async Task UpdatePatch<TEntityDTO>(TEntityDTO entityCreation, Expression<Func<TEntity, dynamic>> predicate)
        {
            var account = _mapper.Map<TEntityDTO, TEntity>(entityCreation);

            _context.Set<TEntity>().Attach(account);
            _context.Entry(account).Property(predicate).IsModified = true;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<dynamic>> ListField(Func<TEntity, dynamic> predicate, Func<TEntity, bool> predicateCondition = null)
        {
            var result = await _context.Set<TEntity>().ToListAsync();
            return predicateCondition != null
                ? result.Where(predicateCondition).Select(predicate)
                : result.Select(predicate);
        }

    }
}
