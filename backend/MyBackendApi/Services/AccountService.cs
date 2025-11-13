using MyBackendApi.DTO.Account;
using MyBackendApi.Mappers;
using MyBackendApi.Models.Banking;
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
                var account = await _repo.GetAccountByIdAsync(id);

                // Logic for returning to controller
                if (account == null) return null;

                return account.ToDto();
            }
        public async Task<IEnumerable<AccountDto>> GetAllAccounts()
        {
            var accounts = await _repo.GetAllAccounts();
            return accounts?.ToDto() ?? Enumerable.Empty<AccountDto>();

        }
        public async Task<int?> ArchiveUserAccountAsync(int id)
        {
            var result = await _repo.ArchiveUserAccountAsync(id);
            return result;
        }
        public async Task<AccountUpdateDto?> UpdateUserAccountAsync(int id, AccountUpdateDto data)
        {
            var account = data.ToUpdateModel(id); // account is of model type UserAccount
            var result = await _repo.UpdateUserAccountAndReturnRecordWithHelperAsyncLol(account);
            // result is of type UserAccount

            if (result == null) return null;
            // must turn it back into a dto

            var return_dto = result.ToUpdateDto();

            return return_dto;

        }
            

  
            
            
        }
    }