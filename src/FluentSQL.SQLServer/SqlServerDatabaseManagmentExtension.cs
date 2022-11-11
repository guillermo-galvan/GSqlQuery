using Microsoft.Data.SqlClient;

namespace FluentSQL.SQLServer
{
    public static  class SqlServerDatabaseManagmentExtension
    {
        public static TResult ExecuteWithTransaction<TResult>(this IExecute<TResult, SqlConnection> query)
        {
            using SqlConnection connection = query.DatabaseManagment.GetConnection();
            using SqlTransaction transaction = connection.BeginTransaction();
            TResult result = query.Execute(transaction.Connection);
            transaction.Commit();
            connection.Close();
            return result;
        }
        public static TResult ExecuteWithTransaction<TResult>(this IExecute<TResult, SqlConnection> query, SqlTransaction transaction)
        {
            return query.Execute(transaction.Connection);
        }

        public static async Task<TResult> ExecuteWithTransactionAsync<TResult>(this IExecute<TResult, SqlConnection> query)
        {
            using SqlConnection connection = await query.DatabaseManagment.GetConnectionAsync();
            using SqlTransaction transaction = (SqlTransaction)await connection.BeginTransactionAsync();
            TResult result = await query.ExecuteAsync(transaction.Connection);
            await transaction.CommitAsync();
            await connection.CloseAsync();
            return result;
        }

        public static Task<TResult> ExecuteWithTransactionAsync<TResult>(this IExecute<TResult, SqlConnection> query, SqlTransaction transaction)
        {
            return query.ExecuteAsync(transaction.Connection);
        }
    }
}
