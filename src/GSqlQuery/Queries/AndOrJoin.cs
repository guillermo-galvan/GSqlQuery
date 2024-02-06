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
    /// <typeparam name="TOptions">Options type</typeparam>
    /// <param name="queryBuilderWithWhere">Implementation of the IQueryBuilderWithWhere interface</param>
    /// <param name="formats">Formats</param>
    /// <exception cref="ArgumentException"></exception>
    internal class AndOrJoin<T1, T2, TJoin, TReturn, TOptions>(IQueryBuilderWithWhere<TReturn, TOptions> queryBuilderWithWhere, IFormats formats) : AndOrBase<TJoin, TReturn, TOptions>(queryBuilderWithWhere, formats)
        where T1 : class
        where T2 : class
        where TJoin : class
        where TReturn : IQuery<TJoin>
    {

        /// <summary>
        /// Build the sentence for the criteria
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>The search criteria</returns>
        public override IEnumerable<CriteriaDetail> BuildCriteria()
        {
            ClassOptions[] classOptions =
            [
                ClassOptionsFactory.GetClassOptions(typeof(T1)),
                ClassOptionsFactory.GetClassOptions(typeof(T2))
            ];

            Queue<CriteriaDetail> result = new Queue<CriteriaDetail>();

            foreach (ISearchCriteria x in _searchCriterias)
            {
                CriteriaDetail criteria = x.GetCriteria(Formats, classOptions.First(y => y.Table.Scheme == x.Table.Scheme && y.Table.Name == x.Table.Name).PropertyOptions);
                result.Enqueue(criteria);
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
    /// <typeparam name="TOptions">Options type</typeparam>
    /// <param name="queryBuilderWithWhere">Implementation of the IQueryBuilderWithWhere interface</param>
    /// <param name="formats">Formats</param>
    /// <exception cref="ArgumentException"></exception>
    internal class AndOrJoin<T1, T2, T3, TJoin, TReturn, TOptions>(IQueryBuilderWithWhere<TReturn, TOptions> queryBuilderWithWhere, IFormats formats) : AndOrBase<TJoin, TReturn, TOptions>(queryBuilderWithWhere,formats)
        where T1 : class
        where T2 : class
        where T3 : class
        where TJoin : class
        where TReturn : IQuery<TJoin>
    {

        /// <summary>
        /// Build the sentence for the criteria
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>The search criteria</returns>
        public override IEnumerable<CriteriaDetail> BuildCriteria()
        {
            ClassOptions[] classOptions =
            [
                ClassOptionsFactory.GetClassOptions(typeof(T1)),
                ClassOptionsFactory.GetClassOptions(typeof(T2)),
                ClassOptionsFactory.GetClassOptions(typeof(T3)),
            ];
            Queue<CriteriaDetail> result = new Queue<CriteriaDetail>();

            foreach (ISearchCriteria x in _searchCriterias)
            {
                CriteriaDetail criteria = x.GetCriteria(Formats, classOptions.First(y => y.Table.Scheme == x.Table.Scheme && y.Table.Name == x.Table.Name).PropertyOptions);
                result.Enqueue(criteria);
            }

            return result;
        }
    }

}