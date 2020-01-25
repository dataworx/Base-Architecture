using BaseArchitecture.Domain.Data;
using BaseArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaseArchitecture.Infrastructure.Data.EntityFramework
{
    public class DataContext : IdentityDbContext<AppUser>, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}