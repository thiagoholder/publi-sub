using PubliSub.Banking.Application.Interfaces;
using PubliSub.Banking.Application.Models;
using PubliSub.Banking.Domain.Commands;
using PubliSub.Banking.Domain.Interfaces;
using PubliSub.Banking.Domain.Models;
using PubliSub.Domain.Core.Bus;

namespace PubliSub.Banking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEventBus _eventBus;

        public AccountService(IAccountRepository accountRepository, IEventBus eventBus)
        {
            _accountRepository = accountRepository;
            _eventBus = eventBus;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public void Transfer(AccountTransfer accountTransfer)
        {
            var createTransferCommand = new CreateTransferCommand(accountTransfer.FromAccount,
                                                                  accountTransfer.ToAccount,
                                                                  accountTransfer.TransferAmount);

            _eventBus.SendCommand(createTransferCommand);
        }
    }
}
