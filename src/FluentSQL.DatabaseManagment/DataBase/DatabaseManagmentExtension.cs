﻿using FluentSQL.DatabaseManagement;

namespace FluentSQL.DataBase
{
    public static class DatabaseManagmentExtension
    {
        public static TResult ExecuteWithTransaction<TResult>(this IExecute<TResult, IConnection> query)
        {
            using var connection = query.DatabaseManagment.GetConnection();
            using var transaction = connection.BeginTransaction();
            TResult result = query.Execute(transaction.Connection);
            transaction.Commit();
            connection.Close();
            return result;
        }

        public static TResult ExecuteWithTransaction<TResult>(this IExecute<TResult, IConnection> query, ITransaction transaction)
        {
            return query.Execute(transaction.Connection);
        }

        public static async Task<TResult> ExecuteWithTransactionAsync<TResult>(this IExecute<TResult, IConnection> query)
        {
            using var connection = await query.DatabaseManagment.GetConnectionAsync();
            using var transaction = await connection.BeginTransactionAsync();
            TResult result = await query.ExecuteAsync(transaction.Connection);
            await transaction.CommitAsync();
            await connection.CloseAsync();
            return result;
        }

        public static Task<TResult> ExecuteWithTransactionAsync<TResult>(this IExecute<TResult, IConnection> query, ITransaction transaction)
        {
            return query.ExecuteAsync(transaction.Connection);
        }
    }
}