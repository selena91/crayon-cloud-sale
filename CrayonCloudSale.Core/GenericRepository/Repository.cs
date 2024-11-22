using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace CrayonCloudSale.Infrastructure.GenericRepository;

public class Repository<TEntity, TDbContext> : IRepository<TEntity>
    where TEntity : class
    where TDbContext : DbContext
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly DbContext _dbContext;

    public Repository(TDbContext context)
    {
        _dbSet = context.Set<TEntity>();
        _dbContext = context;
    }

    public IQueryable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet.AsQueryable();

        query = filter == null ?
            query : query.Where(filter);

        includeProperties.ToList().ForEach(p => query = query.Include(p));

        return orderBy != null ?
            orderBy(query) : query;
    }

    public async Task<ICollection<TEntity>> GetAsync(
       Expression<Func<TEntity, bool>> filter = null,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
       params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return await Get(filter, orderBy, includeProperties).ToListAsync();
    }

    public async Task<ICollection<TEntity>> GetAsyncWithoutTracking(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return await Get(filter, orderBy, includeProperties).AsNoTracking().ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }    

    public virtual async void Update(TEntity entity)
    {
        if (entity == null)
        {
            return;
        }

        if (_dbContext.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        _dbContext.Entry(entity).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }
}
