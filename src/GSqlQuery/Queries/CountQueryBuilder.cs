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
    internal abstract class CountQueryBuilder<T, TReturn, TOptions, TSelectQuery> : QueryBuilderWithCriteria<T, TReturn>
        where T : class
        where TReturn : CountQuery<T>
        where TSelectQuery : SelectQuery<T>
    {
        protected readonly IQueryBuilder<TSelectQuery, TOptions> _queryBuilder;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="queryBuilder">Implementation of the IQueryBuilderWithWhere interface</param>
        /// <param name="formats">Formats</param>
        public CountQueryBuilder(IQueryBuilderWithWhere<TSelectQuery, TOptions> queryBuilder, IFormats formats) : base(formats)
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
            SelectQuery<T> selectQuery = _queryBuilder.Build();
            IEnumerable<string> columnsName = selectQuery.Columns.Select(x => Options.GetColumnName(_tableName, x.ColumnAttribute, QueryType.Read));
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
    internal class CountQueryBuilder<T>(IQueryBuilderWithWhere<SelectQuery<T>, IFormats> queryBuilder) : 
        CountQueryBuilder<T, CountQuery<T>, IFormats, SelectQuery<T>>(queryBuilder, queryBuilder.Options) where T : class
    {
        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Count Query</returns>
        public override CountQuery<T> Build()
        {
            string query = CreateQuery();
            return new CountQuery<T>(query, Columns, _criteria, _queryBuilder.Options);
        }
    }
}