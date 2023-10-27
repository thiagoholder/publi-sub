using PubliSub.Banking.Domain.CommandHandlers;
using PubliSub.Banking.Domain.Commands;
using PubliSub.Banking.Domain.Events;
using PubliSub.Domain.Core.Bus;

namespace PubliSub.Banking.Domain.Tests
{
    public class TransferCommnadHandlerTests
    {
        [Fact]
        public async Task Handle_ValidTransferCommand_PublishesTransferCreatedEvent()
        {
            // Arrange
            var mockEventBus = new Mock<IEventBus>();
            var handler = new TransferCommandHandler(mockEventBus.Object);
            var transferCommand = new CreateTransferCommand(from: 1, to: 3, amount: 100.0M);

            // Act
            await handler.Handle(transferCommand, CancellationToken.None);

            // Assert
            mockEventBus.Verify(
                bus => bus.Publish(It.Is<TransferCreatedEvent>(
                    e => e.From == transferCommand.From
                         && e.To == transferCommand.To
                         && e.Amount == transferCommand.Amount)),
                Times.Once);
        }
    }
}