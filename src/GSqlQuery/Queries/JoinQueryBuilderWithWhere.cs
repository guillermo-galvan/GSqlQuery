using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery.Queries
{
    internal class JoinQueryBuilderWithWhere<T1, T2> : JoinQueryBuilderWithWhereBase<T1, T2, Join<T1, T2>, JoinQuery<Join<T1, T2>>, IStatements>,
        IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<Join<T1, T2>>, IStatements>
        where T1 : class, new()
        where T2 : class, new()
    {
        public JoinQueryBuilderWithWhere(string tableName, IEnumerable<PropertyOptions> columns, JoinEnum joinEnum, IStatements statements,
            IEnumerable<PropertyOptions> columnsT2 = null) : base(null, statements)
        {
            _joinInfos.Enqueue(new JoinInfo
            {
                TableName = tableName,
                Columns = columns,
                IsMain = true,
            });

            var tmp = ClassOptionsFactory.GetClassOptions(typeof(T2));
            _joinInfo = new JoinInfo
            {
                TableName = tmp.Table.GetTableName(statements),
                Columns = columnsT2 ?? tmp.GetPropertyQuery(tmp.PropertyOptions.Select(x => x.PropertyInfo.Name)),
                JoinEnum = joinEnum
            };

            _joinInfos.Enqueue(_joinInfo);

            Columns = _joinInfos.SelectMany(x => x.Columns);
        }

        public override JoinQuery<Join<T1, T2>> Build()
        {
            var query = CreateQuery(Options);
            return new JoinQuery<Join<T1, T2>>(query, Columns.Select(x => x.ColumnAttribute), _criteria, Options);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IStatements> InnerJoin<TJoin>() where TJoin : class, new()
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, JoinEnum.Inner, Options);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IStatements> LeftJoin<TJoin>() where TJoin : class, new()
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, JoinEnum.Left, Options);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IStatements> RightJoin<TJoin>() where TJoin : class, new()
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, JoinEnum.Right, Options);
        }

        private IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IStatements> Join<TJoin, TProperties>(JoinEnum joinEnum, Expression<Func<TJoin, TProperties>> expression)
            where TJoin : class, new()
        {
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            var selectMember = options.MemberInfo.Select(x => x.Name);
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, joinEnum, Options, ClassOptionsFactory.GetClassOptions(typeof(TJoin)).GetPropertyQuery(selectMember));
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IStatements> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new()
        {
            return Join(JoinEnum.Inner, expression);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IStatements> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class, new()
        {
            return Join(JoinEnum.Left, expression);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IStatements> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class, new()
        {
            return Join(JoinEnum.Right, expression);
        }
    }

    internal class JoinQueryBuilderWithWhere<T1, T2, T3> : JoinQueryBuilderWithWhereBase<T1, T2, T3, Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>>, IStatements>,
        IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<Join<T1, T2, T3>>, IStatements>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
    {
        public JoinQueryBuilderWithWhere(Queue<JoinInfo> joinInfos, JoinEnum joinEnum, IStatements statements, IEnumerable<PropertyOptions> columnsT3 = null) :
            base(joinInfos, joinEnum, statements, columnsT3)
        { }

        public override JoinQuery<Join<T1, T2, T3>> Build()
        {
            var query = CreateQuery(Options);
            return new JoinQuery<Join<T1, T2, T3>>(query, Columns.Select(x => x.ColumnAttribute), _criteria, Options);
        }
    }

}