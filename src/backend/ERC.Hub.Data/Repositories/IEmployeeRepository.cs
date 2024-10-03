using ERC.Hub.Data.Models;

namespace ERC.Hub.Data.Repositories
{
    public interface IEmployeeRepository : IRepository<long, Employee>
    {
        Task<Employee> GetByEmailAsync(string email);
    }
}
