using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery
{
    /// <summary>
    /// Query Builder With Criteria
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    public abstract class QueryBuilderWithCriteria<T, TReturn, TQueryOptions> : QueryBuilderBase<T, TReturn, TQueryOptions>, IQueryBuilderWithWhere<TReturn, TQueryOptions>,
        IQueryBuilderWithWhere<T, TReturn, TQueryOptions>
        where T : class
        where TReturn : IQuery<T, TQueryOptions>
        where TQueryOptions : QueryOptions
    {
        protected IEnumerable<CriteriaDetailCollection> _criteria = null;
        protected IAndOr<TReturn> _andOr;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="formats"></param>
        protected QueryBuilderWithCriteria(TQueryOptions queryOptions, PropertyOptionsCollection columns = null) : base(queryOptions, columns)
        { }

        /// <summary>
        /// Method to add the Where statement
        /// </summary>
        /// <returns>IWhere&lt;<typeparamref name="T"/>, <typeparamref name="TReturn"/>&gt;</returns>
        public virtual IWhere<T, TReturn, TQueryOptions> Where()
        {
            _andOr = new AndOrBase<T, TReturn, TQueryOptions>(this, QueryOptions, _classOptions);
            return (IWhere<T, TReturn, TQueryOptions>)_andOr;
        }

        /// <summary>
        /// Get critetia text
        /// </summary>
        /// <returns></returns>
        protected string GetCriteria()
        {
            _criteria ??= _andOr.BuildCriteria();
            IEnumerable<string> queryParts = _criteria.Select(x => x.QueryPart);
            return string.Join(" ", queryParts);
        }

        /// <summary>
        /// Method to add the Where statement
        /// </summary>
        /// <returns>IWhere&lt;<typeparamref name="TReturn"/>&gt;</returns>
        IWhere<TReturn> IQueryBuilderWithWhere<TReturn, TQueryOptions>.Where()
        {
            return Where();
        }
    }
}