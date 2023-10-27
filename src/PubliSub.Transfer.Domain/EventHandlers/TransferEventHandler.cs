using PubliSub.Domain.Core.Bus;
using PubliSub.Transfer.Domain.Events;

namespace PubliSub.Transfer.Domain.EventHandlers
{
    public class TransferEventHandler : IEventHandler<TransferCreatedEvent>
    {

        public TransferEventHandler()
        {
            
        }

        public Task Handle(TransferCreatedEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
