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
        public static TResult ExecuteWithTransaction<TResult>(this IExecute<TResult, MySqlConnection> query)
        {
            using MySqlConnection connection = query.DatabaseManagment.GetConnection();
            using MySqlTransaction transaction = connection.BeginTransaction();
            TResult result = query.Exec(transaction.Connection);
            transaction.Commit();
            connection.Close();
            return result;
        }

        public static TResult ExecuteWithTransaction<TResult>(this IExecute<TResult, MySqlConnection> query, MySqlTransaction transaction)
        {
            return query.Exec(transaction.Connection);
        }

        public static TResult ExecuteWithTransaction<T, TReturn, TResult>(this IQueryBuilder<T, TReturn, MySqlConnection> query)
            where T : class, new() where TReturn : IQuery<T, MySqlConnection, TResult>
        {
            return query.Build().ExecuteWithTransaction();
        }

        public static TResult ExecuteWithTransaction<T, TReturn, TResult>(this IQueryBuilder<T, TReturn, MySqlConnection> query,
            MySqlTransaction transaction)
            where T : class, new() where TReturn : IQuery<T, MySqlConnection, TResult>
        {
            return query.Build().ExecuteWithTransaction(transaction);
        }

        public static TResult ExecuteWithTransaction<T, TReturn, TResult>(this IAndOr<T, TReturn> query)
           where T : class, new() where TReturn : IQuery<T, MySqlConnection, TResult>
        {
            return query.Build().ExecuteWithTransaction();
        }

        public static TResult ExecuteWithTransaction<T, TReturn, TResult>(this IAndOr<T, TReturn> query,
            MySqlTransaction transaction)
            where T : class, new() where TReturn : IQuery<T, MySqlConnection, TResult>
        {
            return query.Build().ExecuteWithTransaction(transaction);
        }

        public static TResult Execute<T, TReturn, TResult>(this IAndOr<T, TReturn> query)
          where T : class, new() where TReturn : IQuery<T, MySqlConnection, TResult>
        {
            return query.Build().Exec();
        }

        public static TResult Execute<T, TReturn, TResult>(this IAndOr<T, TReturn> query, MySqlConnection connection)
          where T : class, new() where TReturn : IQuery<T, MySqlConnection, TResult>
        {
            return query.Build().Exec(connection);
        }

        public static TResult Execute<T, TReturn, TResult>(this IQueryBuilder<T, TReturn, MySqlConnection> query)
          where T : class, new() where TReturn : IQuery<T, MySqlConnection, TResult>
        {
            return query.Build().Exec();
        }

        public static TResult Execute<T, TReturn, TResult>(this IQueryBuilder<T, TReturn, MySqlConnection> query, MySqlConnection connection)
          where T : class, new() where TReturn : IQuery<T, MySqlConnection, TResult>
        {
            return query.Build().Exec(connection);
        }
    }
}
