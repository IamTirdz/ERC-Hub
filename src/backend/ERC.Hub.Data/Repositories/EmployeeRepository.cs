using ERC.Hub.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ERC.Hub.Data.Repositories
{
    public class EmployeeRepository(DbContext dbContext) : RepositoryBase<long, Employee>(dbContext), IEmployeeRepository
    {
        public override async Task<IEnumerable<Employee>> GetAllAsync()
            => await Context
                .Include(d => d.Department)
                .Include(p => p.Position)
                .ToListAsync();

        public override async Task<Employee> GetByIdAsync(long id)
            => await Context
                .Include(d => d.Department)
                .Include(p => p.Position)
                .FirstOrDefaultAsync(e => e.Id == id)
            ?? default!;

        public async Task<Employee> GetByEmailAsync(string email)
            => await Context
                .Include(d => d.Department)
                .Include(p => p.Position)
                .FirstOrDefaultAsync(e => e.Email == email) ?? default!;
    }
}
