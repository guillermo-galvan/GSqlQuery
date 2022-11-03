using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    public abstract class QueryBuilderWithCriteria<T, TReturn> : QueryBuilderBase<T, TReturn>, IQueryBuilderWithWhere<T, TReturn> where T : class, new() where TReturn : IQuery
    {
        protected IEnumerable<CriteriaDetail>? _criteria = null;
        protected IAndOr<T, TReturn>? _andOr;
        
        protected QueryBuilderWithCriteria(IStatements statements, QueryType queryType): base(statements, queryType)
        {}

        public abstract IWhere<T, TReturn> Where();

        protected string GetCriteria()
        {
           return _andOr!.GetCliteria(Statements, ref _criteria);
        }
    }

    public abstract class QueryBuilderWithCriteria<T, TReturn, TDbConnection> : QueryBuilderWithCriteria<T, TReturn>,
        IQueryBuilderWithWhere<T, TReturn, TDbConnection>, IQueryBuilder<T, TReturn, TDbConnection>, IBuilder<TReturn>
        where T : class, new() where TReturn : IQuery
    {
        protected QueryBuilderWithCriteria(ConnectionOptions<TDbConnection> connectionOptions, QueryType queryType) 
            : base(connectionOptions != null ? connectionOptions.Statements : null!,queryType)
        {
            ConnectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
        }

        public ConnectionOptions<TDbConnection> ConnectionOptions { get; }
    }
}
