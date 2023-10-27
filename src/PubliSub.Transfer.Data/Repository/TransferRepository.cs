
using PubliSub.Transfer.Data.Context;
using PubliSub.Transfer.Domain.Interfaces;
using PubliSub.Transfer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubliSub.Transfer.Data.Repository
{
    public class TransferRepository : ITransferRepository
    {
        private TransferDbContext _dbContext;

        public TransferRepository(TransferDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _dbContext.TransferLogs;
        }

        public void Add(TransferLog transferLog)
        {
            _dbContext.TransferLogs.Add(transferLog);
            _dbContext.SaveChanges();
        }

    }
}
