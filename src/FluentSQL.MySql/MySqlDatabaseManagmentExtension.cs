using MySql.Data.MySqlClient;

namespace FluentSQL.MySql
{
    public static class MySqlDatabaseManagmentExtension
    {
        public static TResult ExecuteWithTransaction<TResult>(this IExecute<TResult, MySqlConnection> query)
        {
            using MySqlConnection connection = query.DatabaseManagment.GetConnection();
            using MySqlTransaction transaction = connection.BeginTransaction();
            TResult result = query.Execute(transaction.Connection);
            transaction.Commit();
            connection.Close();
            return result;
        }

        public static TResult ExecuteWithTransaction<TResult>(this IExecute<TResult, MySqlConnection> query, MySqlTransaction transaction)
        {
            return query.Execute(transaction.Connection);
        }

        public static async Task<TResult> ExecuteWithTransactionAsync<TResult>(this IExecute<TResult, MySqlConnection> query)
        {
            using MySqlConnection connection = await query.DatabaseManagment.GetConnectionAsync();
            using MySqlTransaction transaction = await connection.BeginTransactionAsync();
            TResult result = await query.ExecuteAsync(transaction.Connection);
            await transaction.CommitAsync();
            await connection.CloseAsync();
            return result;
        }

        public static Task<TResult> ExecuteWithTransactionAsync<TResult>(this IExecute<TResult, MySqlConnection> query, MySqlTransaction transaction)
        {
            return query.ExecuteAsync(transaction.Connection);
        }
    }
}
