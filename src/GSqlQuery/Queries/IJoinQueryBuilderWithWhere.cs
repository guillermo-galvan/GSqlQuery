using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public interface IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> : IQueryBuilderWithWhere<TReturn, TOptions>, IQueryBuilderWithWhere<Join<T1, T2>, TReturn, TOptions>
        where T1 : class
        where T2 : class
        where TReturn : IQuery<Join<T1, T2>>
    {
        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, TOptions> InnerJoin<TJoin>() where TJoin : class;

        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, TOptions> LeftJoin<TJoin>() where TJoin : class;

        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, TOptions> RightJoin<TJoin>() where TJoin : class;

        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, TOptions> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class;

        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, TOptions> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class;

        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, TOptions> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class;
    }

    public interface IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> : IQueryBuilderWithWhere<TReturn, TOptions>
        where T1 : class
        where T2 : class
        where T3 : class
        where TReturn : IQuery<Join<T1, T2, T3>>
    {
        new IWhere<Join<T1, T2, T3>, TReturn> Where();
    }
}