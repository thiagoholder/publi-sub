using PubliSub.Banking.Domain.Events;

namespace PubliSub.Banking.Tests
{
    public  class TransferCreatedEventTests
    {
        [Fact]
        public void TransferCreatedEvent_Initialization()
        {
            // Arrange
            int from = 1;
            int to = 2;
            decimal amount = 100.0M;

            // Act
            var transferCreatedEvent = new TransferCreatedEvent(from, to, amount);

            // Assert
            Assert.Equal(from, transferCreatedEvent.From);
            Assert.Equal(to, transferCreatedEvent.To);
            Assert.Equal(amount, transferCreatedEvent.Amount);
        }
    }
}
