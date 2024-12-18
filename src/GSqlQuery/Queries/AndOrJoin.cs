using GSqlQuery.SearchCriteria;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Generate the search criteria
    /// </summary>
    /// <typeparam name="T1">Type of class</typeparam>
    /// <typeparam name="T2">Type of class</typeparam>
    /// <typeparam name="TJoin">Type of class</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TQueryOptions">Options type</typeparam>
    /// <param name="queryBuilderWithWhere">Implementation of the IQueryBuilderWithWhere interface</param>
    /// <param name="formats">Formats</param>
    /// <exception cref="ArgumentException"></exception>
    internal class AndOrJoin<T1, T2, TJoin, TReturn, TQueryOptions>(IQueryBuilderWithWhere<TReturn, TQueryOptions> queryBuilderWithWhere, TQueryOptions queryOptions) :
        AndOrBase<TJoin, TReturn, TQueryOptions>(queryBuilderWithWhere, queryOptions)
        where T1 : class
        where T2 : class
        where TJoin : class
        where TReturn : IQuery<TJoin, TQueryOptions>
        where TQueryOptions : QueryOptions
    {

        /// <summary>
        /// Build the sentence for the criteria
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>The search criteria</returns>
        public override IEnumerable<CriteriaDetailCollection> Create()
        {
            ClassOptions[] classOptions =
            [
                ClassOptionsFactory.GetClassOptions(typeof(T1)),
                ClassOptionsFactory.GetClassOptions(typeof(T2))
            ];

            List<CriteriaDetailCollection> result = [];
            uint parameterId = 0;

            foreach (ISearchCriteria x in _searchCriterias)
            {
                CriteriaDetailCollection criteria = x.GetCriteria(ref parameterId);
                result.Add(criteria);
            }

            return result;
        }
    }

    /// <summary>
    /// Generate the search criteria
    /// </summary>
    /// <typeparam name="T1">Type of class</typeparam>
    /// <typeparam name="T2">Type of class</typeparam>
    /// <typeparam name="T3">Type of class</typeparam>
    /// <typeparam name="TJoin">Type of class</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TQueryOptions">Options type</typeparam>
    /// <param name="queryBuilderWithWhere">Implementation of the IQueryBuilderWithWhere interface</param>
    /// <param name="formats">Formats</param>
    /// <exception cref="ArgumentException"></exception>
    internal class AndOrJoin<T1, T2, T3, TJoin, TReturn, TQueryOptions>(IQueryBuilderWithWhere<TReturn, TQueryOptions> queryBuilderWithWhere, TQueryOptions queryOptions) : AndOrBase<TJoin, TReturn, TQueryOptions>(queryBuilderWithWhere, queryOptions)
        where T1 : class
        where T2 : class
        where T3 : class
        where TJoin : class
        where TReturn : IQuery<TJoin, TQueryOptions>
        where TQueryOptions : QueryOptions
    {

        /// <summary>
        /// Build the sentence for the criteria
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>The search criteria</returns>
        public override IEnumerable<CriteriaDetailCollection> Create()
        {
            ClassOptions[] classOptions =
            [
                ClassOptionsFactory.GetClassOptions(typeof(T1)),
                ClassOptionsFactory.GetClassOptions(typeof(T2)),
                ClassOptionsFactory.GetClassOptions(typeof(T3)),
            ];
            List<CriteriaDetailCollection> result = [];
            uint parameterId = 0;

            foreach (ISearchCriteria x in _searchCriterias)
            {
                CriteriaDetailCollection criteria = x.GetCriteria(ref parameterId);
                result.Add(criteria);
            }

            return result;
        }
    }

}