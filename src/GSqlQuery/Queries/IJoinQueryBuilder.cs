using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    /// <summary>
    /// Join Query Builder
    /// </summary>
    /// <typeparam name="T">Type for first table</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TQueryOptions">Options type</typeparam>
    public interface IJoinQueryBuilder<T, TReturn, TQueryOptions> : IQueryBuilderWithWhere<TReturn, TQueryOptions>, IQueryBuilderWithWhere<T, TReturn, TQueryOptions>, IQueryOptions<TQueryOptions>
        where T : class
        where TReturn : IQuery<T, TQueryOptions>
        where TQueryOptions : QueryOptions
    {
        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TQueryOptions"/>&gt;</returns>
        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TQueryOptions>, TQueryOptions> InnerJoin<TJoin>() 
            where TJoin : class;

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TQueryOptions"/>&gt;</returns>
        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TQueryOptions>, TQueryOptions> LeftJoin<TJoin>() where TJoin : class;

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TQueryOptions"/>&gt;</returns>
        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TQueryOptions>, TQueryOptions> RightJoin<TJoin>() where TJoin : class;

        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TQueryOptions"/>&gt;</returns>
        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TQueryOptions>, TQueryOptions> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class;

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TQueryOptions"/>&gt;</returns>
        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TQueryOptions>, TQueryOptions> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class;

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TQueryOptions"/>&gt;</returns>
        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, TQueryOptions>, TQueryOptions> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class;
    }
}