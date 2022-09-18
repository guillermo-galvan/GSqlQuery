using FluentSQL.Default;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL.MySql
{
    public static class MySqlDatabaseManagmentExtension
    {
        ///// <summary>
        ///// Open the connection and the transaction after executing the query, close the transaction and the connection by committing the transaction 
        ///// </summary>
        ///// <typeparam name="TResult">Result type</typeparam>
        ///// <param name="query">Query</param>
        ///// <param name="databaseManagement">Connection used for the transaction</param>
        ///// <returns>Query result</returns>
        //public static TResult ExecuteWithTransaction<TResult>(this ISetDatabaseManagement<TResult> query, IDatabaseManagement<MySqlConnection> databaseManagement)
        //{
        //    using MySqlConnection connection = databaseManagement.GetConnection();
        //    using MySqlTransaction transaction = connection.BeginTransaction();
        //    TResult result = query.SetDatabaseManagement(databaseManagement).Exec(transaction.Connection);
        //    transaction.Commit();
        //    connection.Close();
        //    return result;
        //}

        //public static TResult ExecuteWithTransaction<TResult>(this ISetDatabaseManagement<TResult> query, MySqlTransaction transaction)
        //{
        //    TResult result = query.SetDatabaseManagement(new MySqlDatabaseManagment(string.Empty)).Exec(transaction.Connection);
        //    return result;
        //}

        //public static TResult Execute<TResult>(this ISetDatabaseManagement<TResult> query, IDatabaseManagement<MySqlConnection> databaseManagement)
        //{
        //    using MySqlConnection connection = databaseManagement.GetConnection();            
        //    TResult result = query.SetDatabaseManagement(databaseManagement).Exec(connection);
        //    connection.Close();
        //    return result;
        //}

        //public static TResult Execute<TResult>(this ISetDatabaseManagement<TResult> query, MySqlConnection connection)
        //{
        //    TResult result = query.SetDatabaseManagement(new MySqlDatabaseManagment(string.Empty)).Exec(connection);            
        //    return result;
        //}

        //#region Return IEnumerable<T>

        //public static IEnumerable<T> Execute<T, TReturn>(this IQueryBuilderWithWhere<T, TReturn> andOr, IDatabaseManagement<MySqlConnection> databaseManagement)
        //    where T : class, new() where TReturn : IQuery<T>, ISetDatabaseManagement<IEnumerable<T>>
        //{
        //    return andOr.Build().Execute(databaseManagement);
        //}

        //public static IEnumerable<T> Execute<T, TReturn>(this IAndOr<T, TReturn> andOr, IDatabaseManagement<MySqlConnection> databaseManagement)
        //    where T : class, new() where TReturn : IQuery<T>, ISetDatabaseManagement<IEnumerable<T>>
        //{
        //    return andOr.Build().Execute(databaseManagement);
        //}

        //public static IEnumerable<T> Execute<T, TReturn>(this IAndOr<T, TReturn> andOr, MySqlConnection connection)
        //    where T : class, new() where TReturn : IQuery<T>, ISetDatabaseManagement<IEnumerable<T>>
        //{
        //    return andOr.Build().Execute(connection);
        //}
        //#endregion|

        //#region Delete Query
        //public static int Execute<T>(this IAndOr<T, DeleteQuery<T>> andOr, IDatabaseManagement<MySqlConnection> databaseManagement)
        //    where T : class, new()
        //{
        //    return andOr.Build().Execute(databaseManagement);
        //}

        //public static int ExecuteWithTransaction<T>(this IAndOr<T, DeleteQuery<T>> andOr, IDatabaseManagement<MySqlConnection> databaseManagement)
        //    where T : class, new()
        //{
        //    return andOr.Build().ExecuteWithTransaction(databaseManagement);
        //}

        //public static int ExecuteWithTransaction<T>(this IAndOr<T, DeleteQuery<T>> andOr, MySqlTransaction transaction)
        //    where T : class, new()
        //{
        //    return andOr.Build().ExecuteWithTransaction(transaction);
        //}
        //#endregion

        //#region ContinueExecute
        //public static TResult? StartWithTransaction<TResult>(this ContinueExecution<TResult, MySqlConnection> continueExecution)
        //{
        //    using MySqlConnection connection = continueExecution.DatabaseManagement.GetConnection();
        //    using MySqlTransaction transaction = connection.BeginTransaction();
        //    TResult? result = continueExecution.Start(transaction.Connection);
        //    transaction.Commit();
        //    connection.Close();
        //    return result;
        //}

        //public static TResult? StartWithTransaction<TResult>(this ContinueExecution<TResult, MySqlConnection> continueExecution, MySqlTransaction transaction)
        //{
        //    TResult? result = continueExecution.Start(transaction.Connection);
        //    return result;
        //}
        //#endregion

        //#region Update Query
        //public static int Execute<T>(this IAndOr<T, UpdateQuery<T>> andOr, IDatabaseManagement<MySqlConnection> databaseManagement)
        //    where T : class, new()
        //{
        //    return andOr.Build().Execute(databaseManagement);
        //}

        //public static int ExecuteWithTransaction<T>(this IAndOr<T, UpdateQuery<T>> andOr, IDatabaseManagement<MySqlConnection> databaseManagement)
        //    where T : class, new()
        //{
        //    return andOr.Build().ExecuteWithTransaction(databaseManagement);
        //}

        //public static int ExecuteWithTransaction<T>(this IAndOr<T, UpdateQuery<T>> andOr, MySqlTransaction transaction)
        //    where T : class, new()
        //{
        //    return andOr.Build().ExecuteWithTransaction(transaction);
        //}
        //#endregion

        //#region BatchExecute<TDbConnection>
        //public static int ExecuteWithTransaction(this BatchExecute<MySqlConnection> batchExecute)
        //{
        //    using MySqlConnection connection = batchExecute.DatabaseManagement.GetConnection();
        //    using MySqlTransaction transaction = connection.BeginTransaction();
        //    int result = batchExecute.ExecuteWithTransaction(transaction);
        //    transaction.Commit();
        //    connection.Close();
        //    return result;
        //}

        //public static int ExecuteWithTransaction(this BatchExecute<MySqlConnection> batchExecute, MySqlTransaction transaction)
        //{
        //    return batchExecute.Exec(transaction.Connection);
        //}
        //#endregion

        //#region Count Query

        //public static long Execute<T>(this IQueryBuilderWithWhere<T, CountQuery<T>> andOr, IDatabaseManagement<MySqlConnection> databaseManagement)
        //    where T : class, new()
        //{
        //    return andOr.Build().Execute(databaseManagement);
        //}

        //public static long Execute<T>(this IAndOr<T, CountQuery<T>> andOr, IDatabaseManagement<MySqlConnection> databaseManagement)
        //    where T : class, new()
        //{
        //    return andOr.Build().Execute(databaseManagement);
        //}

        //public static long ExecuteWithTransaction<T>(this IAndOr<T, CountQuery<T>> andOr, IDatabaseManagement<MySqlConnection> databaseManagement)
        //    where T : class, new()
        //{
        //    return andOr.Build().ExecuteWithTransaction(databaseManagement);
        //}

        //public static long ExecuteWithTransaction<T>(this IAndOr<T, CountQuery<T>> andOr, MySqlTransaction transaction)
        //    where T : class, new()
        //{
        //    return andOr.Build().ExecuteWithTransaction(transaction);
        //}
        //#endregion

    }
}
