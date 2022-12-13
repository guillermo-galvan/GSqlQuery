using FluentSQL.DataBase;
using System.Data.Common;

namespace FluentSQL.Sqlite
{
    public class SqliteDatabaseTransaction : Transaction
    {
        public SqliteDatabaseTransaction(SqliteDatabaseConnection connection, DbTransaction transaction) : base(connection, transaction)
        {
        }

        public SqliteDatabaseConnection Connection => (SqliteDatabaseConnection)_connection;

        public DbTransaction Transaction => _transaction;

        ~SqliteDatabaseTransaction()
        {
            Dispose(disposing: false);
        }
    }
}
