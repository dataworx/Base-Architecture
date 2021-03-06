﻿using Baseline.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Baseline.Domain.Data
{
    public interface IDataContext
    {
        DbSet<AppUser> Users { get; set; }

        DbSet<IdentityUserClaim<string>> UserClaims { get; set; }

        DbSet<IdentityUserLogin<string>> UserLogins { get; set; }

        DbSet<IdentityUserToken<string>> UserTokens { get; set; }

        DbSet<IdentityUserRole<string>> UserRoles { get; set; }

        DbSet<IdentityRole> Roles { get; set; }

        DbSet<IdentityRoleClaim<string>> RoleClaims { get; set; }

        DbSet<LogEntry> LogEntries { get; set; }

        int SaveChanges();
        
        Task<int> SaveChangesAsync();
    }
}