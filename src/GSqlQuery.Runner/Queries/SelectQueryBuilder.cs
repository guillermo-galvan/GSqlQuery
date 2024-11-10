using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery.Runner.Queries
{
    internal class SelectQueryBuilder<T, TDbConnection> : SelectQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>,
        IQueryBuilder<SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>,
        IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>,
        GSqlQuery.IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
        where T : class
    {
        public SelectQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> classOptionsTupla, ConnectionOptions<TDbConnection> connectionOptions) : 
            base(classOptionsTupla, connectionOptions)
        {  }

        public SelectQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions) : base(connectionOptions)
        { }

        public override SelectQuery<T, TDbConnection> Build()
        {
            string text = CreateQuery();
            return new SelectQuery<T, TDbConnection>(text, Columns, _criteria, QueryOptions);
        }

        private IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> Join<TJoin, TProperties>
            (JoinType joinEnum, Expression<Func<TJoin, TProperties>> expression)
            where TJoin : class
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }

            ClassOptionsTupla<IEnumerable<MemberInfo>> options = ExpressionExtension.GetOptionsAndMembers(expression);
            return new JoinQueryBuilderWithWhere<T, TJoin, TDbConnection>(Columns, joinEnum, QueryOptions, ExpressionExtension.GetPropertyQuery(options));
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> InnerJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T, TJoin, TDbConnection>(Columns, JoinType.Inner, QueryOptions);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> LeftJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T, TJoin, TDbConnection>(Columns, JoinType.Left, QueryOptions);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> RightJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T, TJoin, TDbConnection>(Columns, JoinType.Right, QueryOptions);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class
        {
            return Join(JoinType.Inner, expression);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class
        {
            return Join(JoinType.Left, expression);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class
        {
            return Join(JoinType.Right, expression);
        }

        IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>.InnerJoin<TJoin>()
        {
            return (IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)new JoinQueryBuilderWithWhere<T, TJoin, TDbConnection>(Columns, JoinType.Inner, QueryOptions);
        }

        IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>.LeftJoin<TJoin>()
        {
            return (IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)new JoinQueryBuilderWithWhere<T, TJoin, TDbConnection>(Columns, JoinType.Left, QueryOptions);
        }

        IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>.RightJoin<TJoin>()
        {
            return (IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)new JoinQueryBuilderWithWhere<T, TJoin, TDbConnection>(Columns, JoinType.Right, QueryOptions);
        }

        IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>.InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
        {
            return (IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)Join(JoinType.Inner, expression);
        }

        IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>.LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression)
        {
            return (IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)Join(JoinType.Left, expression);
        }

        IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>.RightJoin<TJoin>(Expression<Func<TJoin, object>> expression)
        {
            return (IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)Join(JoinType.Right, expression);
        }
    }
}