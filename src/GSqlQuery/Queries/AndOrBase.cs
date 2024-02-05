using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery
{
    /// <summary>
    /// Generate the search criteria
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TOptions">Options type</typeparam>
    /// <param name="queryBuilderWithWhere">Implementation of the IQueryBuilderWithWhere interface</param>
    /// <param name="formats">Formats</param>
    /// <param name="isColumns">Determines whether to take the columns from <typeparamref name="T"/></param>
    /// <exception cref="ArgumentException"></exception>
    public class AndOrBase<T, TReturn, TOptions> : 
        WhereBase<TReturn>, IAndOr<TReturn>, ISearchCriteriaBuilder<TReturn>, IAndOr<T, TReturn>, IWhere<T, TReturn>
        where TReturn : IQuery<T>
        where T : class
    {
        protected readonly Queue<ISearchCriteria> _searchCriterias = new Queue<ISearchCriteria>();
        internal readonly IQueryBuilderWithWhere<TReturn, TOptions> _queryBuilderWithWhere;

        protected IEnumerable<PropertyOptions> Columns { get; set; }

        public IFormats Formats { get; }

        public AndOrBase(IQueryBuilderWithWhere<TReturn, TOptions> queryBuilderWithWhere, IFormats formats) : base()
        {
            _queryBuilderWithWhere = queryBuilderWithWhere ?? throw new ArgumentNullException(nameof(queryBuilderWithWhere));
            Columns = Enumerable.Empty<PropertyOptions>();
            Formats = formats ?? throw new ArgumentNullException(nameof(formats));
        }

        public AndOrBase(IQueryBuilderWithWhere<TReturn, TOptions> queryBuilderWithWhere, IFormats formats, ClassOptions classOptions) : base()
        {
            _queryBuilderWithWhere = queryBuilderWithWhere ?? throw new ArgumentNullException(nameof(queryBuilderWithWhere));
            Columns = classOptions?.PropertyOptions ?? throw new ArgumentNullException(nameof(classOptions));
            Formats = formats ?? throw new ArgumentNullException(nameof(formats));
        }

        /// <summary>
        /// Add search criteria
        /// </summary>
        /// <param name="criteria"></param>
        public void Add(ISearchCriteria criteria)
        {
            _searchCriterias.Enqueue(criteria ?? throw new ArgumentNullException(nameof(criteria), ErrorMessages.ParameterNotNull));
        }

        /// <summary>
        /// Build the sentence for the criteria
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>The search criteria</returns>
        public virtual IEnumerable<CriteriaDetail> BuildCriteria()
        {
            CriteriaDetail[] result = new CriteriaDetail[_searchCriterias.Count];
            int count = 0;

            foreach (ISearchCriteria item in _searchCriterias)
            {
                result[count++] = item.GetCriteria(Formats, Columns);
            }

            return result;
        }
        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>The query</returns>
        public virtual TReturn Build()
        {
            return _queryBuilderWithWhere.Build();
        }
    }
}