using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GSqlQuery.Queries
{
    internal abstract class SelectQueryBuilder<T, TReturn> : QueryBuilderWithCriteria<T, TReturn>
        where T : class, new()
        where TReturn : SelectQuery<T>
    {

        public SelectQueryBuilder(IEnumerable<string> selectMember, IStatements statements)
           : base(statements)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            Columns = ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember);
        }

        internal string CreateQuery(IStatements statements)
        {
            string result = string.Empty;

            if (_andOr == null)
            {
                result = string.Format(statements.Select,
                    string.Join(",", Columns.Select(x => x.ColumnAttribute.GetColumnName(_tableName, statements))),
                    _tableName);
            }
            else
            {
                result = string.Format(statements.SelectWhere,
                    string.Join(",", Columns.Select(x => x.ColumnAttribute.GetColumnName(_tableName, statements))),
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
        IJoinQueryBuilder<T, SelectQuery<T>, IStatements> where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the SelectQueryBuilder class.
        /// </summary>
        /// <param name="options">Detail of the class to transform</param>
        /// <param name="selectMember">Selected Member Set</param>
        /// <param name="statements">Statements to build the query</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SelectQueryBuilder(IEnumerable<string> selectMember, IStatements statements)
            : base(selectMember, statements)
        { }

        /// <summary>
        /// Build select query
        /// </summary>
        /// <returns>SelectQuery</returns>
        public override SelectQuery<T> Build()
        {
            return new SelectQuery<T>(CreateQuery(Options), Columns.Select(x => x.ColumnAttribute), _criteria, Options);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, IStatements> InnerJoin<TJoin>() where TJoin : class, new()
        {
            return new JoinQueryBuilderWithWhere<T, TJoin>(_tableName, Columns, JoinType.Inner, Options);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, IStatements> LeftJoin<TJoin>() where TJoin : class, new()
        {
            return new JoinQueryBuilderWithWhere<T, TJoin>(_tableName, Columns, JoinType.Left, Options);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, IStatements> RightJoin<TJoin>() where TJoin : class, new()
        {
            return new JoinQueryBuilderWithWhere<T, TJoin>(_tableName, Columns, JoinType.Right, Options);
        }

        private IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, IStatements> Join<TJoin, TProperties>(JoinType joinEnum, Expression<Func<TJoin, TProperties>> expression)
            where TJoin : class, new()
        {
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            options.MemberInfo.ValidateMemberInfos($"Could not infer property name for expression.");
            var selectMember = options.MemberInfo.Select(x => x.Name);
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            return new JoinQueryBuilderWithWhere<T, TJoin>(_tableName, Columns, joinEnum, Options, ClassOptionsFactory.GetClassOptions(typeof(TJoin)).GetPropertyQuery(selectMember));
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, IStatements> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new()
        {
            return Join(JoinType.Inner, expression);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, IStatements> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class, new()
        {
            return Join(JoinType.Left, expression);
        }

        public IComparisonOperators<Join<T, TJoin>, JoinQuery<Join<T, TJoin>>, IStatements> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression) where TJoin : class, new()
        {
            return Join(JoinType.Right, expression);
        }
    }
}