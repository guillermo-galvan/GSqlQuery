using System;

namespace GSqlQuery.Runner
{
    public abstract class QueryBuilderWithCriteria<T, TReturn, TDbConnection> : QueryBuilderWithCriteria<T, TReturn>,
        IQueryBuilderWithWhereRunner<T, TReturn, TDbConnection>, IQueryBuilderRunner<T, TReturn, TDbConnection>, IBuilder<TReturn>
        where T : class, new() where TReturn : IQuery
    {
        protected QueryBuilderWithCriteria(ConnectionOptions<TDbConnection> connectionOptions)
            : base(connectionOptions != null ? connectionOptions.Statements : null)
        {
            ConnectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
        }

        public ConnectionOptions<TDbConnection> ConnectionOptions { get; }
    }
}
