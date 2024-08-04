using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Join Query Builder
    /// </summary>
    /// <typeparam name="T1">Type for first table</typeparam>
    /// <typeparam name="T2">Type for second table</typeparam>
    internal class JoinQueryBuilderWithWhere<T1, T2> : JoinQueryBuilderWithWhereBase<T1, T2, Join<T1, T2>, JoinQuery<Join<T1, T2>, QueryOptions>, QueryOptions>,
        IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<Join<T1, T2>, QueryOptions>, QueryOptions>,
        IComparisonOperators<Join<T1, T2>, JoinQuery<Join<T1, T2>, QueryOptions>, QueryOptions>
        where T1 : class
        where T2 : class
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="columns">Columns</param>
        /// <param name="joinType"> Join Type</param>
        /// <param name="formats">Formats</param>
        /// <param name="columnsT2">Columns second table</param>
        public JoinQueryBuilderWithWhere(PropertyOptionsCollection columns, JoinType joinType, QueryOptions queryOptions,
            DynamicQuery dynamicQuerySecond) : base(null, queryOptions)
        {
            // Main  table
            ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(typeof(T1));
            JoinInfo joinInfo = new JoinInfo(columns, classOptions, true);
            _joinInfos.Enqueue(joinInfo);

            ClassOptions classOptions2 = ClassOptionsFactory.GetClassOptions(typeof(T2));
            _joinInfo = new JoinInfo(dynamicQuerySecond, classOptions2, joinType);

            _joinInfos.Enqueue(_joinInfo);
        }

        public JoinQueryBuilderWithWhere(PropertyOptionsCollection columns, JoinType joinType, QueryOptions queryOptions,
            PropertyOptionsCollection columnsT2) : base(null, queryOptions)
        {
            ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(typeof(T1));
            JoinInfo joinInfo = new JoinInfo(columns, classOptions, true);
            _joinInfos.Enqueue(joinInfo);

            ClassOptions classOptions2 = ClassOptionsFactory.GetClassOptions(typeof(T2));

            columnsT2 ??= classOptions2.PropertyOptions;

            _joinInfo = new JoinInfo(columnsT2, classOptions2, joinType);

            _joinInfos.Enqueue(_joinInfo);
        }

        public JoinQueryBuilderWithWhere(DynamicQuery dynamicQueryMain, JoinType joinType, QueryOptions queryOptions,
            DynamicQuery dynamicQuerySecond) : base(null, queryOptions)
        {
            ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(typeof(T1));
            JoinInfo joinInfo = new JoinInfo(dynamicQueryMain, classOptions, true);
            _joinInfos.Enqueue(joinInfo);

            ClassOptions classOptions2 = ClassOptionsFactory.GetClassOptions(typeof(T2));
            _joinInfo = new JoinInfo(dynamicQuerySecond, classOptions2, joinType);
            _joinInfos.Enqueue(_joinInfo);
        }

        public JoinQueryBuilderWithWhere(DynamicQuery dynamicQueryMain, JoinType joinType, QueryOptions queryOptions,
            PropertyOptionsCollection columnsT2) : base(null, queryOptions)
        {
            ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(typeof(T1));
            JoinInfo joinInfo = new JoinInfo(dynamicQueryMain, classOptions, true);
            _joinInfos.Enqueue(joinInfo);

            ClassOptions classOptions2 = ClassOptionsFactory.GetClassOptions(typeof(T2));
            _joinInfo = new JoinInfo(columnsT2, classOptions2, joinType);
            _joinInfos.Enqueue(_joinInfo);
        }

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Join Query</returns>
        public override JoinQuery<Join<T1, T2>, QueryOptions> Build()
        {
            string query = CreateQuery(out PropertyOptionsCollection columns);
            return new JoinQuery<Join<T1, T2>, QueryOptions>(query, ClassOptionsFactory.GetClassOptions(typeof(T1)).FormatTableName.Table, columns, _criteria, QueryOptions, ClassOptionsFactory.GetClassOptions(typeof(T2)).FormatTableName.Table);
        }

        private IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, QueryOptions>, QueryOptions> Join<TJoin>(JoinType joinEnum) where TJoin : class
        {
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(TJoin));
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, joinEnum, QueryOptions, options.PropertyOptions);
        }

        private IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, QueryOptions>, QueryOptions> Join<TJoin, TProperties>(JoinType joinEnum, Func<TJoin, TProperties> func)
            where TJoin : class
        {
            Type tmp = typeof(TJoin);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(tmp);
            var result = func((TJoin)options.Entity);
            DynamicQuery dynamicQuery = new DynamicQuery(tmp, result.GetType());
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, joinEnum, QueryOptions, dynamicQuery);
        }

        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, QueryOptions>, QueryOptions> InnerJoin<TJoin>() where TJoin : class
        {
            return Join<TJoin>(JoinType.Inner);
        }

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, QueryOptions>, QueryOptions> LeftJoin<TJoin>() where TJoin : class
        {
            return Join<TJoin>(JoinType.Left);
        }

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, QueryOptions>, QueryOptions> RightJoin<TJoin>() where TJoin : class
        {
            return Join<TJoin>(JoinType.Right);
        }

        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, QueryOptions>, QueryOptions> InnerJoin<TJoin>(Func<TJoin, object> func)
            where TJoin : class
        {
            return Join(JoinType.Inner, func);
        }

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, QueryOptions>, QueryOptions> LeftJoin<TJoin>(Func<TJoin, object> func) where TJoin : class
        {
            return Join(JoinType.Left, func);
        }

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, QueryOptions>, QueryOptions> RightJoin<TJoin>(Func<TJoin, object> func) where TJoin : class
        {
            return Join(JoinType.Right, func);
        }
    }

    /// <summary>
    /// Join Query Builder
    /// </summary>
    /// <typeparam name="T1">Type for first table</typeparam>
    /// <typeparam name="T2">Type for second table</typeparam>
    /// <typeparam name="T3">Type for third table</typeparam>
    internal class JoinQueryBuilderWithWhere<T1, T2, T3> : JoinQueryBuilderWithWhereBase<T1, T2, T3, Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, QueryOptions>, QueryOptions>,
        IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, QueryOptions>, QueryOptions>,
        IComparisonOperators<Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, QueryOptions>, QueryOptions>
        where T1 : class
        where T2 : class
        where T3 : class
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="joinInfos">Join infos</param>
        /// <param name="joinType">Join Type</param>
        /// <param name="formats">Formats</param>
        /// <param name="columnsT3">Columns third table</param>
        public JoinQueryBuilderWithWhere(Queue<JoinInfo> joinInfos, JoinType joinType, QueryOptions queryOptions, PropertyOptionsCollection columnsT3 = null) :
            base(joinInfos, joinType, queryOptions, columnsT3)
        { }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="joinInfos">Join infos</param>
        /// <param name="joinType">Join Type</param>
        /// <param name="formats">Formats</param>
        /// <param name="columnsT3">Columns third table</param>
        public JoinQueryBuilderWithWhere(Queue<JoinInfo> joinInfos, JoinType joinType, QueryOptions queryOptions, DynamicQuery dynamicQuery = null) :
            base(joinInfos, joinType, queryOptions, dynamicQuery)
        { }

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Order by Query</returns>
        public override JoinQuery<Join<T1, T2, T3>, QueryOptions> Build()
        {
            string query = CreateQuery(out PropertyOptionsCollection columns);
            return new JoinQuery<Join<T1, T2, T3>, QueryOptions>(query, _classOptions.FormatTableName.Table, columns, _criteria, QueryOptions, ClassOptionsFactory.GetClassOptions(typeof(T2)).FormatTableName.Table, ClassOptionsFactory.GetClassOptions(typeof(T3)).FormatTableName.Table);
        }
    }

}