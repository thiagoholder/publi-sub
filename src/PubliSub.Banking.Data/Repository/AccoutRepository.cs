using PubliSub.Banking.Data.Context;
using PubliSub.Banking.Domain.Interfaces;
using PubliSub.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubliSub.Banking.Data.Repository
{
    public class AccoutRepository : IAccountRepository
    {
        private BankingDbContext _dbContext;

        public AccoutRepository(BankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _dbContext.Accounts;
        }
    }
}
