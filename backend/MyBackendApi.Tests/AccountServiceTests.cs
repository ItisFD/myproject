using Moq; // for mocking dependencies
using Xunit;
using MyBackendApi.Services;
using MyBackendApi.Repositories;
using MyBackendApi.Models.Banking;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AccountServiceTests
{
    [Fact]
    public async Task GetAccountByIdAsync_ReturnsDto_WhenAccountExists()
    {
        // Arrange
        var mockRepo = new Mock<IAccountRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(new UserAccount
                {
                    Id = 1,
                    UserId = 42,
                    AccountNumber = "ACC123",
                    Balance = 500.00m
                });

        var service = new AccountService(mockRepo.Object);

        // Act
        var result = await service.GetAccountByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("ACC123", result.AccountNumber);
        Assert.Equal(500.00m, result.Balance);
    }

    [Fact]
    public async Task GetAllAccounts_ReturnsMappedDtos()
    {
        // Arrange
        var mockRepo = new Mock<IAccountRepository>();
        mockRepo.Setup(r => r.GetAllAccounts())
                .ReturnsAsync(new List<UserAccount>
                {
                    new() { Id = 1, AccountNumber = "A1" },
                    new() { Id = 2, AccountNumber = "A2" }
                });

        var service = new AccountService(mockRepo.Object);

        // Act
        var result = await service.GetAllAccounts();

        // Assert
        Assert.Collection(result,
            dto => Assert.Equal("A1", dto.AccountNumber),
            dto => Assert.Equal("A2", dto.AccountNumber));
    }
}
