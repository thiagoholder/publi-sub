using PubliSub.Transfer.Domain.Models;

namespace PubliSub.Transfer.Application.Interfaces
{
    public interface ITransferService
    {
        IEnumerable<TransferLog> GetTransferLogs();
    }
}
