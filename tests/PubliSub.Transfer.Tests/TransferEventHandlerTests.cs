using Moq;
using PubliSub.Transfer.Domain.EventHandlers;
using PubliSub.Transfer.Domain.Events;
using PubliSub.Transfer.Domain.Interfaces;
using PubliSub.Transfer.Domain.Models;

namespace PubliSub.Transfer.Tests
{
    public class TransferEventHandlerTests
    {
        [Fact]
        public async Task Handle_ValidTransferCreatedEvent_AddsTransferLogToRepository()
        {
            // Arrange
            var mockTransferRepository = new Mock<ITransferRepository>();
            var transferEventHandler = new TransferEventHandler(mockTransferRepository.Object);

            var transferCreatedEvent = new TransferCreatedEvent(from: 1, to: 2, 100.0M);

            // Act
            await transferEventHandler.Handle(transferCreatedEvent);

            // Assert
            mockTransferRepository.Verify(
                repo => repo.Add(It.Is<TransferLog>(
                    log => log.FromAccount == transferCreatedEvent.From
                        && log.ToAccount == transferCreatedEvent.To
                        && log.TransferAmount == transferCreatedEvent.Amount)),
                Times.Once);
        }
    }
}