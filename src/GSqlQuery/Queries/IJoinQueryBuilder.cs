using System.Linq.Expressions;
using System;

namespace GSqlQuery
{
    public interface IJoinQueryBuilder<T, TReturn> : IQueryBuilderWithWhere<T, TReturn> where T : class, new() where TReturn : IQuery
    {
        IComparisonOperators<JoinTwoTables<T, TJoin>, JoinQuery<JoinTwoTables<T, TJoin>>> InnerJoin<TJoin>() where TJoin : class, new();

        IComparisonOperators<JoinTwoTables<T, TJoin>, JoinQuery<JoinTwoTables<T, TJoin>>> LeftJoin<TJoin>() where TJoin : class, new();

        IComparisonOperators<JoinTwoTables<T, TJoin>, JoinQuery<JoinTwoTables<T, TJoin>>> RightJoin<TJoin>() where TJoin : class, new();

        IComparisonOperators<JoinTwoTables<T, TJoin>, JoinQuery<JoinTwoTables<T, TJoin>>> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression) 
            where TJoin : class, new();

        IComparisonOperators<JoinTwoTables<T, TJoin>, JoinQuery<JoinTwoTables<T, TJoin>>> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new();

        IComparisonOperators<JoinTwoTables<T, TJoin>, JoinQuery<JoinTwoTables<T, TJoin>>> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new();
    }
}
