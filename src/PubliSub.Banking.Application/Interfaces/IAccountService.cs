using PubliSub.Banking.Domain.Models;

namespace PubliSub.Banking.Application.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
    }
}
