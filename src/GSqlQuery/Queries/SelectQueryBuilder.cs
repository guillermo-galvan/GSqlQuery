using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="formats">Formats</param>
        public SelectQueryBuilder(ClassOptionsTupla<PropertyOptionsCollection> classOptionsTupla, TQueryOptions queryOptions)
           : base(queryOptions)
        {
            if(classOptionsTupla == null || classOptionsTupla.Columns == null )
            {
                throw new ArgumentNullException(nameof(classOptionsTupla));
            }

            Columns = classOptionsTupla.Columns;
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
        public SelectQueryBuilder(ClassOptionsTupla<PropertyOptionsCollection> classOptionsTupla, QueryOptions queryOptions)
            : base(classOptionsTupla, queryOptions)
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
            QueryIdentity identity = new QueryIdentity(typeof(T), QueryType.Read, Columns, QueryOptions.Formats.GetType());

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

        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, QueryOptions>, QueryOptions> InnerJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T, TJoin>(Columns, JoinType.Inner, QueryOptions);
        }

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, QueryOptions>, QueryOptions> LeftJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T, TJoin>(Columns, JoinType.Left, QueryOptions);
        }

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, QueryOptions>, QueryOptions> RightJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T, TJoin>(Columns, JoinType.Right, QueryOptions);
        }

        private IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, QueryOptions>, QueryOptions> Join<TJoin, TProperties>(JoinType joinEnum, Expression<Func<TJoin, TProperties>> expression)
            where TJoin : class
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression), ErrorMessages.ParameterNotNull);
            }

            ClassOptionsTupla<PropertyOptionsCollection> options = ExpressionExtension.GeTQueryOptionsAndMembers(expression);
            ExpressionExtension.ValidateClassOptionsTupla(QueryType.Criteria, options);
            return new JoinQueryBuilderWithWhere<T, TJoin>(Columns, joinEnum, QueryOptions, options.Columns);
        }

        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, QueryOptions>, QueryOptions> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class
        {
            return Join(JoinType.Inner, expression);
        }

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, QueryOptions>, QueryOptions> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class
        {
            return Join(JoinType.Left, expression);
        }

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>, QueryOptions>, QueryOptions> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class
        {
            return Join(JoinType.Right, expression);
        }
    }
}