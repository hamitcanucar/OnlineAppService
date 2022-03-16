using Microsoft.EntityFrameworkCore;
using OnlineAppService.Domain.Entities;

namespace OnlineAppService.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<User>();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
