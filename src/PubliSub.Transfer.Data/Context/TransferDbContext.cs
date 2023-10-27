using Microsoft.EntityFrameworkCore;
using PubliSub.Transfer.Domain.Models;

namespace PubliSub.Transfer.Data.Context
{
    public class TransferDbContext : DbContext
    {
        public TransferDbContext(DbContextOptions options) : base(options) 
        { 
        }

        public DbSet<TransferLog>? TransferLogs { get; set; }

       
    }
}
