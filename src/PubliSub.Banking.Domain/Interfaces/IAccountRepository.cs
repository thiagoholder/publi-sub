using PubliSub.Banking.Domain.Models;

namespace PubliSub.Banking.Domain.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccounts();
    }
}
