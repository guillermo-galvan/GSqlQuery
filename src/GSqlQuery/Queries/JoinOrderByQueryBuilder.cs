using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Join Order By Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TQueryOptions">Options Type</typeparam>
    /// <typeparam name="TSelectQuery">Select Query</typeparam>
    internal abstract class JoinOrderByQueryBuilder<T, TReturn, TQueryOptions, TSelectQuery> : QueryBuilderWithCriteria<T, TReturn, TQueryOptions>, IOrderByQueryBuilder
        where T : class
        where TReturn : IQuery<T, TQueryOptions>
        where TSelectQuery : IQuery<T, TQueryOptions>
         where TQueryOptions : QueryOptions
    {
        protected readonly IQueryBuilderWithWhere<TSelectQuery, TQueryOptions> _queryBuilder;
        protected readonly IAndOr<T, TSelectQuery, TQueryOptions> _andorBuilder;
        protected readonly Queue<ColumnsOrderBy> _columnsByOrderBy;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order by Type</param>
        /// <param name="queryBuilder">Implementation of the IQueryBuilderWithWhere interface</param>
        /// <param name="queryOptions">Formats</param>
        public JoinOrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy,
            IQueryBuilderWithWhere<T, TSelectQuery, TQueryOptions> queryBuilder, TQueryOptions queryOptions)
            : base(queryOptions)
        {
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(GetPropertyQuery(selectMember), orderBy));
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
        public JoinOrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy,
           IAndOr<T, TSelectQuery, TQueryOptions> andOr, TQueryOptions queryOptions)
           : base(queryOptions)
        {
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(GetPropertyQuery(selectMember), orderBy));
            _andorBuilder = andOr;
            Columns = [];
        }

        /// <summary>
        /// Create query
        /// </summary>
        /// <param name="columns">Colums</param>
        /// <param name="criteria">Criteria</param>
        /// <returns>Query text</returns>
        internal string CreateQuery(out IEnumerable<PropertyOptions> columns, out IEnumerable<CriteriaDetail> criteria)
        {
            IAddJoinCriteria<JoinModel> addJoinCriteria = null;

            if (_queryBuilder != null && _queryBuilder is IAddJoinCriteria<JoinModel> queryBuilder)
            {
                addJoinCriteria = queryBuilder;
            }
            else if (_andorBuilder != null && _andorBuilder is AndOrBase<T, TSelectQuery, TQueryOptions> andor && andor._queryBuilderWithWhere is IAddJoinCriteria<JoinModel> andorBuilder)
            {
                addJoinCriteria = andorBuilder;
            }

            TSelectQuery selectQuery = _andorBuilder != null ? _andorBuilder.Build() : _queryBuilder.Build();
            Queue<string> parts = new Queue<string>();

            foreach (ColumnsOrderBy x in _columnsByOrderBy)
            {
                IEnumerable<string> columnNames =
                    x.Columns.Select(y => QueryOptions.Formats.GetColumnName(TableAttributeExtension.GetTableName(y.TableAttribute, QueryOptions.Formats), y.ColumnAttribute, QueryType.Join));
                string tmpJoinColumns = string.Join(",", columnNames);

                string tmpColumns = "{0} {1}".Replace("{0}", tmpJoinColumns).Replace("{1}", x.OrderBy.ToString());
                parts.Enqueue(tmpColumns);
            }

            string columnsOrderby = string.Join(",", parts);
            columns = selectQuery.Columns;
            criteria = selectQuery.Criteria;

            IEnumerable<string> joinColumns = JoinQueryBuilderWithWhereBase.GetColumns(addJoinCriteria.JoinInfos, QueryOptions.Formats);
            JoinInfo tableMain = JoinQueryBuilderWithWhereBase.GetTableMain(addJoinCriteria.JoinInfos);
            IEnumerable<string> joinQuerys = JoinQueryBuilderWithWhereBase.CreateJoinQuery(addJoinCriteria.JoinInfos, QueryOptions.Formats);

            string resultJoinColumns = string.Join(",", joinColumns);
            string tableName = TableAttributeExtension.GetTableName(tableMain.ClassOptions.Table, QueryOptions.Formats);
            string resultJoinQuerys = string.Join(" ", joinQuerys);

            if (_andorBuilder == null)
            {
                return ConstFormat.JOINSELECTORDERBY.Replace("{0}", resultJoinColumns).Replace("{1}", tableName)
                                                    .Replace("{2}", resultJoinQuerys).Replace("{3}", columnsOrderby);
            }
            else
            {
                string resultWhere = string.Join(" ", selectQuery.Criteria.Select(x => x.QueryPart));
                return ConstFormat.JOINSELECTWHEREORDERBY.Replace("{0}", resultJoinColumns).Replace("{1}", tableName)
                                                         .Replace("{2}", resultJoinQuerys).Replace("{3}", resultWhere)
                                                         .Replace("{4}", columnsOrderby);
            }
        }

        /// <summary>
        /// Add Columns
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order by Type</param>
        public void AddOrderBy(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy)
        {
            if (selectMember == null)
            {
                throw new ArgumentNullException(nameof(selectMember));
            }
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(GetPropertyQuery(selectMember), orderBy));
        }

        /// <summary>
        /// Gets the property options
        /// </summary>
        /// <param name="optionsTupla">Class that contains the ClassOptions and selectMember information</param>
        /// <returns>Properties that match selectMember</returns>
        /// <exception cref="ArgumentNullException"></exception>
        protected IEnumerable<PropertyOptions> GetPropertyQuery(ClassOptionsTupla<IEnumerable<MemberInfo>> optionsTupla)
        {
            if (optionsTupla == null)
            {
                throw new ArgumentNullException(nameof(optionsTupla), ErrorMessages.ParameterNotNull);
            }

            List<PropertyOptions> properties = [];
            IEnumerable<string> listName = optionsTupla.MemberInfo.Select(x => x.Name);

            if (optionsTupla.MemberInfo.Any(x => x.DeclaringType.IsGenericType))
            {
                foreach (PropertyOptions item in optionsTupla.ClassOptions.PropertyOptions.Where(x => x.PropertyInfo.PropertyType.IsClass))
                {
                    ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(item.PropertyInfo.PropertyType);
                    IEnumerable<PropertyOptions> a = classOptions.PropertyOptions.Where(x => listName.Contains(x.PropertyInfo.Name));

                    properties.AddRange(a);
                }
            }
            else
            {
                foreach (MemberInfo item in optionsTupla.MemberInfo)
                {
                    ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(item.DeclaringType);
                    IEnumerable<PropertyOptions> a = classOptions.PropertyOptions.Where(x => listName.Contains(x.PropertyInfo.Name));

                    properties.AddRange(a);
                }
            }

            return properties;
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
        public JoinOrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy,
            IQueryBuilderWithWhere<T, JoinQuery<T, QueryOptions>, QueryOptions> queryBuilder)
            : base(selectMember, orderBy, queryBuilder, queryBuilder.QueryOptions)
        { }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order by Type</param>
        /// <param name="andOr">Implementation of the IAndOr interface</param>
        /// <param name="formats">Formats</param>
        public JoinOrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy,
           IAndOr<T, JoinQuery<T, QueryOptions>, QueryOptions> andOr, QueryOptions queryOptions)
           : base(selectMember, orderBy, andOr, queryOptions)
        { }

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Order by Query</returns>
        public override OrderByQuery<T> Build()
        {
            string query = CreateQuery(out IEnumerable<PropertyOptions> columns, out IEnumerable<CriteriaDetail> criteria);
            return new OrderByQuery<T>(query, columns, criteria, QueryOptions);
        }
    }
}