using PubliSub.Banking.Application.Interfaces;
using PubliSub.Banking.Domain.Interfaces;
using PubliSub.Banking.Domain.Models;

namespace PubliSub.Banking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;   
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }
    }
}
