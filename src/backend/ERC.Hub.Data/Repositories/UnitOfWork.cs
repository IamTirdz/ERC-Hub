using ERC.Hub.Data.Context;

namespace ERC.Hub.Data.Repositories
{
    public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        public void Dispose() => dbContext.Dispose();

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await dbContext.SaveChangesAsync(cancellationToken);
    }
}
