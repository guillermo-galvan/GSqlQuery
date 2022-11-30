using FluentSQL.Extensions;

namespace FluentSQL.Default
{
    public abstract class QueryBuilderWithCriteria<T, TReturn> : QueryBuilderBase<T, TReturn>, IQueryBuilderWithWhere<T, TReturn> where T : class, new() where TReturn : IQuery
    {
        protected IEnumerable<CriteriaDetail>? _criteria = null;
        protected IAndOr<T,TReturn>? _andOr;
        
        protected QueryBuilderWithCriteria(IStatements statements, QueryType queryType): base(statements, queryType)
        {}

        public abstract IWhere<T,TReturn> Where();

        protected string GetCriteria()
        {
           return _andOr!.GetCliteria(Statements, ref _criteria);
        }
    }
}
