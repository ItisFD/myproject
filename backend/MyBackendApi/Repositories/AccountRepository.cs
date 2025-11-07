
namespace MyBackendApi.Repositories
{
    using System.Text;
    using MyBackendApi.DTO.Account;
    using MyBackendApi.Helpers;
    using MyBackendApi.Models.Banking;
    using MyBackendApi.Repositories;
    public class AccountRepository : IAccountRepository
    {
        // Inject db helper

        private readonly IDatabaseHelper _db;

        public AccountRepository(IDatabaseHelper db)
        {
            _db = db;
        }

        // Create DB calls

        public async Task<UserAccount?> GetByIdAsync(int Id)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT id, user_id AS UserId, account_number AS AccountNumber, ");
            sql.Append("balance, currency_code AS CurrencyCode, nickname, is_archived AS IsArchived, ");
            sql.Append("created_at AS CreatedAt, updated_at AS UpdatedAt FROM user_accounts ");
            sql.Append("WHERE id = @id ");

            var result = await _db.SelectSingleAsync<UserAccount>(sql.ToString(), new { Id });

            return result;
        }
        public async Task<IEnumerable<UserAccount>> GetAllAccounts()
        {
            var sql = new StringBuilder();

            sql.Append("SELECT id, user_id AS UserId, account_number AS AccountNumber, ");
            sql.Append("balance, currency_code AS CurrencyCode, nickname, is_archived AS IsArchived, ");
            sql.Append("created_at AS CreatedAt, updated_at AS UpdatedAt FROM user_accounts ");

            var result = await _db.SelectMultipleAsync<UserAccount>(sql.ToString());
            return result;
        }


    }
}