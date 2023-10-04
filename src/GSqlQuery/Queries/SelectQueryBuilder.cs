using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Select Query Builder
    /// </summary>
    /// <typeparam name="T">Type to create the query</typeparam>
    /// <typeparam name="TReturn">Query</typeparam>
    internal abstract class SelectQueryBuilder<T, TReturn> : QueryBuilderWithCriteria<T, TReturn>
        where T : class
        where TReturn : SelectQuery<T>
    {

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="selectMember">Name of properties to search</param>
        /// <param name="formats">Formats</param>
        public SelectQueryBuilder(IEnumerable<string> selectMember, IFormats formats)
           : base(formats)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            Columns = ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember);
        }

        /// <summary>
        /// Create query
        /// </summary>
        /// <returns>Query Text</returns>
        internal string CreateQuery()
        {
            string result = string.Empty;

            if (_andOr == null)
            {
                result = string.Format(ConstFormat.SELECT,
                    string.Join(",", Columns.Select(x => x.ColumnAttribute.GetColumnName(_tableName, Options, QueryType.Read))),
                    _tableName);
            }
            else
            {
                result = string.Format(ConstFormat.SELECTWHERE,
                    string.Join(",", Columns.Select(x => x.ColumnAttribute.GetColumnName(_tableName, Options, QueryType.Read))),
                    _tableName, GetCriteria());
            }

            return result;
        }
    }

    /// <summary>
    /// Select Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class SelectQueryBuilder<T> : SelectQueryBuilder<T, SelectQuery<T>>,
        IJoinQueryBuilder<T, SelectQuery<T>, IFormats> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the SelectQueryBuilder class.
        /// </summary>
        /// <param name="selectMember">Selected Member Set</param>
        /// <param name="formats">formats</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SelectQueryBuilder(IEnumerable<string> selectMember, IFormats formats)
            : base(selectMember, formats)
        { }

        /// <summary>
        /// Build select query
        /// </summary>
        /// <returns>SelectQuery</returns>
        public override SelectQuery<T> Build()
        {
            return new SelectQuery<T>(CreateQuery(), Columns, _criteria, Options);
        }

        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, IFormats> InnerJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T, TJoin>(_tableName, Columns, JoinType.Inner, Options);
        }

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, IFormats> LeftJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T, TJoin>(_tableName, Columns, JoinType.Left, Options);
        }

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, IFormats> RightJoin<TJoin>() where TJoin : class
        {
            return new JoinQueryBuilderWithWhere<T, TJoin>(_tableName, Columns, JoinType.Right, Options);
        }

        private IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, IFormats> Join<TJoin, TProperties>(JoinType joinEnum, Expression<Func<TJoin, TProperties>> expression)
            where TJoin : class
        {
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            var selectMember = options.MemberInfo.Select(x => x.Name);
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            return new JoinQueryBuilderWithWhere<T, TJoin>(_tableName, Columns, joinEnum, Options, ClassOptionsFactory.GetClassOptions(typeof(TJoin)).GetPropertyQuery(selectMember));
        }

        /// <summary>
        /// Inner Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, IFormats> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class
        {
            return Join(JoinType.Inner, expression);
        }

        /// <summary>
        /// Left Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, IFormats> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class
        {
            return Join(JoinType.Left, expression);
        }

        /// <summary>
        /// Rigth Join query
        /// </summary>
        /// <typeparam name="TJoin">Type for second table</typeparam>
        /// <returns>IComparisonOperators&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;,JoinQuery&lt;Join&lt;<typeparamref name="T"/>,<typeparamref name="TJoin"/>&gt;&gt;,Formats&gt;</returns>
        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, IFormats> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class
        {
            return Join(JoinType.Right, expression);
        }
    }
}