using Microsoft.EntityFrameworkCore;

namespace ERC.Hub.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());

                var idProperty = entityType.FindProperty("Id");
                if (idProperty?.ClrType == typeof(Guid))
                    modelBuilder.Entity(entityType.ClrType).HasIndex("Id").IsUnique();
            }
        }
    }
}
