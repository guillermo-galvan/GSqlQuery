using FluentSQL.DatabaseManagement;

namespace FluentSQL.MySql
{
    public static class MySqlDatabaseManagmentExtension
    {
        public static TResult ExecuteWithTransaction<TResult>(this IExecute<TResult, MySqlDatabaseConnection> query)
        {
            using var connection = query.DatabaseManagment.GetConnection();
            using var transaction = connection.BeginTransaction();
            TResult result = query.Execute(transaction.Connection);
            transaction.Commit();
            connection.Close();
            return result;
        }

        public static TResult ExecuteWithTransaction<TResult>(this IExecute<TResult, MySqlDatabaseConnection> query, MySqlDatabaseTransaction transaction)
        {
            return query.Execute(transaction.Connection);
        }

        public static async Task<TResult> ExecuteWithTransactionAsync<TResult>(this IExecute<TResult, MySqlDatabaseConnection> query)
        {
            using var connection = await query.DatabaseManagment.GetConnectionAsync();
            using var transaction = await connection.BeginTransactionAsync();
            TResult result = await query.ExecuteAsync(transaction.Connection);
            await transaction.CommitAsync();
            await connection.CloseAsync();
            return result;
        }

        public static Task<TResult> ExecuteWithTransactionAsync<TResult>(this IExecute<TResult, MySqlDatabaseConnection> query, MySqlDatabaseTransaction transaction)
        {
            return query.ExecuteAsync(transaction.Connection);
        }
    }
}
