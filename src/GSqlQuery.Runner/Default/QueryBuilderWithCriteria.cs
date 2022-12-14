using GSqlQuery.Runner.Models;

namespace GSqlQuery.Runner.Default
{
    public abstract class QueryBuilderWithCriteria<T, TReturn, TDbConnection> : QueryBuilderWithCriteria<T, TReturn>,
        IQueryBuilderWithWhere<T, TReturn, TDbConnection>, IQueryBuilder<T, TReturn, TDbConnection>, IBuilder<TReturn>
        where T : class, new() where TReturn : IQuery
    {
        protected QueryBuilderWithCriteria(ConnectionOptions<TDbConnection> connectionOptions, QueryType queryType)
            : base(connectionOptions != null ? connectionOptions.Statements : null!, queryType)
        {
            ConnectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
        }

        public ConnectionOptions<TDbConnection> ConnectionOptions { get; }
    }
}
