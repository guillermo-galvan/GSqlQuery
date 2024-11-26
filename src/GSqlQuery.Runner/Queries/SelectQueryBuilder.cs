using GSqlQuery.Cache;
using GSqlQuery.Queries;
using System;
using System.Collections.Generic;

namespace GSqlQuery.Runner.Queries
{
    internal class SelectQueryBuilder<T, TDbConnection> : SelectQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>,
        IQueryBuilder<SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>,
        IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, TDbConnection>,
        IQueryBuilderWithWhere<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>,
        GSqlQuery.IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
        where T : class
    {
        public SelectQueryBuilder(DynamicQuery dynamicQuery, ConnectionOptions<TDbConnection> connectionOptions) : 
            base(dynamicQuery, connectionOptions)
        {  }

        public SelectQueryBuilder(ConnectionOptions<TDbConnection> connectionOptions) : base(connectionOptions)
        { }

        private IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> Join<TJoin>(JoinType joinEnum) where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T, TJoin, TDbConnection>(joinEnum, QueryOptions);
        }

        private IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> Join<TJoin>(JoinType joinEnum, Func<TJoin, object> func)
           where TJoin : class
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func), ErrorMessages.ParameterNotNull);
            }

            Type secondTable = typeof(TJoin);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(secondTable);
            var result = func((TJoin)options.Entity);
            DynamicQuery dynamicQuery = new DynamicQuery(secondTable, result.GetType());

            return _dynamicQuery == null ? new JoinQueryBuilderWithWhere<T, TJoin, TDbConnection>(joinEnum, QueryOptions, dynamicQuery) : new JoinQueryBuilderWithWhere<T, TJoin, TDbConnection>(_dynamicQuery, joinEnum, QueryOptions, dynamicQuery);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> InnerJoin<TJoin>() where TJoin : class
        {
            return Join<TJoin>(JoinType.Inner);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> LeftJoin<TJoin>() where TJoin : class
        {
            return Join<TJoin>(JoinType.Left);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> RightJoin<TJoin>() where TJoin : class
        {
            return Join<TJoin>(JoinType.Right);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> InnerJoin<TJoin>(Func<TJoin, object> func) where TJoin : class
        {
            return Join(JoinType.Inner, func);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> LeftJoin<TJoin>(Func<TJoin, object> func) where TJoin : class
        {
            return Join(JoinType.Left, func);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> RightJoin<TJoin>(Func<TJoin, object> func) where TJoin : class
        {
            return Join(JoinType.Right, func);
        }

        IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>.InnerJoin<TJoin>()
        {
            return (IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)new JoinQueryBuilderWithWhere<T, TJoin, TDbConnection>(JoinType.Inner, QueryOptions);
        }

        IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>.LeftJoin<TJoin>()
        {
            return (IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)new JoinQueryBuilderWithWhere<T, TJoin, TDbConnection>( JoinType.Left, QueryOptions);
        }

        IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>.RightJoin<TJoin>()
        {
            return (IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)new JoinQueryBuilderWithWhere<T, TJoin, TDbConnection>( JoinType.Right, QueryOptions);
        }

        IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>.InnerJoin<TJoin>(Func<TJoin, object> func)
        {
            return (IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)Join(JoinType.Inner, func);
        }

        IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>.LeftJoin<TJoin>(Func<TJoin, object> func)
        {
            return (IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)Join(JoinType.Left, func);
        }

        IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>> GSqlQuery.IJoinQueryBuilder<T, SelectQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>.RightJoin<TJoin>(Func<TJoin, object> func)
        {
            return (IComparisonOperators<Join<T, TJoin>, GSqlQuery.JoinQuery<Join<T, TJoin>, ConnectionOptions<TDbConnection>>, ConnectionOptions<TDbConnection>>)Join(JoinType.Right, func);
        }

        public override SelectQuery<T, TDbConnection> GetQuery(string text, PropertyOptionsCollection columns, IEnumerable<CriteriaDetailCollection> criteria, ConnectionOptions<TDbConnection> queryOptions)
        {
            return new SelectQuery<T, TDbConnection>(text, _classOptions.FormatTableName.Table, columns, criteria, queryOptions);
        }
    }
}