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
            TResult result = query.Execute(transaction.Connection);
            transaction.Commit();
            connection.Close();
            return result;
        }

        public static TResult ExecuteWithTransaction<TResult>(this IExecute<TResult, MySqlConnection> query, MySqlTransaction transaction)
        {
            return query.Execute(transaction.Connection);
        }
    }
}
