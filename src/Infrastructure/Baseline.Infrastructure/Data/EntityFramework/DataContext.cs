using Baseline.Domain.Data;
using Baseline.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Baseline.Infrastructure.Data.EntityFramework
{
    public class DataContext : IdentityDbContext<AppUser>, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        
        public new int SaveChanges()
        {
            return base.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        public DbSet<LogEntry> LogEntries { get; set; }
    }
}