using System.Data;
using System.Data.Common;
using Dapper;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBackendApi.Helpers
{
    public class DatabaseHelper : IDatabaseHelper
    {
        // Pull connection string here


        // This is our injection/Contructor for our connection string from appsettings
        private readonly string _connectionString;
        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Creates a GetConnection method that instantiates a 'MySqlConnection' given the connection string - TCP
        // connection string must be in expected format
        // Currently does not include port specification
        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        // Read Multiple
        public async Task<IEnumerable<T>> SelectMultipleAsync<T>(string sql, object? parameters = null)
        {
            using (var db = GetConnection())
            {
                return await db.QueryAsync<T>(sql, parameters);
            }
        }
    
        public async Task<T?> SelectSingleAsync<T>(string sql, object? parameters = null)

        {
            using (var db = GetConnection())
            {
                return await db.QueryFirstOrDefaultAsync<T>(sql, parameters);
            }
        }

        public async Task<int> WriteAsync(string sql, object? parameters = null)
        {
            using (var db = GetConnection())
            {
                return await db.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<int> InsertAsync(string sql, object? parameters = null)
        {
            using (var db = GetConnection())
            {
                return await db.ExecuteAsync(sql, parameters);
            }
        }




    }
}