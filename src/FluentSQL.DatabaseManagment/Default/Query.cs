using FluentSQL.DatabaseManagement.Models;
using FluentSQL.Default;

namespace FluentSQL.DatabaseManagement.Default
{
    public abstract class Query<T, TDbConnection, TResult> : Query<T>, IQuery<T, TDbConnection, TResult> where T : class, new()
    {
        public IDatabaseManagement<TDbConnection> DatabaseManagment { get; }

        protected Query(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria,
            ConnectionOptions<TDbConnection> connectionOptions) :
            base(text, columns, criteria, connectionOptions?.Statements!)
        {
            connectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
            DatabaseManagment = connectionOptions.DatabaseManagment;
        }

        public abstract TResult Execute();

        public abstract TResult Execute(TDbConnection dbConnection);

        public abstract Task<TResult> ExecuteAsync(CancellationToken cancellationToken = default);

        public abstract Task<TResult> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default);
    }
}
