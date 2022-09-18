using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    public abstract class QueryBuilderWithCriteria<T, TReturn> : QueryBuilderBase, IQueryBuilderWithWhere<T, TReturn> where T : class, new() where TReturn : IQuery<T>
    {
        protected IEnumerable<CriteriaDetail>? _criteria = null;
        protected IAndOr<T, TReturn>? _andOr;

        protected QueryBuilderWithCriteria(IStatements statements, QueryType queryType) :
            base(ClassOptionsFactory.GetClassOptions(typeof(T)).Table.GetTableName(statements), ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions,
                statements, queryType)
        { }

        public abstract TReturn Build();


        public abstract IWhere<T, TReturn> Where();

        protected string GetCriteria()
        {
           return _andOr!.GetCliteria(Statements, ref _criteria);
        }
    }

    public abstract class QueryBuilderWithCriteria<T, TReturn, TDbConnection, TResult> : QueryBuilderBase<TDbConnection>,
        IQueryBuilderWithWhere<T, TReturn, TDbConnection, TResult>, IQueryBuilder<T, TReturn, TDbConnection, TResult>, IBuilder<TReturn>
        where T : class, new() where TReturn : IQuery<T, TDbConnection, TResult>
    {
        protected IEnumerable<CriteriaDetail>? _criteria = null;
        protected IAndOr<T, TReturn, TDbConnection>? _andOr;

        protected QueryBuilderWithCriteria(ConnectionOptions<TDbConnection> connectionOptions, QueryType queryType) 
            : base(connectionOptions != null ? ClassOptionsFactory.GetClassOptions(typeof(T)).Table.GetTableName(connectionOptions.Statements): string.Empty, 
                  ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions,
                  connectionOptions!, queryType)
        {
        }

        public abstract TReturn Build();

        public abstract IWhere<T, TReturn, TDbConnection> Where();

        protected string GetCriteria()
        {
            return _andOr!.GetCliteria(ConnectionOptions.Statements, ref _criteria);
        }
    }
}
