using System.Linq.Expressions;
using System;

namespace GSqlQuery
{
    public interface IJoinQueryBuilder<T, TReturn, TDbConnection> : IQueryBuilderWithWhereRunner<T, TReturn,TDbConnection> 
        where T : class, new() 
        where TReturn : IQuery
    {
        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>,TDbConnection>> InnerJoin<TJoin>() where TJoin : class, new();

        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>,TDbConnection>> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new();

        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>> LeftJoin<TJoin>() where TJoin : class, new();

        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new();

        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>> RightJoin<TJoin>() where TJoin : class, new();

        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TDbConnection>> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new();
    }
}
