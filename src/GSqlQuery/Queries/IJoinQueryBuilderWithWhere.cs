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
    /// <typeparam name="TOptions">Options type</typeparam>
    public interface IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> : IQueryBuilderWithWhere<TReturn, TOptions>, IQueryBuilderWithWhere<Join<T1, T2>, TReturn, TOptions>
        where T1 : class
        where T2 : class
        where TReturn : IQuery<Join<T1, T2>>
    {
        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TOptions"/>&gt;</returns>
        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, TOptions> InnerJoin<TJoin>() where TJoin : class;

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TOptions"/>&gt;</returns>
        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, TOptions> LeftJoin<TJoin>() where TJoin : class;

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TOptions"/>&gt;</returns>
        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, TOptions> RightJoin<TJoin>() where TJoin : class;

        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TOptions"/>&gt;</returns>
        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, TOptions> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class;

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TOptions"/>&gt;</returns>
        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, TOptions> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class;

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,<typeparamref name="TOptions"/>&gt;</returns>
        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, TOptions> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class;
    }

    /// <summary>
    /// Join Query Builder
    /// </summary>
    /// <typeparam name="T1">Type for first table</typeparam>
    /// <typeparam name="T2">Type for second table</typeparam>
    /// <typeparam name="T3">Type for third table</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    /// <typeparam name="TOptions">Options type</typeparam>
    public interface IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> : IQueryBuilderWithWhere<TReturn, TOptions>, 
        IQueryBuilderWithWhere<Join<T1, T2, T3>, TReturn, TOptions>
        where T1 : class
        where T2 : class
        where T3 : class
        where TReturn : IQuery<Join<T1, T2, T3>>
    {
        new IWhere<Join<T1, T2, T3>, TReturn> Where();
    }
}