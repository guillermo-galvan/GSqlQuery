﻿using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Select Query Builder
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    internal abstract class SelectQueryBuilder<T, TReturn, TQueryOptions> : QueryBuilderWithCriteria<T, TReturn, TQueryOptions>
        where T : class
        where TReturn : IQuery<T,TQueryOptions>
        where TQueryOptions : QueryOptions
    {
        protected readonly DynamicQuery _dynamicQuery;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="formats">Formats</param>
        public SelectQueryBuilder(DynamicQuery dynamicQuery, TQueryOptions queryOptions)
           : base(queryOptions)
        {
            _dynamicQuery = dynamicQuery ?? throw new ArgumentNullException(nameof(dynamicQuery));
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="formats">Formats</param>
        public SelectQueryBuilder(TQueryOptions queryOptions)
           : base(queryOptions)
        { }

        /// <summary>
        /// Create query
        /// </summary>
        /// <returns>Query Text</returns>
        internal string CreateQuery()
        {
            if (_dynamicQuery != null)
            {
                ClassOptionsTupla<PropertyOptionsCollection> options = ExpressionExtension.GeTQueryOptionsAndMembersByFunc(_dynamicQuery);
                ExpressionExtension.ValidateClassOptionsTupla(QueryType.Read, options);
                Columns = options.Columns;
            }

            IEnumerable<string> columnsName = Columns.Values.Select(x => x.FormatColumnName.GetColumnName(QueryOptions.Formats, QueryType.Read));
            string columns = string.Join(",", columnsName);

            if (_andOr == null)
            {
                return ConstFormat.SELECT.Replace("{0}", columns).Replace("{1}", _tableName);
            }
            else
            {
                string criteria = GetCriteria();
                return ConstFormat.SELECTWHERE.Replace("{0}", columns).Replace("{1}", _tableName).Replace("{2}", criteria);
            }
        }
    }

    /// <summary>
    /// Select Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class SelectQueryBuilder<T> : SelectQueryBuilder<T, SelectQuery<T>, QueryOptions>,
        IJoinQueryBuilder<T, SelectQuery<T>, QueryOptions> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the SelectQueryBuilder class.
        /// </summary>
        /// <param name="selectMember">Selected Member Set</param>
        /// <param name="formats">formats</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SelectQueryBuilder(DynamicQuery dynamicQuery, QueryOptions queryOptions)
            : base(dynamicQuery, queryOptions)
        { }

        /// <summary>
        /// Initializes a new instance of the SelectQueryBuilder class.
        /// </summary>
        /// <param name="propertyOptions">PropertyOptionst</param>
        /// <param name="formats">formats</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SelectQueryBuilder(QueryOptions queryOptions)
            : base(queryOptions)
        { }

        /// <summary>
        /// Build select query
        /// </summary>
        /// <returns>SelectQuery</returns>
        public override SelectQuery<T> Build()
        {
            QueryIdentity identity = new QueryIdentity(typeof(T), QueryType.Read, QueryOptions.Formats.GetType(), _dynamicQuery?.Properties, _andOr?.GetType());

            if (QueryCache.Cache.TryGetValue(identity, out IQuery query))
            {
                return query as SelectQuery<T>;
            }
            else
            {
                string text = CreateQuery();
                SelectQuery<T> result = new SelectQuery<T>(text, Columns, _criteria, QueryOptions);
                QueryCache.Cache.Add(identity, result);
                return result;
            }
        }

        private IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, QueryOptions>, QueryOptions> Join<TJoin>(JoinType joinEnum) where TJoin : class
        {
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(TJoin));

            return _dynamicQuery == null ? new JoinQueryBuilderWithWhere<T, TJoin>(Columns, joinEnum, QueryOptions, options.PropertyOptions) : new JoinQueryBuilderWithWhere<T, TJoin>(_dynamicQuery, JoinType.Inner, QueryOptions, options.PropertyOptions);
        }

        private IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, QueryOptions>, QueryOptions> Join<TJoin>(JoinType joinEnum, Func<TJoin, object> func)
           where TJoin : class
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func), ErrorMessages.ParameterNotNull);
            }
            Type secondTable = typeof(TJoin);
            ClassOptions options = ClassOptionsFactory.GetClassOptions(secondTable);
            var result = func((TJoin)options.Entity);
            DynamicQuery  dynamicQuery = new DynamicQuery(secondTable, result.GetType());

            return _dynamicQuery == null ? new JoinQueryBuilderWithWhere<T, TJoin>(Columns, joinEnum, QueryOptions, dynamicQuery) : new JoinQueryBuilderWithWhere<T, TJoin>(_dynamicQuery, joinEnum, QueryOptions, dynamicQuery);
        }

        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, QueryOptions>, QueryOptions> InnerJoin<TJoin>() where TJoin : class
        {
            return Join<TJoin>(JoinType.Inner);
        }

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, QueryOptions>, QueryOptions> LeftJoin<TJoin>() where TJoin : class
        {
            return Join<TJoin>(JoinType.Left);
        }

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, QueryOptions>, QueryOptions> RightJoin<TJoin>() where TJoin : class
        {
            return Join<TJoin>(JoinType.Right);
        }

        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, QueryOptions>, QueryOptions> InnerJoin<TJoin>(Func<TJoin, object> func)
            where TJoin : class
        {
            return Join(JoinType.Inner, func);
        }

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, QueryOptions>, QueryOptions> LeftJoin<TJoin>(Func<TJoin, object> func) where TJoin : class
        {
            return Join(JoinType.Left, func);
        }

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, QueryOptions>, QueryOptions> RightJoin<TJoin>(Func<TJoin, object> func) where TJoin : class
        {
            return Join(JoinType.Right, func);
        }
    }
}