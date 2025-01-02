using GSqlQuery.Cache;
using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Join Order By Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TQueryOptions">Options Type</typeparam>
    /// <typeparam name="TSelectQuery">Select Query</typeparam>
    internal abstract class JoinOrderByQueryBuilder<T, TReturn, TQueryOptions, TSelectQuery> : QueryBuilderWithCriteria<T, TReturn, TQueryOptions>, IJoinOrderByQueryBuilder<T, TReturn, TQueryOptions>
        where T : class
        where TReturn : IQuery<T, TQueryOptions>
        where TSelectQuery : IQuery<T, TQueryOptions>
         where TQueryOptions : QueryOptions
    {
        protected readonly IQueryBuilderWithWhere<TSelectQuery, TQueryOptions> _queryBuilder;
        protected readonly IAndOr<T, TSelectQuery, TQueryOptions> _andorBuilder;
        protected readonly List<ColumnsJoinOrderBy> _expression = [];

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order by Type</param>
        /// <param name="queryBuilder">Implementation of the IQueryBuilderWithWhere interface</param>
        /// <param name="queryOptions">Formats</param>
        public JoinOrderByQueryBuilder(Expression expression, OrderBy orderBy, IQueryBuilderWithWhere<T, TSelectQuery, TQueryOptions> queryBuilder, TQueryOptions queryOptions)
            : base(queryOptions)
        {
            _expression.Add(new ColumnsJoinOrderBy(expression, orderBy));
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order by Type</param>
        /// <param name="andOr">Implementation of the IAndOr interface</param>
        /// <param name="queryOptions">TQueryOptions</param>
        public JoinOrderByQueryBuilder(Expression expression, OrderBy orderBy,
           IAndOr<T, TSelectQuery, TQueryOptions> andOr, TQueryOptions queryOptions)
           : base(queryOptions)
        {
            _expression.Add(new ColumnsJoinOrderBy(expression, orderBy));
            _andorBuilder = andOr;
        }

        /// <summary>
        /// Create query
        /// </summary>
        /// <param name="columns">Colums</param>
        /// <param name="criteria">Criteria</param>
        /// <returns>Query text</returns>
        internal string CreateQueryText(out PropertyOptionsCollection columns, out IEnumerable<CriteriaDetailCollection> criteria)
        {
            TSelectQuery selectQuery = _andorBuilder != null ? _andorBuilder.Build() : _queryBuilder.Build();
            List<string> parts = [];

            foreach (ColumnsJoinOrderBy x in _expression)
            {
                ClassOptionsTupla<PropertyOptionsCollection> options = ExpressionExtension.GetOptionsAndMembers<T>(x.Expression);
                ExpressionExtension.ValidateClassOptionsTupla(QueryType.Criteria, options);

                IEnumerable<string> columnNames =
                    options.Columns.Values.Select(y => y.FormatColumnName.GetColumnName(QueryOptions.Formats, QueryType.Join));
                string tmpJoinColumns = string.Join(",", columnNames);

                string tmpColumns = "{0} {1}".Replace("{0}", tmpJoinColumns).Replace("{1}", x.OrderBy.ToString());
                parts.Add(tmpColumns);
            }

            string columnsOrderby = string.Join(",", parts);
            columns = selectQuery.Columns;
            criteria = selectQuery.Criteria;
            string text = selectQuery.Text.Replace(";", string.Empty);


            if (_andorBuilder == null)
            {
                return ConstFormat.JOINSELECTORDERBY.Replace("{0}", text).Replace("{1}", columnsOrderby);
            }
            else
            {
                return ConstFormat.JOINSELECTORDERBY.Replace("{0}", text).Replace("{1}", columnsOrderby);
            }
        }

        public override TReturn Build()
        {
            return CreateQuery();
        }

        public abstract TReturn GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, TQueryOptions queryOptions);

        public virtual void AddOrderBy<TProperties>(Expression<Func<T, TProperties>> expression, OrderBy orderBy)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            _expression.Add(new ColumnsJoinOrderBy(expression, orderBy));
        }

        public TReturn CreateQuery()
        {
            string query = CreateQueryText(out PropertyOptionsCollection columns, out IEnumerable<CriteriaDetailCollection> criteria);
            TReturn result = GetQuery(query, columns, criteria, QueryOptions);
            return result;
        }
    }

    /// <summary>
    /// Join Order By Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class JoinOrderByQueryBuilder<T> : JoinOrderByQueryBuilder<T, OrderByQuery<T>, QueryOptions, JoinQuery<T, QueryOptions>>
        where T : class
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order by Type</param>
        /// <param name="queryBuilder">Implementation of the IQueryBuilderWithWhere interface</param>
        public JoinOrderByQueryBuilder(Expression expression, OrderBy orderBy,
            IQueryBuilderWithWhere<T, JoinQuery<T, QueryOptions>, QueryOptions> queryBuilder)
            : base(expression, orderBy, queryBuilder, queryBuilder.QueryOptions)
        { }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order by Type</param>
        /// <param name="andOr">Implementation of the IAndOr interface</param>
        /// <param name="formats">Formats</param>
        public JoinOrderByQueryBuilder(Expression expression, OrderBy orderBy,
           IAndOr<T, JoinQuery<T, QueryOptions>, QueryOptions> andOr, QueryOptions queryOptions)
           : base(expression, orderBy, andOr, queryOptions)
        { }

        public override OrderByQuery<T> GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, QueryOptions queryOptions)
        {
            return new OrderByQuery<T>(text, _classOptions.FormatTableName.Table, columns, criteria, QueryOptions);
        }
    }
}