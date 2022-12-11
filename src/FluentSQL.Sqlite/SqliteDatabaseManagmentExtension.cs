using FluentSQL.DatabaseManagement;

namespace FluentSQL.Sqlite
{
    public static class SqliteDatabaseManagmentExtension
    {
        public static TResult ExecuteWithTransaction<TResult>(this IExecute<TResult, SqliteDatabaseConnection> query)
        {
            query.DatabaseManagment.GetConnection();

            using var connection = query.DatabaseManagment.GetConnection();
            using var transaction = connection.BeginTransaction();
            TResult result = query.Execute(transaction.Connection);
            transaction.Commit();
            connection.Close();
            return result;
        }
        public static TResult ExecuteWithTransaction<TResult>(this IExecute<TResult, SqliteDatabaseConnection> query, SqliteDatabaseTransaction transaction)
        {
            return query.Execute(transaction.Connection);
        }

        public static async Task<TResult> ExecuteWithTransactionAsync<TResult>(this IExecute<TResult, SqliteDatabaseConnection> query)
        {
            using var connection = await query.DatabaseManagment.GetConnectionAsync();
            using var transaction = await connection.BeginTransactionAsync();
            TResult result = await query.ExecuteAsync(transaction.Connection);
            await transaction.CommitAsync();
            await connection.CloseAsync();
            return result;
        }

        public static Task<TResult> ExecuteWithTransactionAsync<TResult>(this IExecute<TResult, SqliteDatabaseConnection> query, SqliteDatabaseTransaction transaction)
        {
            return query.ExecuteAsync(transaction.Connection);
        }
    }
}
