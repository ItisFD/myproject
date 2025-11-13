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
        private MySqlConnection GetConnection() => new MySqlConnection(_connectionString);

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
        /// <summary>
        /// Executes a database operation within a transaction using a higher-order function.
        /// </summary>
        /// <typeparam name="T">The type of the result returned from the operation.</typeparam>
        /// <param name="action">
        /// A delegate that receives an <see cref="IDbConnection"/> and an <see cref="IDbTransaction"/> 
        /// to execute any database logic. This allows the caller to define custom queries or updates
        /// while the transaction and connection are managed automatically.
        /// </param>
        /// <returns>
        /// The result of type <typeparamref name="T"/> returned by the delegate.
        /// Returns null if the operation does not produce a result.
        /// </returns>
        /// <remarks>
        /// - Opens a new MySQL connection (via <see cref="GetConnection"/>).  
        /// - Begins a transaction and commits if the delegate succeeds.  
        /// - Rolls back the transaction if an exception occurs.  
        /// - Encapsulates Dapper or ADO.NET calls, so repositories do not manage connections/transactions directly.  
        /// - Useful for any scenario where multiple operations must be atomic.
        /// </remarks>
        public async Task<T?> WithTransactionAsync<T>(
            Func<IDbConnection, IDbTransaction, Task<T>> action // delegate type parameter, caller passes function instead of value
        )
        {
            await using var db = GetConnection(); // ADO.NET Connection
            await db.OpenAsync();           // open connection

            await using var transaction = await db.BeginTransactionAsync(); // Begin Transaction

            try
            {
                var result = await action(db, transaction); // run user defined logic
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

        public async Task<T?> UpdateAndReturnAsync<T>(string updateSql, string selectSql, object? parameters)
        {
            return await WithTransactionAsync(async (db, tx) => // transaction is passed to ensure the same transaction is used and passed per sql command
            {   // Anything in here is what is considered the "action"
                await db.ExecuteAsync(updateSql, parameters, tx); // Dapper inside helper
                return await db.QuerySingleOrDefaultAsync<T>(selectSql, parameters, tx);
            });
        }


        


        // 11/11 9:20PM Fer Lyndon Mallari
        // This is how we originally created transactions 
        /* Transaction with using syntax
        public async Task<T?> WriteAndReturnRecordUsingAsync<T>(
            string updateSql,
            string selectSql,
            object? parameters = null)
        {
            // ADO.NET resource handling
            await using var db = GetConnection();              // ADO.NET primitive
            await db.OpenAsync();                              // open physical connection

            await using var transaction = await db.BeginTransactionAsync(); // begin transaction

            try
            {
                // Dapper method (wraps ADO.NET ExecuteNonQuery)
                await db.ExecuteAsync(updateSql, parameters, transaction);

                // Dapper method (wraps ADO.NET ExecuteReader)
                var result = await db.QuerySingleOrDefaultAsync<T>(selectSql, parameters, transaction);

                await transaction.CommitAsync(); // ADO.NET commit
                return result;
            }
            catch
            {
                await transaction.RollbackAsync(); // ADO.NET rollback
                throw;
            }
        }

        Repo Usage Example
        public class UserRepository
        {
            private readonly IDatabaseHelper _dbHelper;

            public UserRepository(IDatabaseHelper dbHelper)
            {
                _dbHelper = dbHelper;
            }

            // Example: Update a user's email and return the updated record
            public async Task<User?> UpdateEmailAsync(int id, string newEmail)
            {
                var updateSql = @"UPDATE Users 
                                SET Email = @Email, UpdatedAt = NOW() 
                                WHERE Id = @Id;";

                var selectSql = "SELECT * FROM Users WHERE Id = @Id;";

                return await _dbHelper.WriteAndReturnRecordUsingAsync<User>(
                    updateSql,
                    selectSql,
                    new { Email = newEmail, Id = id }
                );
            }
        }
        */




    }
}