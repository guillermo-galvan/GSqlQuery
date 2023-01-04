using GSqlQuery.Runner;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.MySql
{
    public static class MySqlDatabaseManagementExtension
    {
        public static TResult ExecuteWithTransaction<TResult>(this IExecuteDatabaseManagement<TResult, MySqlDatabaseConnection> query)
        {
            using (var connection = query.DatabaseManagment.GetConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    TResult result = query.Execute(transaction.Connection);
                    transaction.Commit();
                    connection.Close();
                    return result;
                }
            }
        }

        public static TResult ExecuteWithTransaction<TResult>(this IExecuteDatabaseManagement<TResult, MySqlDatabaseConnection> query, MySqlDatabaseTransaction transaction)
        {
            return query.Execute(transaction.Connection);
        }

        public static async Task<TResult> ExecuteWithTransactionAsync<TResult>(this IExecuteDatabaseManagement<TResult, MySqlDatabaseConnection> query, CancellationToken cancellationToken = default)
        {
            using (var connection = await query.DatabaseManagment.GetConnectionAsync(cancellationToken))
            {
                using (var transaction = await connection.BeginTransactionAsync(cancellationToken))
                {
                    TResult result = await query.ExecuteAsync(transaction.Connection, cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    await connection.CloseAsync(cancellationToken);
                    return result;
                }
            }
        }

        public static Task<TResult> ExecuteWithTransactionAsync<TResult>(this IExecuteDatabaseManagement<TResult, MySqlDatabaseConnection> query, MySqlDatabaseTransaction transaction,
            CancellationToken cancellationToken = default)
        {
            return query.ExecuteAsync(transaction.Connection,cancellationToken);
        }
    }
}
