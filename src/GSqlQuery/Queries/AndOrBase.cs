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
    public class AndOrBase<T, TReturn, TOptions>(IQueryBuilderWithWhere<TReturn, TOptions> queryBuilderWithWhere, IFormats formats, bool isColumns = true) : WhereBase<TReturn>(), IAndOr<TReturn>, ISearchCriteriaBuilder<TReturn>, IAndOr<T, TReturn>, IWhere<T, TReturn>
        where TReturn : IQuery<T>
        where T : class
    {
        protected readonly Queue<ISearchCriteria> _searchCriterias = new Queue<ISearchCriteria>();
        internal readonly IQueryBuilderWithWhere<TReturn, TOptions> _queryBuilderWithWhere = queryBuilderWithWhere ?? throw new ArgumentException(nameof(queryBuilderWithWhere));

        protected IEnumerable<PropertyOptions> Columns { get; set; } = isColumns ? ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions : Enumerable.Empty<PropertyOptions>();

        public IFormats Formats { get; } = formats ?? throw new ArgumentException(nameof(formats));

        /// <summary>
        /// Add search criteria
        /// </summary>
        /// <param name="criteria"></param>
        public void Add(ISearchCriteria criteria)
        {
            criteria.NullValidate(ErrorMessages.ParameterNotNull, nameof(criteria));
            _searchCriterias.Enqueue(criteria);
        }

        /// <summary>
        /// Build the sentence for the criteria
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>The search criteria</returns>
        public virtual IEnumerable<CriteriaDetail> BuildCriteria(IFormats formats)
        {
            return _searchCriterias.Select(x => x.GetCriteria(formats, Columns)).ToArray();
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