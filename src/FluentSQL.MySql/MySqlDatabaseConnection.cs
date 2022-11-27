﻿using FluentSQL.DataBase;
using MySql.Data.MySqlClient;
using System.Data;

namespace FluentSQL.MySql
{
    public sealed class MySqlDatabaseConnection : Connection, IConnection
    {
        public MySqlDatabaseConnection(string connectionString) : base(new MySqlConnection(connectionString))
        {}

        public MySqlDatabaseTransaction BeginTransaction()
        {
            return (MySqlDatabaseTransaction)SetTransaction(new MySqlDatabaseTransaction(this, ((MySqlConnection)_connection).BeginTransaction()));
        }

        public MySqlDatabaseTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return (MySqlDatabaseTransaction)SetTransaction(new MySqlDatabaseTransaction(this, ((MySqlConnection)_connection).BeginTransaction(isolationLevel)));
        }

        public async Task<MySqlDatabaseTransaction> BeginTransactionAsync()
        {
            return (MySqlDatabaseTransaction)SetTransaction(new MySqlDatabaseTransaction(this, await ((MySqlConnection)_connection).BeginTransactionAsync()));
        }

        public async Task<MySqlDatabaseTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            return 
                (MySqlDatabaseTransaction)SetTransaction(new MySqlDatabaseTransaction(this, 
                await ((MySqlConnection)_connection).BeginTransactionAsync(isolationLevel, cancellationToken)));
        }

        public override Task CloseAsync(CancellationToken cancellationToken = default)
        {
            return ((MySqlConnection)_connection).CloseAsync(cancellationToken);
        }

        ITransaction IConnection.BeginTransaction() => BeginTransaction();

        ITransaction IConnection.BeginTransaction(IsolationLevel isolationLevel) => BeginTransaction(isolationLevel);

        async Task<ITransaction> IConnection.BeginTransactionAsync() => await BeginTransactionAsync();

        async Task<ITransaction> IConnection.BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default) => 
            await BeginTransactionAsync(isolationLevel, cancellationToken);

        ~MySqlDatabaseConnection()
        {
            Dispose(disposing: false);
        }
    }
}
