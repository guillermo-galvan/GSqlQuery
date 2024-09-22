using GSqlQuery.Cache;
using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Order By Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TOptions">Options Type</typeparam>
    /// <typeparam name="TSelectQuery">Select query</typeparam>
    internal abstract class OrderByQueryBuilder<T, TReturn, TQueryOptions, TSelectQuery> : QueryBuilderWithCriteria<T, TReturn, TQueryOptions>, IOrderByQueryBuilder<T, TReturn, TQueryOptions>
        where T : class
        where TReturn : IQuery<T, TQueryOptions>
        where TSelectQuery : IQuery<T, TQueryOptions>
        where TQueryOptions : QueryOptions
    {
        protected readonly IQueryBuilderWithWhere<TSelectQuery, TQueryOptions> _queryBuilder;
        protected readonly IAndOr<T, TSelectQuery, TQueryOptions> _andorBuilder = null;
        protected readonly List<ColumnsOrderBy> _columnsByOrderBy = [];

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="classOptionsTupla">>Name of properties to search</param>
        /// <param name="orderBy">Order By Type</param>
        /// <param name="queryBuilder">Implementation of the IQueryBuilderWithWhere interface</param>
        /// <param name="queryOptions">Formats</param>
        protected OrderByQueryBuilder(DynamicQuery dynamicQuery, OrderBy orderBy,
            IQueryBuilderWithWhere<T, TSelectQuery, TQueryOptions> queryBuilder, TQueryOptions queryOptions) : base(queryOptions)
        {
            ColumnsOrderBy columnsOrderBy = new ColumnsOrderBy(dynamicQuery, orderBy);
            _columnsByOrderBy.Add(columnsOrderBy);
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="classOptionsTupla">Name of properties to search</param>
        /// <param name="orderBy">Order By Type</param>
        /// <param name="andOr">Implementation of the IAndOr interface</param>
        protected OrderByQueryBuilder(DynamicQuery dynamicQuery, OrderBy orderBy,
           IAndOr<T, TSelectQuery, TQueryOptions> andOr) : base(andOr.QueryOptions)
        {
            ColumnsOrderBy columnsOrderBy = new ColumnsOrderBy(dynamicQuery, orderBy);
            _columnsByOrderBy.Add(columnsOrderBy);
            _andorBuilder = andOr;
            Columns = new PropertyOptionsCollection([]);
        }

        /// <summary>
        /// Add columns to the 'Order By' statement
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">>Order By Type</param>
        public void AddOrderBy<TProperties>(Func<T, TProperties> func, OrderBy orderBy)
        {
            ColumnsOrderBy columnsOrderBy = new ColumnsOrderBy(new DynamicQuery(typeof(T), typeof(TProperties)), orderBy);
            _columnsByOrderBy.Add(columnsOrderBy);
        }

        /// <summary>
        /// Create query
        /// </summary>
        /// <param name="columns">Columns</param>
        /// <param name="criteria">Criterias</param>
        /// <returns></returns>
        internal string CreateQueryText(out PropertyOptionsCollection columns, out IEnumerable<CriteriaDetailCollection> criteria)
        {
            TSelectQuery selectQuery = _andorBuilder != null ? _andorBuilder.Build() : _queryBuilder.Build();
            Queue<string> parts = new Queue<string>();

            foreach (ColumnsOrderBy x in _columnsByOrderBy)
            {
                ClassOptionsTupla<PropertyOptionsCollection> options = ExpressionExtension.GeTQueryOptionsAndMembersByFunc(x.DynamicQuery);
                ExpressionExtension.ValidateClassOptionsTupla(QueryType.Read, options);
                IEnumerable<string> names = options.Columns.Values.Select(y => y.FormatColumnName.GetColumnName(QueryOptions.Formats, QueryType.Read));
                string columnsName = string.Join(",", names);
                string orderByQuery = "{0} {1}".Replace("{0}", columnsName).Replace("{1}", x.OrderBy.ToString());
                parts.Enqueue(orderByQuery);
            }

            string columnsOrderby = string.Join(",", parts);
            columns = selectQuery.Columns;
            criteria = selectQuery.Criteria;

            IEnumerable<string> ColumnNames = columns.Values.Select(x => x.FormatColumnName.GetColumnName(QueryOptions.Formats, QueryType.Read));
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

        public override TReturn Build()
        {
            return CacheQueryBuilderExtension.CreateOrderByQuery(QueryOptions, _queryBuilder, _andorBuilder, _columnsByOrderBy, CreateQuery, GetQuery);
        }

        public abstract TReturn GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, TQueryOptions queryOptions);

        public TReturn CreateQuery()
        {
            string text = CreateQueryText(out PropertyOptionsCollection columns, out IEnumerable<CriteriaDetailCollection> criteria);
            TReturn result = GetQuery(text, columns, criteria, QueryOptions);
            return result;
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
        public OrderByQueryBuilder(DynamicQuery dynamicQuery, OrderBy orderBy,
            IQueryBuilderWithWhere<T, SelectQuery<T>, QueryOptions> queryBuilder)
            : base(dynamicQuery, orderBy, queryBuilder, queryBuilder.QueryOptions)
        { }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="classOptionsTupla">Name of properties to search</param>
        /// <param name="orderBy">Order By Type</param>
        /// <param name="andOr">Implementation of the IAndOr interface</param>
        /// <param name="formats">Formats</param>
        public OrderByQueryBuilder(DynamicQuery dynamicQuery, OrderBy orderBy,
           IAndOr<T, SelectQuery<T>, QueryOptions> andOr)
           : base(dynamicQuery, orderBy, andOr)
        { }

        public override OrderByQuery<T> GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, QueryOptions queryOptions)
        {
            return new OrderByQuery<T>(text, _classOptions.FormatTableName.Table, columns, criteria, QueryOptions);
        }
    }
}