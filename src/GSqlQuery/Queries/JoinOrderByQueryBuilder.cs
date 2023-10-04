using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Join Order By Query Builder
    /// </summary>
    internal interface IJoinOrderByQueryBuilder
    {
        /// <summary>
        /// Add Columns
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order by Type</param>
        void AddOrderBy(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy);
    }

    /// <summary>
    /// Join Order By Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TOptions">Options Type</typeparam>
    /// <typeparam name="TSelectQuery">Select Query</typeparam>
    internal abstract class JoinOrderByQueryBuilder<T, TReturn, TOptions, TSelectQuery> : QueryBuilderWithCriteria<T, TReturn>, IJoinOrderByQueryBuilder
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
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(selectMember.GetPropertyQuery(), orderBy));
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
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(selectMember.GetPropertyQuery(), orderBy));
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

            TSelectQuery selectQuery = _queryBuilder == null ? _andorBuilder.Build() : _queryBuilder.Build();
            string columnsOrderby =
                string.Join(",", _columnsByOrderBy.Select(x =>
                $"{string.Join(",", x.Columns.Select(y => y.ColumnAttribute.GetColumnName(ClassOptionsFactory.GetClassOptions(y.PropertyInfo.DeclaringType).Table.GetTableName(Options), Options, QueryType.Join)))} {x.OrderBy}"));

            columns = selectQuery.Columns;
            criteria = selectQuery.Criteria;

            var joinColumns = JoinQueryBuilderWithWhereBase.GetColumns(addJoinCriteria.JoinInfos, Options);
            var tableMain = JoinQueryBuilderWithWhereBase.GetTableMain(addJoinCriteria.JoinInfos);
            IEnumerable<string> JoinQuerys = JoinQueryBuilderWithWhereBase.CreateJoinQuery(addJoinCriteria.JoinInfos, Options);

            string result;

            if (_andorBuilder == null)
            {
                result = string.Format(ConstFormat.JOINSELECTORDERBY, string.Join(",", joinColumns), tableMain.ClassOptions.Table.GetTableName(Options), string.Join(" ", JoinQuerys), columnsOrderby);
            }
            else
            {
                result = string.Format(ConstFormat.JOINSELECTWHEREORDERBY, string.Join(",", joinColumns), tableMain.ClassOptions.Table.GetTableName(Options), string.Join(" ", JoinQuerys),
                    string.Join(" ", selectQuery.Criteria.Select(x => x.QueryPart)), columnsOrderby);
            }

            return result;
        }

        /// <summary>
        /// Add Columns
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="orderBy">Order by Type</param>
        public void AddOrderBy(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(selectMember.GetPropertyQuery(), orderBy));
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
            var query = CreateQuery(out IEnumerable<PropertyOptions> columns, out IEnumerable<CriteriaDetail> criteria);
            return new OrderByQuery<T>(query, columns, criteria, Options);
        }
    }
}