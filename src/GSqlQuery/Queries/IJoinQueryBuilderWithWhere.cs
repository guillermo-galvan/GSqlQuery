using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    /// <summary>
    /// Join Query Builder
    /// </summary>
    /// <typeparam name="T1">Type for first table</typeparam>
    /// <typeparam name="T2">Type for second table</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TQueryOptions">Options type</typeparam>
    public interface IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> : IQueryBuilderWithWhere<TReturn, TQueryOptions>,
        IQueryBuilderWithWhere<Join<T1, T2>, TReturn, TQueryOptions>, IQueryOptions<TQueryOptions>
        where T1 : class
        where T2 : class
        where TReturn : IQuery<Join<T1, T2>, TQueryOptions>
        where TQueryOptions : QueryOptions
    {
        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TQueryOptions"/>&gt;</returns>
        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TQueryOptions>, TQueryOptions> InnerJoin<TJoin>() where TJoin : class;

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TQueryOptions"/>&gt;</returns>
        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TQueryOptions>, TQueryOptions> LeftJoin<TJoin>() where TJoin : class;

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TQueryOptions"/>&gt;</returns>
        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TQueryOptions>, TQueryOptions> RightJoin<TJoin>() where TJoin : class;

        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TQueryOptions"/>&gt;</returns>
        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TQueryOptions>, TQueryOptions> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class;

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TQueryOptions"/>&gt;</returns>
        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TQueryOptions>, TQueryOptions> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class;

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TQueryOptions"/>&gt;</returns>
        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, TQueryOptions>, TQueryOptions> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class;
    }

    /// <summary>
    /// Join Query Builder
    /// </summary>
    /// <typeparam name="T1">Type for first table</typeparam>
    /// <typeparam name="T2">Type for second table</typeparam>
    /// <typeparam name="T3">Type for third table</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TQueryOptions">Options type</typeparam>
    public interface IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> : IQueryBuilderWithWhere<TReturn, TQueryOptions>,
        IQueryBuilderWithWhere<Join<T1, T2, T3>, TReturn, TQueryOptions>
        where T1 : class
        where T2 : class
        where T3 : class
        where TReturn : IQuery<Join<T1, T2, T3>, TQueryOptions>
        where TQueryOptions : QueryOptions
    {
        new IWhere<Join<T1, T2, T3>, TReturn, TQueryOptions> Where();
    }
}