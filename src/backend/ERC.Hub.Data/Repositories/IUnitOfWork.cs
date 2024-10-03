namespace ERC.Hub.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IEmployeeRepository Employee { get; }
    }
}
