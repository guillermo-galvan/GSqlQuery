using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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
        public JoinQueryBuilderWithWhere(IEnumerable<PropertyOptions> columns, JoinType joinType, QueryOptions queryOptions,
            IEnumerable<PropertyOptions> columnsT2 = null) : base(null, queryOptions)
        {
            ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(typeof(T1));
            JoinInfo joinInfo = new JoinInfo(columns, classOptions, true);
            _joinInfos.Enqueue(joinInfo);

            ClassOptions classOptions2 = ClassOptionsFactory.GetClassOptions(typeof(T2));

            columnsT2 ??= classOptions2.PropertyOptions.Values;

            _joinInfo = new JoinInfo(columnsT2, classOptions2, joinType);

            _joinInfos.Enqueue(_joinInfo);

            Columns = _joinInfos.SelectMany(x => x.Columns);
        }

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Join Query</returns>
        public override JoinQuery<Join<T1, T2>, QueryOptions> Build()
        {
            string query = CreateQuery();
            return new JoinQuery<Join<T1, T2>, QueryOptions>(query, Columns, _criteria, QueryOptions);
        }

        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, QueryOptions>, QueryOptions> InnerJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, JoinType.Inner, QueryOptions);
        }

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, QueryOptions>, QueryOptions> LeftJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, JoinType.Left, QueryOptions);
        }

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, QueryOptions>, QueryOptions> RightJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, JoinType.Right, QueryOptions);
        }

        private IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, QueryOptions>, QueryOptions> Join<TJoin, TProperties>(JoinType joinEnum, Expression<Func<TJoin, TProperties>> expression)
            where TJoin : class
        {
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = ExpressionExtension.GeTQueryOptionsAndMembers(expression);
            ExpressionExtension.ValidateMemberInfos(QueryType.Criteria, options);
            IEnumerable<string> selectMember = options.MemberInfo.Select(x => x.Name);
            if (selectMember == null)
            {
                throw new ArgumentNullException(nameof(selectMember));
            }
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, joinEnum, QueryOptions, ExpressionExtension.GetPropertyQuery(options));
        }

        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, QueryOptions>, QueryOptions> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class
        {
            return Join(JoinType.Inner, expression);
        }

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, QueryOptions>, QueryOptions> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class
        {
            return Join(JoinType.Left, expression);
        }

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>, QueryOptions>, QueryOptions> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class
        {
            return Join(JoinType.Right, expression);
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
        public JoinQueryBuilderWithWhere(Queue<JoinInfo> joinInfos, JoinType joinType, QueryOptions queryOptions, IEnumerable<PropertyOptions> columnsT3 = null) :
            base(joinInfos, joinType, queryOptions, columnsT3)
        { }

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Order by Query</returns>
        public override JoinQuery<Join<T1, T2, T3>, QueryOptions> Build()
        {
            string query = CreateQuery();
            return new JoinQuery<Join<T1, T2, T3>, QueryOptions>(query, Columns, _criteria, QueryOptions);
        }
    }

}