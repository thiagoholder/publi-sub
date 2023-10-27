using PubliSub.Transfer.Application.Interfaces;
using PubliSub.Transfer.Domain.Models;
using PubliSub.Domain.Core.Bus;
using PubliSub.Transfer.Domain.Interfaces;

namespace PubliSub.Transfer.Application.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IEventBus _eventBus;

        public TransferService(ITransferRepository transferRepository, IEventBus eventBus)
        {
            _transferRepository = transferRepository;
            _eventBus = eventBus;
        }

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _transferRepository.GetTransferLogs();
        }
    }
}
