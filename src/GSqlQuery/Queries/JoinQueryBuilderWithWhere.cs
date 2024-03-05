﻿using GSqlQuery.Extensions;
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
    internal class JoinQueryBuilderWithWhere<T1, T2> : JoinQueryBuilderWithWhereBase<T1, T2, Join<T1, T2>, JoinQuery<Join<T1, T2>>, IFormats>,
        IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<Join<T1, T2>>, IFormats>
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
        public JoinQueryBuilderWithWhere(IEnumerable<PropertyOptions> columns, JoinType joinType, IFormats formats,
            IEnumerable<PropertyOptions> columnsT2 = null) : base(null, formats, formats)
        {
            ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(typeof(T1));
            JoinInfo joinInfo = new JoinInfo(columns, classOptions, true);
            _joinInfos.Enqueue(joinInfo);

            ClassOptions classOptions2 = ClassOptionsFactory.GetClassOptions(typeof(T2));

            columnsT2 ??= classOptions2.PropertyOptions;

            _joinInfo = new JoinInfo(columnsT2, classOptions2,  joinType);

            _joinInfos.Enqueue(_joinInfo);

            Columns = _joinInfos.SelectMany(x => x.Columns);
        }

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Join Query</returns>
        public override JoinQuery<Join<T1, T2>> Build()
        {
            string query = CreateQuery();
            return new JoinQuery<Join<T1, T2>>(query, Columns, _criteria, Options);
        }

        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IFormats> InnerJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, JoinType.Inner, Options);
        }

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IFormats> LeftJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, JoinType.Left, Options);
        }

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IFormats> RightJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, JoinType.Right, Options);
        }

        private IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IFormats> Join<TJoin, TProperties>(JoinType joinEnum, Expression<Func<TJoin, TProperties>> expression)
            where TJoin : class
        {
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = GeneralExtension.GetOptionsAndMembers(expression);
            GeneralExtension.ValidateMemberInfos(QueryType.Criteria, options);
            IEnumerable<string> selectMember = options.MemberInfo.Select(x => x.Name);
            if (selectMember == null)
            {
                throw new ArgumentNullException(nameof(selectMember));
            }
            return new JoinQueryBuilderWithWhere<T1, T2, TJoin>(_joinInfos, joinEnum, Options, GeneralExtension.GetPropertyQuery(ClassOptionsFactory.GetClassOptions(typeof(TJoin)),selectMember));
        }

        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IFormats> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class
        {
            return Join(JoinType.Inner, expression);
        }

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IFormats> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class
        {
            return Join(JoinType.Left, expression);
        }

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for third table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TJoin"/>&gt;&gt;,IFormats&gt;</returns>
        public IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>, IFormats> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class
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
    internal class JoinQueryBuilderWithWhere<T1, T2, T3> : JoinQueryBuilderWithWhereBase<T1, T2, T3, Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>>, IFormats>,
        IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<Join<T1, T2, T3>>, IFormats>
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
        public JoinQueryBuilderWithWhere(Queue<JoinInfo> joinInfos, JoinType joinType, IFormats formats, IEnumerable<PropertyOptions> columnsT3 = null) :
            base(joinInfos, joinType, formats, formats, columnsT3)
        { }

        /// <summary>
        /// Build the query
        /// </summary>
        /// <returns>Order by Query</returns>
        public override JoinQuery<Join<T1, T2, T3>> Build()
        {
            string query = CreateQuery();
            return new JoinQuery<Join<T1, T2, T3>>(query, Columns, _criteria, Options);
        }
    }

}