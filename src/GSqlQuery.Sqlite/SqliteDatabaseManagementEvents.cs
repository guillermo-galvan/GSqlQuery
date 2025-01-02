using GSqlQuery.Runner;
using GSqlQuery.Runner.TypeHandles;
using Microsoft.Data.Sqlite;
using System;

namespace GSqlQuery.Sqlite
{
    public class SqliteDatabaseManagementEvents : DatabaseManagementEvents
    {
        public static readonly TypeHandleCollection<SqliteDataReader> TypeHandleCollection = TypeHandleCollection<SqliteDataReader>.Instance;

        protected override ITypeHandler<TDbDataReader> GetTypeHandler<TDbDataReader>(Type property)
        {
            return (ITypeHandler<TDbDataReader>)TypeHandleCollection.GetTypeHandler(property);
        }
    }
}