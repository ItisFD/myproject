
namespace MyBackendApi.Repositories
{
    using System.Reflection.Metadata.Ecma335;
    using System.Text;
    using Dapper;
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

        public async Task<int?> CreateAccountAsync(UserAccount data)
        {
            var sql = new StringBuilder();

            sql.Append("INSERT INTO user_accounts (Id, UserId, AccountNumber, ");
            sql.Append("Balance, CurrencyCode, Nickname, IsArchived) VALUES ");
            sql.Append("(id, user_id, account_number, balance, currency_code )");
            sql.Append("nickname, is_archived) ");

            var result = await _db.InsertAsync(sql.ToString(), data);

            return result;
        }

        public async Task<UserAccount?> GetAccountByIdAsync(int Id)
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
        public async Task<IEnumerable<UserAccount>> GetAccountsByUserIdAsync(int userId)
        {
            var sql = new StringBuilder();

            sql.Append("Select id, user_id AS UserId, account_number AS AccountNumber, ");
            sql.Append("balance, currency_code AS CurrencyCode, nickname, is_archived AS IsArchived,  ");
            sql.Append("created_at AS CreatedAt, updated_at AS UpdatedAt FROM user_accounts ");
            sql.Append("WHERE id = @id ");

            var result = await _db.SelectMultipleAsync<UserAccount>(sql.ToString(), userId);

            return result;
        }

        public async Task<int?> UpdateUserAccountAsync(int id, UserAccount data)
        {
            var sql = new StringBuilder();

            sql.Append("UPDATE user_accounts SET nickname = @Nickname, ");
            sql.Append("is_archived = @IsArchived WHERE id = @Id ");

            var result = await _db.WriteAsync(sql.ToString(), data);

            return result; // row affected
        }
        public async Task<int?> ArchiveUserAccountAsync(int id)
        {
            var sql = new StringBuilder();

            sql.Append("UPDATE user_accounts SET is_archived = 1 ");
            sql.Append("WHERE id = @id ");

            var result = await _db.WriteAsync(sql.ToString(), new {Id = id});

            return result;
        }

        public async Task<UserAccount?> UpdateUserAccountAndReturnRecordWithHelperAsyncLol(UserAccount data)
        {
            //Console.WriteLine($"[DAL] Incoming IsArchived = {data.IsArchived}");
            var updatedRow = new StringBuilder();
            var selectedRow = new StringBuilder();

            updatedRow.Append("UPDATE user_accounts SET nickname = @Nickname, ");
            updatedRow.Append("is_archived = @IsArchived WHERE id = @Id ");

            selectedRow.Append("SELECT * FROM user_accounts WHERE id = @Id ");

            return await _db.UpdateAndReturnAsync<UserAccount>(updatedRow.ToString(), selectedRow.ToString(), data);
        }



    }
}