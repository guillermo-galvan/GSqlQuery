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
    internal class AndOrJoin<T1, T2, TJoin, TReturn, TOptions> : AndOrBase<TJoin, TReturn, TOptions>
        where T1 : class
        where T2 : class
        where TJoin : class
        where TReturn : IQuery<TJoin>
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="queryBuilderWithWhere">Implementation of the IQueryBuilderWithWhere interface</param>
        /// <exception cref="ArgumentException"></exception>
        public AndOrJoin(IQueryBuilderWithWhere<TReturn, TOptions> queryBuilderWithWhere) :
            base(queryBuilderWithWhere, false)
        {
        }

        /// <summary>
        /// Build the sentence for the criteria
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>The search criteria</returns>
        public override IEnumerable<CriteriaDetail> BuildCriteria(IFormats formats)
        {
            ClassOptions[] classOptions = new ClassOptions[]
            {
                ClassOptionsFactory.GetClassOptions(typeof(T1)),
                ClassOptionsFactory.GetClassOptions(typeof(T2))
            };

            return _searchCriterias.Select(x => x.GetCriteria(formats,
                classOptions.First(y => y.Table.Scheme == x.Table.Scheme && y.Table.Name == x.Table.Name).PropertyOptions)).ToArray();
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
    internal class AndOrJoin<T1, T2, T3, TJoin, TReturn, TOptions> : AndOrBase<TJoin, TReturn, TOptions>
        where T1 : class
        where T2 : class
        where T3 : class
        where TJoin : class
        where TReturn : IQuery<TJoin>
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="queryBuilderWithWhere">Implementation of the IQueryBuilderWithWhere interface</param>
        /// <exception cref="ArgumentException"></exception>
        public AndOrJoin(IQueryBuilderWithWhere<TReturn, TOptions> queryBuilderWithWhere)
            : base(queryBuilderWithWhere, false)
        {
        }

        /// <summary>
        /// Build the sentence for the criteria
        /// </summary>
        /// <param name="formats">Formats</param>
        /// <returns>The search criteria</returns>
        public override IEnumerable<CriteriaDetail> BuildCriteria(IFormats formats)
        {
            ClassOptions[] classOptions = new ClassOptions[]
            {
                ClassOptionsFactory.GetClassOptions(typeof(T1)),
                ClassOptionsFactory.GetClassOptions(typeof(T2)),
                ClassOptionsFactory.GetClassOptions(typeof(T3)),
            };

            return _searchCriterias.Select(x =>
                x.GetCriteria(formats, classOptions.First(y => y.Table.Scheme == x.Table.Scheme && y.Table.Name == x.Table.Name).PropertyOptions)).ToArray();
        }
    }

}