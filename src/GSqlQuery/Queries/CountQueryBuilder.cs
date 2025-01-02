using GSqlQuery.Cache;
using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Count Query Builder
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TQueryOptions">Options type</typeparam>
    /// <typeparam name="TSelectQuery">Select Query</typeparam>
    /// <remarks>
    /// Class constructor
    /// </remarks>
    /// <param name="queryBuilder">Implementation of the IQueryBuilderWithWhere interface</param>
    /// <param name="formats">Formats</param>
    internal abstract class CountQueryBuilder<T, TReturn, TQueryOptions, TSelectQuery>(IQueryBuilderWithWhere<TSelectQuery, TQueryOptions> queryBuilder, TQueryOptions queryOptions) : QueryBuilderWithCriteria<T, TReturn, TQueryOptions>(queryOptions)
        where T : class
        where TReturn : IQuery<T, TQueryOptions>
        where TSelectQuery : IQuery<T, TQueryOptions>
        where TQueryOptions : QueryOptions
    {
        protected readonly IQueryBuilder<TSelectQuery, TQueryOptions> _queryBuilder = queryBuilder;

        /// <summary>
        /// Create query
        /// </summary>
        /// <returns>Query text</returns>
        internal string CreateQueryText()
        {
            IQuery<T, TQueryOptions> selectQuery = _queryBuilder.Build();
            IEnumerable<string> columnsName = selectQuery.Columns.Values.Select(x => x.FormatColumnName.GetColumnName(QueryOptions.Formats, QueryType.Read));
            string columns = string.Join(",", columnsName);
            columns = "COUNT({0})".Replace("{0}", columns);
            Columns = selectQuery.Columns;

            if (_andOr == null)
            {
                return ConstFormat.SELECT.Replace("{0}", columns).Replace("{1}", _tableName);
            }
            else
            {
                string criteria = GetCriteria();
                return ConstFormat.SELECTWHERE.Replace("{0}", columns).Replace("{1}", _tableName).Replace("{2}", criteria);
            }
        }

        public override TReturn Build()
        {
            return CacheQueryBuilderExtension.CreateCountSelectQuery<T, TReturn, TQueryOptions, TSelectQuery>(QueryOptions, _queryBuilder, _andOr, CreateQuery, GetQuery);
        }

        public abstract TReturn GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, TQueryOptions queryOptions);

        public TReturn CreateQuery()
        {
            string text = CreateQueryText();
            TReturn result = GetQuery(text, Columns, _criteria, QueryOptions);
            return result;
        }
    }

    /// <summary>
    /// Count Query Builder
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    /// <param name="queryBuilder"></param>
    internal class CountQueryBuilder<T>(IQueryBuilderWithWhere<SelectQuery<T>, QueryOptions> queryBuilder) :
        CountQueryBuilder<T, CountQuery<T>, QueryOptions, SelectQuery<T>>(queryBuilder, queryBuilder.QueryOptions) where T : class
    {
        public override CountQuery<T> GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, QueryOptions queryOptions)
        {
            return new CountQuery<T>(text, _classOptions.FormatTableName.Table, columns, criteria, queryOptions);
        }
    }
}