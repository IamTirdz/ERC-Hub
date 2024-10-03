using ERC.Hub.Data.Context;

namespace ERC.Hub.Data.Repositories
{
    public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        private IEmployeeRepository? employeeRepository;

        public void Dispose() => dbContext.Dispose();

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await dbContext.SaveChangesAsync(cancellationToken);

        public IEmployeeRepository Employee => employeeRepository ??= new EmployeeRepository(dbContext);
    }
}
