using Microsoft.EntityFrameworkCore;

namespace ERC.Hub.Data.Repositories
{
    public class RepositoryBase<TId, TEntity>(DbContext dbContext) : IRepository<TId, TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> Context = dbContext.Set<TEntity>();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await Context.ToListAsync();

        public virtual async Task<TEntity> GetByIdAsync(TId id) => await Context.FindAsync(id) ?? default!;

        public virtual void Add(TEntity entity) => Context.Add(entity);

        public virtual void AddRange(IEnumerable<TEntity> entities) => Context.AddRange(entities);

        public virtual void Update(TEntity entity) => Context.Update(entity);
    }
}
