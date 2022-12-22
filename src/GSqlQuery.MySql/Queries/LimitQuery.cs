using GSqlQuery.Runner;
using GSqlQuery.Runner.Extensions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.MySql
{
    public class LimitQuery<T> : Query<T> where T : class, new()
    {
        internal LimitQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail> criteria, IStatements statements) :
            base(text, columns, criteria, statements)
        {
        }
    }

    public class LimitQuery<T, TDbConnection> : Query<T, TDbConnection, IEnumerable<T>>, IQuery<T, TDbConnection, IEnumerable<T>>,
        IExecute<IEnumerable<T>, TDbConnection> where T : class, new()
    {
        internal LimitQuery(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail> criteria, ConnectionOptions<TDbConnection> connectionOptions)
            : base(text, columns, criteria, connectionOptions)
        {
        }

        public override IEnumerable<T> Execute()
        {
            return DatabaseManagment.ExecuteReader<T>(this, GetClassOptions().PropertyOptions,
                this.GetParameters<T, TDbConnection>(DatabaseManagment));
        }

        public override IEnumerable<T> Execute(TDbConnection dbConnection)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection));
            }
            return DatabaseManagment.ExecuteReader<T>(dbConnection, this, GetClassOptions().PropertyOptions,
                this.GetParameters<T, TDbConnection>(DatabaseManagment));
        }

        public override Task<IEnumerable<T>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return DatabaseManagment.ExecuteReaderAsync<T>(this, GetClassOptions().PropertyOptions,
                this.GetParameters<T, TDbConnection>(DatabaseManagment), cancellationToken);
        }

        public override Task<IEnumerable<T>> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection));
            }
            cancellationToken.ThrowIfCancellationRequested();
            return DatabaseManagment.ExecuteReaderAsync<T>(dbConnection, this, GetClassOptions().PropertyOptions,
                this.GetParameters<T, TDbConnection>(DatabaseManagment), cancellationToken);
        }
    }
}
