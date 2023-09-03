using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public interface IJoinQueryBuilder<T, TReturn, TOptions> : IQueryBuilderWithWhere<TReturn, TOptions>, IQueryBuilderWithWhere<T, TReturn, TOptions>
        where T : class, new()
        where TReturn : IQuery<T>
    {
        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, TOptions> InnerJoin<TJoin>() where TJoin : class, new();

        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, TOptions> LeftJoin<TJoin>() where TJoin : class, new();

        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, TOptions> RightJoin<TJoin>() where TJoin : class, new();

        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, TOptions> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new();

        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, TOptions> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new();

        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, TOptions> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new();
    }
}