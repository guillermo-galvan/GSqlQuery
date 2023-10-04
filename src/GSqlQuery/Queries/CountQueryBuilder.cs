using GSqlQuery.Extensions;
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
            string result = string.Empty;
            var selectQuery = _queryBuilder.Build();
            Columns = _queryBuilder.Columns;

            if (_andOr == null)
            {
                result = string.Format(ConstFormat.SELECT,
                    $"COUNT({string.Join(",", selectQuery.Columns.Select(x => x.ColumnAttribute.GetColumnName(_tableName, Options, QueryType.Read)))})",
                    _tableName);
            }
            else
            {
                result = string.Format(ConstFormat.SELECTWHERE,
                    $"COUNT({string.Join(",", selectQuery.Columns.Select(x => x.ColumnAttribute.GetColumnName(_tableName, Options, QueryType.Read)))})",
                    _tableName, GetCriteria());
            }

            return result;
        }
    }

    /// <summary>
    /// Count Query Builder
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    internal class CountQueryBuilder<T> : CountQueryBuilder<T, CountQuery<T>, IFormats, SelectQuery<T>> where T : class
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="queryBuilder"></param>
        public CountQueryBuilder(IQueryBuilderWithWhere<SelectQuery<T>, IFormats> queryBuilder)
            : base(queryBuilder, queryBuilder.Options)
        { }

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Count Query</returns>
        public override CountQuery<T> Build()
        {
            var query = CreateQuery();
            return new CountQuery<T>(query, Columns, _criteria, _queryBuilder.Options);
        }
    }
}