using System.Threading.Tasks;
using System.Collections.Generic;
namespace MyBackendApi.Helpers

{
    public interface IDatabaseHelper
    {
        Task<IEnumerable<T>> SelectMultipleAsync<T>(string sql, object? parameters = null);
        Task<T?> SelectSingleAsync<T>(string sql, object? parameters = null);

        Task<int> WriteAsync(string sql, object? parameters = null);

        Task<int> InsertAsync(string sql, object? parameters = null);
        
        
        // Define database helper methods here
    }
}