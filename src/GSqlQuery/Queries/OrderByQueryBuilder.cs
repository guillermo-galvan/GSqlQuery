using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Order By Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TOptions">Options Type</typeparam>
    /// <typeparam name="TSelectQuery">Select query</typeparam>
    internal abstract class OrderByQueryBuilder<T, TReturn, TQueryOptions, TSelectQuery> : QueryBuilderWithCriteria<T, TReturn, TQueryOptions>, IOrderByQueryBuilder
        where T : class
        where TReturn : IQuery<T, TQueryOptions>
        where TSelectQuery : IQuery<T, TQueryOptions>
        where TQueryOptions : QueryOptions
    {
        protected readonly IQueryBuilderWithWhere<TSelectQuery, TQueryOptions> _queryBuilder;
        protected readonly IAndOr<T, TSelectQuery, TQueryOptions> _andorBuilder = null;
        protected readonly Queue<ColumnsOrderBy> _columnsByOrderBy;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="classOptionsTupla">>Name of properties to search</param>
        /// <param name="orderBy">Order By Type</param>
        /// <param name="queryBuilder">Implementation of the IQueryBuilderWithWhere interface</param>
        /// <param name="queryOptions">Formats</param>
        protected OrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> classOptionsTupla, OrderBy orderBy,
            IQueryBuilderWithWhere<T, TSelectQuery, TQueryOptions> queryBuilder, TQueryOptions queryOptions) : base(queryOptions)
        {
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            PropertyOptionsCollection properties = ExpressionExtension.GetPropertyQuery(classOptionsTupla);
            ColumnsOrderBy columnsOrderBy = new ColumnsOrderBy(properties, orderBy);
            _columnsByOrderBy.Enqueue(columnsOrderBy);
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="classOptionsTupla">Name of properties to search</param>
        /// <param name="orderBy">Order By Type</param>
        /// <param name="andOr">Implementation of the IAndOr interface</param>
        protected OrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> classOptionsTupla, OrderBy orderBy,
           IAndOr<T, TSelectQuery, TQueryOptions> andOr) : base(andOr.QueryOptions)
        {
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            PropertyOptionsCollection properties = ExpressionExtension.GetPropertyQuery(classOptionsTupla);
            ColumnsOrderBy columnsOrderBy = new ColumnsOrderBy(properties, orderBy);
            _columnsByOrderBy.Enqueue(columnsOrderBy);
            _andorBuilder = andOr;
            Columns = new PropertyOptionsCollection([]);
        }

        /// <summary>
        /// Add columns to the 'Order By' statement
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">>Order By Type</param>
        public void AddOrderBy(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy)
        {
            IEnumerable<string> columnsName = selectMember.MemberInfo.Select(x => x.Name);
            PropertyOptionsCollection propertyInfos = ExpressionExtension.GetPropertyQuery(_classOptions, columnsName);
            ColumnsOrderBy columnsOrderBy = new ColumnsOrderBy(propertyInfos, orderBy);
            _columnsByOrderBy.Enqueue(columnsOrderBy);
        }

        /// <summary>
        /// Create query
        /// </summary>
        /// <param name="columns">Columns</param>
        /// <param name="criteria">Criterias</param>
        /// <returns></returns>
        internal string CreateQuery(out PropertyOptionsCollection columns, out IEnumerable<CriteriaDetail> criteria)
        {
            TSelectQuery selectQuery = _andorBuilder != null ? _andorBuilder.Build() : _queryBuilder.Build();
            Queue<string> parts = new Queue<string>();

            foreach (ColumnsOrderBy x in _columnsByOrderBy)
            {
                IEnumerable<string> names = x.Columns.Values.Select(y => QueryOptions.Formats.GetColumnName(_tableName, y.ColumnAttribute, QueryType.Read));
                string columnsName = string.Join(",", names);
                string orderByQuery = "{0} {1}".Replace("{0}", columnsName).Replace("{1}", x.OrderBy.ToString());
                parts.Enqueue(orderByQuery);
            }

            string columnsOrderby = string.Join(",", parts);
            columns = selectQuery.Columns;
            criteria = selectQuery.Criteria;

            IEnumerable<string> ColumnNames = columns.Values.Select(x => QueryOptions.Formats.GetColumnName(_tableName, x.ColumnAttribute, QueryType.Read));
            string resultColumnsName = string.Join(",", ColumnNames);

            if (selectQuery.Criteria == null || !selectQuery.Criteria.Any())
            {
                return ConstFormat.SELECTORDERBY.Replace("{0}", resultColumnsName).Replace("{1}", _tableName).Replace("{2}", columnsOrderby);
            }
            else
            {
                string resultWhere = string.Join(" ", selectQuery.Criteria.Select(x => x.QueryPart));
                return ConstFormat.SELECTWHEREORDERBY.Replace("{0}", resultColumnsName).Replace("{1}", _tableName)
                                                     .Replace("{2}", resultWhere).Replace("{3}", columnsOrderby);
            }
        }
    }

    /// <summary>
    /// Order By Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class OrderByQueryBuilder<T> : OrderByQueryBuilder<T, OrderByQuery<T>, QueryOptions, SelectQuery<T>>,
        IQueryBuilder<OrderByQuery<T>, QueryOptions>
        where T : class
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="classOptionsTupla">Name of properties to search</param>
        /// <param name="orderBy">Order By Type</param>
        /// <param name="queryBuilder">Implementation of the IQueryBuilderWithWhere interface</param>
        public OrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> classOptionsTupla, OrderBy orderBy,
            IQueryBuilderWithWhere<T, SelectQuery<T>, QueryOptions> queryBuilder)
            : base(classOptionsTupla, orderBy, queryBuilder, queryBuilder.QueryOptions)
        { }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="classOptionsTupla">Name of properties to search</param>
        /// <param name="orderBy">Order By Type</param>
        /// <param name="andOr">Implementation of the IAndOr interface</param>
        /// <param name="formats">Formats</param>
        public OrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> classOptionsTupla, OrderBy orderBy,
           IAndOr<T, SelectQuery<T>, QueryOptions> andOr)
           : base(classOptionsTupla, orderBy, andOr)
        { }

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Order by Query</returns>
        public override OrderByQuery<T> Build()
        {
            string query = CreateQuery(out PropertyOptionsCollection columns, out IEnumerable<CriteriaDetail> criteria);
            return new OrderByQuery<T>(query, columns, criteria, QueryOptions);
        }
    }
}