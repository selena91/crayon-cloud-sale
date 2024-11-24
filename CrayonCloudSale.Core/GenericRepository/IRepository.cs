using System.Linq.Expressions;

namespace CrayonCloudSale.Infrastructure.GenericRepository;

public interface IRepository<TEntity>
where TEntity : class
{
    IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includeProperties);
    Task<ICollection<TEntity>> GetAsync(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           params Expression<Func<TEntity, object>>[] includeProperties);
    Task<ICollection<TEntity>> GetAsyncWithoutTracking(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        params Expression<Func<TEntity, object>>[] includeProperties);
    Task<TEntity?> GetByIdAsync(object id);

    void Update(TEntity item);
}

