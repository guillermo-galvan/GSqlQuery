using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery.Runner.Queries
{
    internal class JoinQueryBuilderWithWhere<T1, T2, TDbConnection> :
        JoinQueryBuilderWithWhereBase<T1, T2, Join<T1, T2>, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>>,
        IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, TDbConnection>,
        IComparisonOperators<Join<T1, T2>, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>>,
        GSqlQuery.IJoinQueryBuilderWithWhere<T1, T2, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>
        where T1 : class
        where T2 : class
    {
        public JoinQueryBuilderWithWhere(IEnumerable<PropertyOptions> columns, JoinType joinEnum, ConnectionOptions<TDbConnection> connectionOptions,
            IEnumerable<PropertyOptions> columnsT2 = null) : base(null, connectionOptions)
        {
            ClassOptions classoptions = ClassOptionsFactory.GetClassOptions(typeof(T1));
            JoinInfo joinInfo = new JoinInfo(columns, classoptions, true);
            _joinInfos.Enqueue(joinInfo);

            ClassOptions classoptions2 = ClassOptionsFactory.GetClassOptions(typeof(T2));
            columnsT2 ??= classoptions2.PropertyOptions;

            _joinInfo = new JoinInfo(columnsT2, classoptions2, joinEnum);

            _joinInfos.Enqueue(_joinInfo);

            Columns = _joinInfos.SelectMany(x => x.Columns);
        }

        private IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> Join<TJoin, TProperties>(JoinType joinEnum, Expression<Func<TJoin, TProperties>> expression)
            where TJoin : class
        {
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = ExpressionExtension.GetOptionsAndMembers(expression);
            ExpressionExtension.ValidateMemberInfos(QueryType.Join, options);
            IEnumerable<string> selectMember = options.MemberInfo.Select(x => x.Name);
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin, TDbConnection>(_joinInfos, joinEnum, QueryOptions, ExpressionExtension.GetPropertyQuery(options.ClassOptions, selectMember));
        }

        public override JoinQuery<Join<T1, T2>, TDbConnection> Build()
        {
            string query = CreateQuery();
            return new JoinQuery<Join<T1, T2>, TDbConnection>(query, Columns, _criteria, QueryOptions);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> InnerJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin, TDbConnection>(_joinInfos, JoinType.Inner, QueryOptions);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> LeftJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin, TDbConnection>(_joinInfos, JoinType.Left, QueryOptions);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> RightJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin, TDbConnection>(_joinInfos, JoinType.Right, QueryOptions);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class
        {
            return Join(JoinType.Inner, expression);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class
        {
            return Join(JoinType.Left, expression);
        }

        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class
        {
            return Join(JoinType.Right, expression);
        }

        IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilderWithWhere<T1, T2, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>.InnerJoin<TJoin>()
        {
            return (IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>) new JoinQueryBuilderWithWhere<T1, T2, TJoin, TDbConnection>(_joinInfos, JoinType.Inner, QueryOptions);
        }

        IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilderWithWhere<T1, T2, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>.LeftJoin<TJoin>()
        {
            return (IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)new JoinQueryBuilderWithWhere<T1, T2, TJoin, TDbConnection>(_joinInfos, JoinType.Left, QueryOptions);
        }

        IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilderWithWhere<T1, T2, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>.RightJoin<TJoin>()
        {
            return (IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)new JoinQueryBuilderWithWhere<T1, T2, TJoin, TDbConnection>(_joinInfos, JoinType.Right, QueryOptions);
        }

        IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilderWithWhere<T1, T2, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>.InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
        {
            return (IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)Join(JoinType.Inner, expression);
        }

        IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilderWithWhere<T1, T2, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>.LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression)
        {
            return (IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)Join(JoinType.Left, expression);
        }

        IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilderWithWhere<T1, T2, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>.RightJoin<TJoin>(Expression<Func<TJoin, object>> expression)
        {
            return (IComparisonOperators<Join<T1, T2, TJoin>, GSqlQuery.JoinQuery<Join<T1, T2, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)Join(JoinType.Right, expression);
        }

        IWhere<Join<T1, T2>, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> IQueryBuilderWithWhere<Join<T1, T2>, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>.Where()
        {
            return (IWhere<Join<T1, T2>, GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)base.Where();
        }

        IWhere<GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>> IQueryBuilderWithWhere<GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>.Where()
        {
            return (IWhere<GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>>)base.Where();
        }

        GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>> IBuilder<GSqlQuery.JoinQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>>.Build()
        {
            return Build();
        }
    }

    internal class JoinQueryBuilderWithWhere<T1, T2, T3, TDbConnection> :
        JoinQueryBuilderWithWhereBase<T1, T2, T3, Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>>,
         IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, TDbConnection>
        where T1 : class
        where T2 : class
        where T3 : class
    {
        public JoinQueryBuilderWithWhere(Queue<JoinInfo> joinInfos, JoinType joinEnum, ConnectionOptions<TDbConnection> connectionOptions, IEnumerable<PropertyOptions> columnsT3 = null) :  base(joinInfos, joinEnum, connectionOptions, columnsT3)
        { }

        public override JoinQuery<Join<T1, T2, T3>, TDbConnection> Build()
        {
            string query = CreateQuery();
            return new JoinQuery<Join<T1, T2, T3>, TDbConnection>(query, Columns, _criteria, QueryOptions);
        }


    }
}