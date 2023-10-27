using Microsoft.EntityFrameworkCore;
using PubliSub.Banking.Domain.Models;

namespace PubliSub.Banking.Data.Context
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions options) : base(options) 
        { 
        }

        public DbSet<Account>? Accounts { get; set; }
    }
}
