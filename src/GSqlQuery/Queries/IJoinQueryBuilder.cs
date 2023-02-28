using System.Linq.Expressions;
using System;

namespace GSqlQuery
{
    public interface IJoinQueryBuilder<T, TReturn> : IQueryBuilderWithWhere<T, TReturn> where T : class, new() where TReturn : IQuery
    {
        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>> InnerJoin<TJoin>() where TJoin : class, new();

        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>> LeftJoin<TJoin>() where TJoin : class, new();

        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>> RightJoin<TJoin>() where TJoin : class, new();

        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression) 
            where TJoin : class, new();

        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new();

        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new();
    }
}
