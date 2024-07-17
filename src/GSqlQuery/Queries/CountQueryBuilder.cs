using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Count Query Builder
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TOptions">Options type</typeparam>
    /// <typeparam name="TSelectQuery">Select Query</typeparam>
    internal abstract class CountQueryBuilder<T, TReturn, TOptions, TSelectQuery> : QueryBuilderWithCriteria<T, TReturn, TOptions>
        where T : class
        where TReturn : IQuery<T, TOptions>
        where TSelectQuery : IQuery<T, TOptions>
        where TOptions : QueryOptions
    {
        protected readonly IQueryBuilder<TSelectQuery, TOptions> _queryBuilder;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="queryBuilder">Implementation of the IQueryBuilderWithWhere interface</param>
        /// <param name="formats">Formats</param>
        public CountQueryBuilder(IQueryBuilderWithWhere<TSelectQuery, TOptions> queryBuilder, TOptions queryOptions) : base(queryOptions)
        {
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
        }

        /// <summary>
        /// Create query
        /// </summary>
        /// <returns>Query text</returns>
        internal string CreateQuery()
        {
            IQuery<T, TOptions> selectQuery = _queryBuilder.Build();
            IEnumerable<string> columnsName = selectQuery.Columns.Values.Select(x => QueryOptions.Formats.GetColumnName(_tableName, x.ColumnAttribute, QueryType.Read));
            string columns = string.Join(",", columnsName);
            columns = "COUNT({0})".Replace("{0}", columns);

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
    }

    /// <summary>
    /// Count Query Builder
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    /// <param name="queryBuilder"></param>
    internal class CountQueryBuilder<T>(IQueryBuilderWithWhere<SelectQuery<T>, QueryOptions> queryBuilder) :
        CountQueryBuilder<T, CountQuery<T>, QueryOptions, SelectQuery<T>>(queryBuilder, queryBuilder.QueryOptions) where T : class
    {
        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Count Query</returns>
        public override CountQuery<T> Build()
        {
            string query = CreateQuery();
            return new CountQuery<T>(query, Columns, _criteria, _queryBuilder.QueryOptions);
        }
    }
}