using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public interface IJoinQueryBuilder<T, TReturn, TOptions, TDbConnection> : IJoinQueryBuilder<T, TReturn, ConnectionOptions<TDbConnection>>
        where T : class, new()
        where TReturn : IQuery<T>
    {
        new IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> InnerJoin<TJoin>() where TJoin : class, new();

        new IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> LeftJoin<TJoin>() where TJoin : class, new();

        new IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> RightJoin<TJoin>() where TJoin : class, new();

        new IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new();

        new IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new();

        new IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>, ConnectionOptions<TDbConnection>> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new();
    }
}