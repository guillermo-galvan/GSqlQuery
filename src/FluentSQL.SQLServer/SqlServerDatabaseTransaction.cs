using FluentSQL.DataBase;
using System.Data.Common;

namespace FluentSQL.SQLServer
{
    public class SqlServerDatabaseTransaction : Transaction
    {
        public SqlServerDatabaseTransaction(SqlServerDatabaseConnection connection, DbTransaction transaction) : base(connection, transaction)
        {
        }

        public SqlServerDatabaseConnection Connection => (SqlServerDatabaseConnection)_connection;

        public DbTransaction Transaction => _transaction;

        ~SqlServerDatabaseTransaction()
        {
            Dispose(disposing: false);
        }
    }
}
