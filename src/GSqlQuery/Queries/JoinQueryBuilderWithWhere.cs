using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery.Queries
{
    internal class JoinQueryBuilderWithWhere<T1, T2> : JoinQueryBuilderWithWhereBase<T1, T2, Join<T1, T2>, JoinQuery<Join<T1, T2>>, IFormats>,
        IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<Join<T1, T2>>, IFormats>
        where T1 : class
        where T2 : class
    {
        public JoinQueryBuilderWithWhere(string tableName, IEnumerable<PropertyOptions> columns, JoinType joinEnum, IFormats statements,
            IEnumerable<PropertyOptions> columnsT2 = null) : base(null, statements)
        {
            _joinInfos.Enqueue(new JoinInfo
            {
                ClassOptions = ClassOptionsFactory.GetClassOptions(typeof(T1)),
                Columns = columns,
                IsMain = true,
            });

            var tmp = ClassOptionsFactory.GetClassOptions(typeof(T2));
            _joinInfo = new JoinInfo
            {
                ClassOptions = tmp,
                Columns = columnsT2 ?? tmp.GetPropertyQuery(tmp.PropertyOptions.Select(x => x.PropertyInfo.Name)),
                JoinEnum = joinEnum,
            };

            _joinInfos.Enqueue(_joinInfo);

            Columns = _joinInfos.SelectMany(x => x.Columns);
        }

        public override JoinQuery<Join<T1, T2>> Build()
        {
            var query = CreateQuery(Options);
            return new JoinQuery<Join<T1, T2>>(query, Columns, _criteria, Options);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IFormats> InnerJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, JoinType.Inner, Options);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IFormats> LeftJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, JoinType.Left, Options);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IFormats> RightJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, JoinType.Right, Options);
        }

        private IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IFormats> Join<TJoin, TProperties>(JoinType joinEnum, Expression<Func<TJoin, TProperties>> expression)
            where TJoin : class
        {
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            var selectMember = options.MemberInfo.Select(x => x.Name);
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, joinEnum, Options, ClassOptionsFactory.GetClassOptions(typeof(TJoin)).GetPropertyQuery(selectMember));
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IFormats> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class
        {
            return Join(JoinType.Inner, expression);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IFormats> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class
        {
            return Join(JoinType.Left, expression);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IFormats> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class
        {
            return Join(JoinType.Right, expression);
        }
    }

    internal class JoinQueryBuilderWithWhere<T1, T2, T3> : JoinQueryBuilderWithWhereBase<T1, T2, T3, Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>>, IFormats>,
        IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<Join<T1, T2, T3>>, IFormats>
        where T1 : class
        where T2 : class
        where T3 : class
    {
        public JoinQueryBuilderWithWhere(Queue<JoinInfo> joinInfos, JoinType joinEnum, IFormats statements, IEnumerable<PropertyOptions> columnsT3 = null) :
            base(joinInfos, joinEnum, statements, columnsT3)
        { }

        public override JoinQuery<Join<T1, T2, T3>> Build()
        {
            var query = CreateQuery(Options);
            return new JoinQuery<Join<T1, T2, T3>>(query, Columns, _criteria, Options);
        }
    }

}