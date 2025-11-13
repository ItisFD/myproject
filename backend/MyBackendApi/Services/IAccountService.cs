using MyBackendApi.DTO.Account;
using MyBackendApi.Models.Banking;

namespace MyBackendApi.Services
{
    public interface IAccountService
    {
        Task<AccountDto?> GetAccountByIdAsync(int id);
        Task<IEnumerable<AccountDto>> GetAllAccounts();
        //Task<IEnumerable<AccountDto>> GetAccountsByUserIdAsync(int id);
        //Task<AccountDto> UpdateUserAccountAsync(int id);
        Task<int?> ArchiveUserAccountAsync(int id);
        Task<AccountUpdateDto?> UpdateUserAccountAsync(int id, AccountUpdateDto data);
    }
}