
namespace MyBackendApi.Repositories
{
    using MyBackendApi.DTO.Account;
    using MyBackendApi.Models.Banking;

    public interface IAccountRepository
    {
        Task<int?> CreateAccountAsync(UserAccount data); // Insert a new account and return its id?
        Task<UserAccount?> GetAccountByIdAsync(int id); // Retrieve single account by db id?
        Task<IEnumerable<UserAccount>> GetAllAccounts(); // Retrieve all accounts under user_accounts
        Task<IEnumerable<UserAccount>> GetAccountsByUserIdAsync(int userId); // Retrieve all accounts under a user
        Task<int?> UpdateUserAccountAsync(int id, UserAccount data); // Update an account's user fields
        Task<int?> ArchiveUserAccountAsync(int id); // Set is archived to 1 in db
        Task<UserAccount?> UpdateUserAccountAndReturnRecordWithHelperAsyncLol(UserAccount data);
    }
}