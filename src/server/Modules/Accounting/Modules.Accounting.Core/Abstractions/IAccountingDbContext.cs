using FluentPOS.Modules.Accounting.Core.Entities;
using FluentPOS.Shared.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Modules.Accounting.Core.Abstractions
{
    public interface IAccountingDbContext : IDbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Payment> Payments { get; set; }
    }
}