
using MyBackendApi.Models.Banking;
using MyBackendApi.DTO.Account;

namespace MyBackendApi.Mappers
{
    public static class AccountMapper
    {

        // Map single UserAccount Model to DTO
        public static AccountDto ToDto(this UserAccount model)
        {
            return new AccountDto
            {
                AccountNumber = model.AccountNumber,
                Balance = model.Balance,
                CurrencyCode = model.CurrencyCode,
                Nickname = model.Nickname,
                UpdatedAt = model.UpdatedAt
            };
        }
        // Map List(IEnumerable) of UserAccounts Models to DTO
        public static IEnumerable<AccountDto> ToDto(this IEnumerable<UserAccount> models)
        {
            return models.Select(m => m.ToDto()); // for every m, use extension method ToDto
            /*this method in long form
            var list = new List<AccountDto>();
            foreach (var m in models)
            {
                list.Add(m.ToDto());
            }
            return list;

            */
        }

        public static AccountUpdateDto UpdateToDto(this UserAccount model)
        {
            return new AccountUpdateDto
            {
                Nickname = model.Nickname,
                IsArchived = model.IsArchived
            };
        }
        /* Methods to be created :
        1. Creation
        2. Admin Account Access
        */
        
        
    }


}