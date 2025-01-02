using System;

namespace GSqlQuery
{
    public class ConnectionOptions<TDbConnection> : QueryOptions
    {
        public IDatabaseManagement<TDbConnection> DatabaseManagement { get; }

        public ConnectionOptions(IFormats formats, IDatabaseManagement<TDbConnection> databaseManagement) : base(formats)
        {
            DatabaseManagement = databaseManagement ?? throw new ArgumentNullException(nameof(databaseManagement));
        }
    }
}