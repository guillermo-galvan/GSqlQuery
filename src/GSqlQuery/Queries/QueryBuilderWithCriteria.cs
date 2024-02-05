using GSqlQuery.Extensions;
using System.Collections.Generic;

namespace GSqlQuery
{
    /// <summary>
    /// Query Builder With Criteria
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    public abstract class QueryBuilderWithCriteria<T, TReturn> : QueryBuilderBase<T, TReturn>, IQueryBuilderWithWhere<TReturn, IFormats>,
        IQueryBuilderWithWhere<T, TReturn, IFormats>
        where T : class
        where TReturn : IQuery<T>
    {
        protected IEnumerable<CriteriaDetail> _criteria = null;
        protected IAndOr<TReturn> _andOr;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="formats"></param>
        protected QueryBuilderWithCriteria(IFormats formats) : base(formats)
        { }

        /// <summary>
        /// Method to add the Where statement
        /// </summary>
        /// <returns>IWhere&lt;<typeparamref name="T"/>, <typeparamref name="TReturn"/>&gt;</returns>
        public virtual IWhere<T, TReturn> Where()
        {
            _andOr = new AndOrBase<T, TReturn, IFormats>(this, Options, _classOptions);
            return (IWhere<T, TReturn>)_andOr;
        }

        /// <summary>
        /// Get critetia text
        /// </summary>
        /// <returns></returns>
        protected string GetCriteria()
        {
            return IAndOrExtension.GetCliteria(_andOr,Options, ref _criteria);
        }

        /// <summary>
        /// Method to add the Where statement
        /// </summary>
        /// <returns>IWhere&lt;<typeparamref name="TReturn"/>&gt;</returns>
        IWhere<TReturn> IQueryBuilderWithWhere<TReturn, IFormats>.Where()
        {
            return Where();
        }
    }
}