using MyBackendApi.DTO.Account;
using MyBackendApi.Mappers;
using MyBackendApi.Repositories;

    namespace MyBackendApi.Services
    {

        public class AccountService : IAccountService
        {

        // inject repository
            private readonly IAccountRepository _repo;

            public AccountService(IAccountRepository repo)
            {
                _repo = repo;
            }

            public async Task<AccountDto?> GetAccountByIdAsync(int id)
            {
                // Where logic for mapping to a Model would be
                // for this specific method, we dont need to pass a model because its only 1 field
                var account = await _repo.GetByIdAsync(id);

                // Logic for returning to controller
                if (account == null) return null;

                return account.ToDto();
            }
            public async Task<IEnumerable<AccountDto>> GetAllAccounts()
            {
                var accounts =  await _repo.GetAllAccounts();
                return accounts?.ToDto() ?? Enumerable.Empty<AccountDto>();

            }    
        }
    }