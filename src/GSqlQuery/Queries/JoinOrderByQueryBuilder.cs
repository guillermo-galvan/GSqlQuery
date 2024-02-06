using GSqlQuery.Extensions;
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
    /// <typeparam name="TOptions">Options Type</typeparam>
    /// <typeparam name="TSelectQuery">Select Query</typeparam>
    internal abstract class JoinOrderByQueryBuilder<T, TReturn, TOptions, TSelectQuery> : QueryBuilderWithCriteria<T, TReturn>, IOrderByQueryBuilder
        where T : class
        where TReturn : OrderByQuery<T>
        where TSelectQuery : JoinQuery<T>
    {
        protected readonly IQueryBuilderWithWhere<TSelectQuery, TOptions> _queryBuilder;
        protected readonly IAndOr<T, TSelectQuery> _andorBuilder;
        protected readonly Queue<ColumnsOrderBy> _columnsByOrderBy;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order by Type</param>
        /// <param name="queryBuilder">Implementation of the IQueryBuilderWithWhere interface</param>
        /// <param name="formats">Formats</param>
        public JoinOrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy,
            IQueryBuilderWithWhere<T, TSelectQuery, TOptions> queryBuilder, IFormats formats)
            : base(formats)
        {
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(GeneralExtension.GetPropertyQuery(selectMember), orderBy));
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order by Type</param>
        /// <param name="andOr">Implementation of the IAndOr interface</param>
        /// <param name="formats">Formats</param>
        public JoinOrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy,
           IAndOr<T, TSelectQuery> andOr, IFormats formats)
           : base(formats)
        {
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(GeneralExtension.GetPropertyQuery(selectMember), orderBy));
            _andorBuilder = andOr;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        /// <summary>
        /// Create query
        /// </summary>
        /// <param name="columns">Colums</param>
        /// <param name="criteria">Criteria</param>
        /// <returns>Query text</returns>
        internal string CreateQuery( out IEnumerable<PropertyOptions> columns, out IEnumerable<CriteriaDetail> criteria)
        {
            IAddJoinCriteria<JoinModel> addJoinCriteria = null;

            if (_queryBuilder != null && _queryBuilder is IAddJoinCriteria<JoinModel> queryBuilder)
            {
                addJoinCriteria = queryBuilder;
            }
            else if (_andorBuilder != null && _andorBuilder is AndOrBase<T, TSelectQuery, TOptions> andor && andor._queryBuilderWithWhere is IAddJoinCriteria<JoinModel> andorBuilder)
            {
                addJoinCriteria = andorBuilder;
            }

            TSelectQuery selectQuery = _andorBuilder?.Build() ?? _queryBuilder.Build();
            Queue<string> parts = new Queue<string>();

            foreach (ColumnsOrderBy x in _columnsByOrderBy)
            {
                IEnumerable<string> columnNames = 
                    x.Columns.Select(y => Options.GetColumnName(TableAttributeExtension.GetTableName(y.TableAttribute, Options), y.ColumnAttribute, QueryType.Join));
                string tmpJoinColumns = string.Join(",", columnNames);

                string tmpColumns = "{0} {1}".Replace("{0}", tmpJoinColumns).Replace("{1}", x.OrderBy.ToString());
                parts.Enqueue(tmpColumns);
            }

            string columnsOrderby = string.Join(",", parts);
            columns = selectQuery.Columns;
            criteria = selectQuery.Criteria;

            IEnumerable<string> joinColumns = JoinQueryBuilderWithWhereBase.GetColumns(addJoinCriteria.JoinInfos, Options);
            JoinInfo tableMain = JoinQueryBuilderWithWhereBase.GetTableMain(addJoinCriteria.JoinInfos);
            IEnumerable<string> joinQuerys = JoinQueryBuilderWithWhereBase.CreateJoinQuery(addJoinCriteria.JoinInfos, Options);

            string resultJoinColumns = string.Join(",", joinColumns);
            string tableName = TableAttributeExtension.GetTableName(tableMain.ClassOptions.Table, Options);
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
            ObjectExtension.NullValidate(selectMember,ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(GeneralExtension.GetPropertyQuery(selectMember), orderBy));
        }
    }

    /// <summary>
    /// Join Order By Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class JoinOrderByQueryBuilder<T> : JoinOrderByQueryBuilder<T, OrderByQuery<T>, IFormats, JoinQuery<T>>
        where T : class
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order by Type</param>
        /// <param name="queryBuilder">Implementation of the IQueryBuilderWithWhere interface</param>
        public JoinOrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy,
            IQueryBuilderWithWhere<T, JoinQuery<T>, IFormats> queryBuilder)
            : base(selectMember, orderBy, queryBuilder, queryBuilder.Options)
        { }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order by Type</param>
        /// <param name="andOr">Implementation of the IAndOr interface</param>
        /// <param name="formats">Formats</param>
        public JoinOrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy,
           IAndOr<T, JoinQuery<T>> andOr, IFormats formats)
           : base(selectMember, orderBy, andOr, formats)
        { }

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Order by Query</returns>
        public override OrderByQuery<T> Build()
        {
            string query = CreateQuery(out IEnumerable<PropertyOptions> columns, out IEnumerable<CriteriaDetail> criteria);
            return new OrderByQuery<T>(query, columns, criteria, Options);
        }
    }
}