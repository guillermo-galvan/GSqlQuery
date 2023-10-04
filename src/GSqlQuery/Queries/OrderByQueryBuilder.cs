using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Order By Query Builder
    /// </summary>
    internal interface IOrderByQueryBuilder
    {
        void AddOrderBy(IEnumerable<string> selectMember, OrderBy orderBy);
    }

    /// <summary>
    /// Order By Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TOptions">Options Type</typeparam>
    /// <typeparam name="TSelectQuery">Select query</typeparam>
    internal abstract class OrderByQueryBuilder<T, TReturn, TOptions, TSelectQuery> : QueryBuilderWithCriteria<T, TReturn>, IOrderByQueryBuilder
        where T : class
        where TReturn : OrderByQuery<T>
        where TSelectQuery : SelectQuery<T>
    {
        protected readonly IQueryBuilderWithWhere<TSelectQuery, TOptions> _queryBuilder;
        protected readonly IAndOr<T, TSelectQuery> _andorBuilder;
        protected readonly Queue<ColumnsOrderBy> _columnsByOrderBy;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="selectMember">>Name of properties to search</param>
        /// <param name="orderBy">Order By Type</param>
        /// <param name="queryBuilder">Implementation of the IQueryBuilderWithWhere interface</param>
        /// <param name="formats">Formats</param>
        protected OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
            IQueryBuilderWithWhere<T, TSelectQuery, TOptions> queryBuilder, IFormats formats) : base(formats)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order By Type</param>
        /// <param name="andOr">Implementation of the IAndOr interface</param>
        /// <param name="formats">Formats</param>
        protected OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
           IAndOr<T, TSelectQuery> andOr, IFormats formats) : base(formats)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
            _andorBuilder = andOr;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        /// <summary>
        /// Add columns to the 'Order By' statement
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">>Order By Type</param>
        public void AddOrderBy(IEnumerable<string> selectMember, OrderBy orderBy)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember), orderBy));
        }

        /// <summary>
        /// Create query
        /// </summary>
        /// <param name="columns">Columns</param>
        /// <param name="criteria">Criterias</param>
        /// <returns></returns>
        internal string CreateQuery(out IEnumerable<PropertyOptions> columns, out IEnumerable<CriteriaDetail> criteria)
        {
            TSelectQuery selectQuery = _queryBuilder == null ? _andorBuilder.Build() : _queryBuilder.Build();
            string columnsOrderby =
                string.Join(",", _columnsByOrderBy.Select(x => $"{string.Join(",", x.Columns.Select(y => y.ColumnAttribute.GetColumnName(_tableName, Options, QueryType.Read)))} {x.OrderBy}"));

            columns = selectQuery.Columns;
            criteria = selectQuery.Criteria;

            string result = string.Empty;

            if (selectQuery.Criteria == null || !selectQuery.Criteria.Any())
            {
                result = string.Format(ConstFormat.SELECTORDERBY,
                     string.Join(",", columns.Select(x => x.ColumnAttribute.GetColumnName(_tableName, Options, QueryType.Read))),
                     _tableName,
                     columnsOrderby);
            }
            else
            {
                result = string.Format(ConstFormat.SELECTWHEREORDERBY,
                     string.Join(",", columns.Select(x => x.ColumnAttribute.GetColumnName(_tableName, Options, QueryType.Read))),
                     _tableName, string.Join(" ", selectQuery.Criteria.Select(x => x.QueryPart)), columnsOrderby);
            }

            return result;
        }
    }

    /// <summary>
    /// Order By Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class OrderByQueryBuilder<T> : OrderByQueryBuilder<T, OrderByQuery<T>, IFormats, SelectQuery<T>>,
        IQueryBuilder<OrderByQuery<T>, IFormats>
        where T : class
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order By Type</param>
        /// <param name="queryBuilder">Implementation of the IQueryBuilderWithWhere interface</param>
        public OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
            IQueryBuilderWithWhere<T, SelectQuery<T>, IFormats> queryBuilder)
            : base(selectMember, orderBy, queryBuilder, queryBuilder.Options)
        { }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order By Type</param>
        /// <param name="andOr">Implementation of the IAndOr interface</param>
        /// <param name="formats">Formats</param>
        public OrderByQueryBuilder(IEnumerable<string> selectMember, OrderBy orderBy,
           IAndOr<T, SelectQuery<T>> andOr, IFormats formats)
           : base(selectMember, orderBy, andOr, formats)
        { }

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Order by Query</returns>
        public override OrderByQuery<T> Build()
        {
            var query = CreateQuery(out IEnumerable<PropertyOptions> columns, out IEnumerable<CriteriaDetail> criteria);
            return new OrderByQuery<T>(query, columns, criteria, Options);
        }
    }
}