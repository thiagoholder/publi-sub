using PubliSub.Banking.Application.Models;
using PubliSub.Banking.Application.Services;
using PubliSub.Banking.Domain.Commands;
using PubliSub.Banking.Domain.Interfaces;
using PubliSub.Banking.Domain.Models;
using PubliSub.Domain.Core.Bus;

namespace PubliSub.Banking.Domain.Tests
{
    public class AccountServicesTests
    {
        [Fact]
        public void GetAccounts_ReturnsAccountsFromRepository()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockEventBus = new Mock<IEventBus>();
            var accountService = new AccountService(mockAccountRepository.Object, mockEventBus.Object);

            var expectedAccounts = new List<Account>
        {
            new Account { Id = 1, AccountType = "Saving", AccountBalance = 1000.0M },
            new Account { Id = 2, AccountType = "Investments", AccountBalance = 500.0M }
        };

            mockAccountRepository.Setup(repo => repo.GetAccounts()).Returns(expectedAccounts);

            // Act
            var accounts = accountService.GetAccounts();

            // Assert
            Assert.Equal(expectedAccounts, accounts);
        }

        [Fact]
        public void Transfer_SendsCreateTransferCommandToEventBus()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockEventBus = new Mock<IEventBus>();
            var accountService = new AccountService(mockAccountRepository.Object, mockEventBus.Object);

            var accountTransfer = new AccountTransfer
            {
                FromAccount = 1,
                ToAccount = 2,
                TransferAmount = 100.0M
            };

            // Act
            accountService.Transfer(accountTransfer);

            // Assert
            mockEventBus.Verify(
                bus => bus.SendCommand(It.Is<CreateTransferCommand>(
                    cmd => cmd.From == accountTransfer.FromAccount
                        && cmd.To == accountTransfer.ToAccount
                        && cmd.Amount == accountTransfer.TransferAmount)),
                Times.Once);
        }
    }
}
