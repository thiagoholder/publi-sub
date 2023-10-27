using MediatR;
using Moq;
using PubliSub.Banking.Domain.Commands;
using PubliSub.Banking.Domain.Events;
using PubliSub.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubliSub.Infra.Bus.Tests
{
    public class RabbitMQBusTests
    {
        [Fact]
        public async Task SendCommand_WithValidCommand_ShouldSendToMediator()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var bus = new RabbitMQBus(mediator.Object, null);
            var command = new CreateTransferCommand(1,2,3);


            // Act
            var sendTask = bus.SendCommand(command);
            await Task.WhenAll(sendTask);

            // Assert
            mediator.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
