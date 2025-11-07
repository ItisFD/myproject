using MyBackendApi.DTO.Account;

namespace MyBackendApi.Services
{
    public interface IAccountService
    {
        Task<AccountDto?> GetAccountByIdAsync(int id);
        Task<IEnumerable<AccountDto>> GetAllAccounts();
    }
}