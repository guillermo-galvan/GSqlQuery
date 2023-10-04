using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    /// <summary>
    /// Join Query Builder
    /// </summary>
    /// <typeparam name="T">Type for first table</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TOptions">Options type</typeparam>
    public interface IJoinQueryBuilder<T, TReturn, TOptions> : IQueryBuilderWithWhere<TReturn, TOptions>, IQueryBuilderWithWhere<T, TReturn, TOptions>
        where T : class
        where TReturn : IQuery<T>
    {
        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TOptions"/>&gt;</returns>
        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, TOptions> InnerJoin<TJoin>() where TJoin : class;

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TOptions"/>&gt;</returns>
        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, TOptions> LeftJoin<TJoin>() where TJoin : class;

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TOptions"/>&gt;</returns>
        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, TOptions> RightJoin<TJoin>() where TJoin : class;

        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TOptions"/>&gt;</returns>
        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, TOptions> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class;

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TOptions"/>&gt;</returns>
        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, TOptions> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class;

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TOptions"/>&gt;</returns>
        IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, TOptions> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class;
    }
}