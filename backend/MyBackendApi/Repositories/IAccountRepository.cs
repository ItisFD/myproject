
namespace MyBackendApi.Repositories
{
    using MyBackendApi.Models.Banking;

    public interface IAccountRepository
    {
        Task<UserAccount?> GetByIdAsync(int id);
        Task<IEnumerable<UserAccount>> GetAllAccounts();
    }
}