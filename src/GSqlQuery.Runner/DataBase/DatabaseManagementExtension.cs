using System;
using System.Threading.Tasks;

namespace GSqlQuery.Runner
{
    public static class DatabaseManagementExtension
    {
        public static TResult ExecuteWithTransaction<TResult>(this IExecute<TResult, IConnection> query)
        {
            using (IConnection connection = query.DatabaseManagement.GetConnection())
            {
                using (ITransaction transaction = connection.BeginTransaction())
                {
                    TResult result = query.Execute(transaction.Connection);
                    transaction.Commit();
                    connection.Close();
                    return result;
                }
            }
        }

        public static TResult ExecuteWithTransaction<TResult>(this IExecute<TResult, IConnection> query, ITransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction), ErrorMessages.ParameterNotNull);
            }
            return query.Execute(transaction.Connection);
        }

        public static async Task<TResult> ExecuteWithTransactionAsync<TResult>(this IExecute<TResult, IConnection> query)
        {
            using (IConnection connection = await query.DatabaseManagement.GetConnectionAsync().ConfigureAwait(false))
            {
                using (ITransaction transaction = await connection.BeginTransactionAsync().ConfigureAwait(false))
                {
                    TResult result = await query.ExecuteAsync(transaction.Connection).ConfigureAwait(false);
                    await transaction.CommitAsync().ConfigureAwait(false);
                    await connection.CloseAsync().ConfigureAwait(false);
                    return result;
                }
            }
        }

        public static Task<TResult> ExecuteWithTransactionAsync<TResult>(this IExecute<TResult, IConnection> query, ITransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction), ErrorMessages.ParameterNotNull);
            }

            return query.ExecuteAsync(transaction.Connection);
        }
    }
}