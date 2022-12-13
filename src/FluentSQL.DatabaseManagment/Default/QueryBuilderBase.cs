using FluentSQL.DatabaseManagement.Models;
using FluentSQL.Default;

namespace FluentSQL.DatabaseManagement.Default
{
    public abstract class QueryBuilderBase<T, TReturn, TDbConnection> : QueryBuilderBase<T, TReturn>, IQueryBuilder<T, TReturn>, IBuilder<TReturn>
        where T : class, new() where TReturn : IQuery
    {
        public ConnectionOptions<TDbConnection> ConnectionOptions { get; }

        public QueryBuilderBase(ConnectionOptions<TDbConnection> connectionOptions, QueryType queryType)
            : base(connectionOptions != null ? connectionOptions.Statements : null!, queryType)
        {
            ConnectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
        }
    }
}
