using GSqlQuery.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GSqlQuery.Queries
{
    internal interface IJoinOrderByQueryBuilder
    {
        void AddOrderBy(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy);
    }

    internal abstract class JoinOrderByQueryBuilder<T, TReturn, TOptions, TSelectQuery> : QueryBuilderWithCriteria<T, TReturn>, IJoinOrderByQueryBuilder
        where T : class
        where TReturn : OrderByQuery<T>
        where TSelectQuery : JoinQuery<T>
    {
        protected readonly IQueryBuilderWithWhere<TSelectQuery, TOptions> _queryBuilder;
        protected readonly IAndOr<T, TSelectQuery> _andorBuilder;
        protected readonly Queue<ColumnsOrderBy> _columnsByOrderBy;

        public JoinOrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy,
            IQueryBuilderWithWhere<T, TSelectQuery, TOptions> queryBuilder, IStatements statements)
            : base(statements)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(selectMember.GetPropertyQuery(), orderBy));
            _queryBuilder = queryBuilder;
            Columns = queryBuilder.Columns;
        }

        public JoinOrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy,
           IAndOr<T, TSelectQuery> andOr, IStatements statements)
           : base(statements)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy = new Queue<ColumnsOrderBy>();
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(selectMember.GetPropertyQuery(), orderBy));
            _andorBuilder = andOr;
            Columns = Enumerable.Empty<PropertyOptions>();
        }

        internal string CreateQuery(IStatements statements, out IEnumerable<PropertyOptions> columns, out IEnumerable<CriteriaDetail> criteria)
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
                $"{string.Join(",", x.Columns.Select(y => y.ColumnAttribute.GetColumnName(ClassOptionsFactory.GetClassOptions(y.PropertyInfo.DeclaringType).Table.GetTableName(statements), statements, QueryType.Join)))} {x.OrderBy}"));

            columns = selectQuery.Columns;
            criteria = selectQuery.Criteria;

            var joinColumns = JoinQueryBuilderWithWhereBase.GetColumns(addJoinCriteria.JoinInfos, statements);
            var tableMain = JoinQueryBuilderWithWhereBase.GetTableMain(addJoinCriteria.JoinInfos);
            IEnumerable<string> JoinQuerys = JoinQueryBuilderWithWhereBase.CreateJoinQuery(addJoinCriteria.JoinInfos, statements);

            string result;

            if (_andorBuilder == null)
            {
                result = string.Format(statements.JoinSelectOrderBy, string.Join(",", joinColumns), tableMain.ClassOptions.Table.GetTableName(statements), string.Join(" ", JoinQuerys), columnsOrderby);
            }
            else
            {
                result = string.Format(statements.JoinSelectWhereOrderBy, string.Join(",", joinColumns), tableMain.ClassOptions.Table.GetTableName(statements), string.Join(" ", JoinQuerys),
                    string.Join(" ", selectQuery.Criteria.Select(x => x.QueryPart)), columnsOrderby);
            }

            return result;
        }

        public void AddOrderBy(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            _columnsByOrderBy.Enqueue(new ColumnsOrderBy(selectMember.GetPropertyQuery(), orderBy));
        }
    }

    internal class JoinOrderByQueryBuilder<T> : JoinOrderByQueryBuilder<T, OrderByQuery<T>, IStatements, JoinQuery<T>>
        where T : class
    {
        public JoinOrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy,
            IQueryBuilderWithWhere<T, JoinQuery<T>, IStatements> queryBuilder)
            : base(selectMember, orderBy, queryBuilder, queryBuilder.Options)
        { }

        public JoinOrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy,
           IAndOr<T, JoinQuery<T>> andOr, IStatements statements)
           : base(selectMember, orderBy, andOr, statements)
        { }

        public override OrderByQuery<T> Build()
        {
            var query = CreateQuery(Options, out IEnumerable<PropertyOptions> columns, out IEnumerable<CriteriaDetail> criteria);
            return new OrderByQuery<T>(query, columns, criteria, Options);
        }
    }
}