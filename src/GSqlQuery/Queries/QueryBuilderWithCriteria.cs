using GSqlQuery.Extensions;
using System.Collections.Generic;

namespace GSqlQuery
{
    public abstract class QueryBuilderWithCriteria<T, TReturn> : QueryBuilderBase<T, TReturn>, IQueryBuilderWithWhere<TReturn, IStatements>,
        IQueryBuilderWithWhere<T, TReturn, IStatements>
        where T : class
        where TReturn : IQuery<T>
    {
        protected IEnumerable<CriteriaDetail> _criteria = null;
        protected IAndOr<TReturn> _andOr;

        protected QueryBuilderWithCriteria(IStatements statements) : base(statements)
        { }

        public virtual IWhere<T, TReturn> Where()
        {
            _andOr = new AndOrBase<T, TReturn, IStatements>(this);
            return (IWhere<T, TReturn>)_andOr;
        }

        protected string GetCriteria()
        {
            return _andOr.GetCliteria(Options, ref _criteria);
        }

        IWhere<TReturn> IQueryBuilderWithWhere<TReturn, IStatements>.Where()
        {
            return Where();
        }
    }
}