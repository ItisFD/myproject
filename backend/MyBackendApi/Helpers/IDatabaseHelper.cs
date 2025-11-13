using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
namespace MyBackendApi.Helpers

{
    public interface IDatabaseHelper
    {
        Task<IEnumerable<T>> SelectMultipleAsync<T>(string sql, object? parameters = null);
        Task<T?> SelectSingleAsync<T>(string sql, object? parameters = null);
        Task<int> WriteAsync(string sql, object? parameters = null);
        Task<int> InsertAsync(string sql, object? parameters = null);
        // Transaction Method - Higher Order Helper
        Task<T?> WithTransactionAsync<T>(
            Func<IDbConnection, IDbTransaction, Task<T>> action
        );
        // Update and Return Async
        Task<T?> UpdateAndReturnAsync<T>(string updatedSql, string selectSql, object? parameters = null);
    }
}