using GSqlQuery.Queries;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    /// <summary>
    /// Comparison Operators Extension
    /// </summary>
    public static class ComparisonOperatorsExtension
    {
        #region JoinTwoTables
        /// <summary>
        /// Adds a new column to the query.
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type </typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IComparisonOperators interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="criteriaEnum">Join Criteria Type</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> AddColumn<T1, T2, TReturn, TDbConnection, TProperties>(IComparisonOperators<Join<T1, T2>, TReturn, ConnectionOptions<TDbConnection>> joinQueryBuilderWith, Expression<Func<Join<T1, T2>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            if (field1 == null)
            {
                throw new ArgumentNullException(nameof(field1), ErrorMessages.ParameterNotNull);
            }

            if (field2 == null)
            {
                throw new ArgumentNullException(nameof(field2), ErrorMessages.ParameterNotNull);
            }

            if ( joinQueryBuilderWith is Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> result)
            {
                return JoinQueryBuilderWithWhereExtension.AddColumn(result, field1, criteriaEnum, field2);
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        /// <summary>
        /// Add the equals statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IComparisonOperators interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> Equal<T1, T2, TReturn, TDbConnection, TProperties>
            (this IComparisonOperators<Join<T1, T2>, TReturn, ConnectionOptions<TDbConnection>> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.Equal, field2);
        }

        /// <summary>
        /// Add the not equals statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IComparisonOperators interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> NotEqual<T1, T2, TReturn, TDbConnection, TProperties> (this IComparisonOperators<Join<T1, T2>, TReturn, ConnectionOptions<TDbConnection>> joinQueryBuilderWith, Expression<Func<Join<T1, T2>, TProperties>> field1, Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.NotEqual, field2);
        }

        /// <summary>
        /// Add the greater than statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IComparisonOperators interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> GreaterThan<T1, T2, TReturn, TDbConnection, TProperties>
            (this IComparisonOperators<Join<T1, T2>, TReturn, ConnectionOptions<TDbConnection>> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.GreaterThan, field2);
        }

        /// <summary>
        /// Add the less than statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IComparisonOperators interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> LessThan<T1, T2, TReturn, TDbConnection, TProperties>
            (this IComparisonOperators<Join<T1, T2>, TReturn, ConnectionOptions<TDbConnection>> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.LessThan, field2);
        }

        /// <summary>
        /// Add the greater than or equal statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IComparisonOperators interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> GreaterThanOrEqual<T1, T2, TReturn, TDbConnection, TProperties>
            (this IComparisonOperators<Join<T1, T2>, TReturn, ConnectionOptions<TDbConnection>> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        /// <summary>
        /// Add the less than or equal statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IComparisonOperators interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> LessThanOrEqual<T1, T2, TReturn, TDbConnection, TProperties>
            (this IComparisonOperators<Join<T1, T2>, TReturn, ConnectionOptions<TDbConnection>> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.LessThanOrEqual, field2);
        }
        #endregion

        #region JoinThreeTables

        /// <summary>
        /// Adds a new column to the query.
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IComparisonOperators interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="criteriaEnum">Join Criteria Type</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection>
            AddColumn<T1, T2, T3, TReturn, TDbConnection, TProperties>(IComparisonOperators<Join<T1, T2, T3>, TReturn, ConnectionOptions<TDbConnection>> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            if (field1 == null)
            {
                throw new ArgumentNullException(nameof(field1), ErrorMessages.ParameterNotNull);
            }

            if (field2 == null)
            {
                throw new ArgumentNullException(nameof(field2), ErrorMessages.ParameterNotNull);
            }

            if (joinQueryBuilderWith is Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> result )
            {
                return JoinQueryBuilderWithWhereExtension.AddColumn(result, field1, criteriaEnum, field2);
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        /// <summary>
        /// Add the equals statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IComparisonOperators interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> Equal<T1, T2, T3, TReturn, TDbConnection, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn, ConnectionOptions<TDbConnection>> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.Equal, field2);
        }

        /// <summary>
        /// Add the not equals statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IComparisonOperators interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> NotEqual<T1, T2, T3, TReturn, TDbConnection, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn, ConnectionOptions<TDbConnection>> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.NotEqual, field2);
        }

        /// <summary>
        /// Add the greate than statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IComparisonOperators interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> GreaterThan<T1, T2, T3, TReturn, TDbConnection, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn, ConnectionOptions<TDbConnection>> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.GreaterThan, field2);
        }

        /// <summary>
        /// Add the less than statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IComparisonOperators interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> LessThan<T1, T2, T3, TReturn, TDbConnection, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn, ConnectionOptions<TDbConnection>> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.LessThan, field2);
        }

        /// <summary>
        /// Add the greater than or equal statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IComparisonOperators interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> GreaterThanOrEqual<T1, T2, T3, TReturn, TDbConnection, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn, ConnectionOptions<TDbConnection>> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        /// <summary>
        /// Add the less than or equal statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IComparisonOperators interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> LessThanOrEqual<T1, T2, T3, TReturn, TDbConnection, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn, ConnectionOptions<TDbConnection>> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        #endregion
    }
}