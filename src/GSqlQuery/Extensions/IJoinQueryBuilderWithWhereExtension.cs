using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    /// <summary>
    /// IJoinQueryBuilderWithWhere Extension
    /// </summary>
    public static class IJoinQueryBuilderWithWhereExtension
    {
        #region JoinTwoTables
        /// <summary>
        /// Adds a new column to the query.
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="logicalOperador">logicalOperador</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="criteriaEnum">Join Criteria Type</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions>
            AddColumn<T1, T2, TReturn, TQueryOptions, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> joinQueryBuilderWith, string logicalOperador,
            Expression<Func<Join<T1, T2>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            if (field1 == null)
            {
                throw new ArgumentNullException(nameof(field1), ErrorMessages.ParameterNotNull);
            }

            if (field2 == null)
            {
                throw new ArgumentNullException(nameof(field2), ErrorMessages.ParameterNotNull);
            }

            if (joinQueryBuilderWith is IAddJoinCriteria<JoinModel> joinCriteria)
            {
                IAddJoinCriteriaExtension.AddColumnJoin(joinCriteria, logicalOperador, field1, criteriaEnum, field2);
                return joinQueryBuilderWith;
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        /// <summary>
        /// Adds a new column to the query.
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="criteriaEnum">Join Criteria Type</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions>
            AddColumn<T1, T2, TReturn, TQueryOptions, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, null, field1, criteriaEnum, field2);
        }

        /// <summary>
        /// Add the and equals statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> AndEqual<T1, T2, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.Equal, field2);
        }

        /// <summary>
        /// Add the or equals statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> OrEqual<T1, T2, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.Equal, field2);
        }

        /// <summary>
        /// Add the and not equals statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> AndNotEqual<T1, T2, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.NotEqual, field2);
        }

        /// <summary>
        /// Add the or not equals statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> OrNotEqual<T1, T2, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.NotEqual, field2);
        }

        /// <summary>
        /// Add the and greater than statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> AndGreaterThan<T1, T2, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThan, field2);
        }

        /// <summary>
        /// Add the or greater than statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> OrGreaterThan<T1, T2, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThan, field2);
        }

        /// <summary>
        /// Add the and less than statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> AndLessThan<T1, T2, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThan, field2);
        }

        /// <summary>
        /// Add the or less than statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> OrLessThan<T1, T2, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThan, field2);
        }

        /// <summary>
        /// Add the and greater than or equal statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> AndGreaterThanOrEqual<T1, T2, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        /// <summary>
        /// Add the or greater than or equal statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> OrGreaterThanOrEqual<T1, T2, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        /// <summary>
        /// Add the and less than or equal statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> AndLessThanOrEqual<T1, T2, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        /// <summary>
        /// Add the or less than or equal statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> OrLessThanOrEqual<T1, T2, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }
        #endregion

        #region ThreeTable
        /// <summary>
        /// Adds a new column to the query.
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="logicalOperador">logicalOperador</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="criteriaEnum">Join Criteria Type</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions>
            AddColumn<T1, T2, T3, TReturn, TQueryOptions, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> joinQueryBuilderWith, string logicalOperador,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            if (field1 == null)
            {
                throw new ArgumentNullException(nameof(field1), ErrorMessages.ParameterNotNull);
            }

            if (field2 == null)
            {
                throw new ArgumentNullException(nameof(field2), ErrorMessages.ParameterNotNull);
            }

            if (joinQueryBuilderWith is IAddJoinCriteria<JoinModel> joinCriteria)
            {
                IAddJoinCriteriaExtension.AddColumnJoin(joinCriteria, logicalOperador, field1, criteriaEnum, field2);
                return joinQueryBuilderWith;
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        /// <summary>
        /// Adds a new column to the query.
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="criteriaEnum">Join Criteria Type</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions>
            AddColumn<T1, T2, T3, TReturn, TQueryOptions, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, null, field1, criteriaEnum, field2);
        }

        /// <summary>
        /// Add the and equals statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> AndEqual<T1, T2, T3, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.Equal, field2);
        }

        /// <summary>
        /// Add the or equals statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> OrEqual<T1, T2, T3, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.Equal, field2);
        }

        /// <summary>
        /// Add the and not equals statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> AndNotEqual<T1, T2, T3, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.NotEqual, field2);
        }

        /// <summary>
        /// Add the or not equals statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> OrNotEqual<T1, T2, T3, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.NotEqual, field2);
        }

        /// <summary>
        /// Add the and greate than statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> AndGreaterThan<T1, T2, T3, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThan, field2);
        }

        /// <summary>
        /// Add the or greate than statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> OrGreaterThan<T1, T2, T3, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThan, field2);
        }

        /// <summary>
        /// Add the and less than statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> AndLessThan<T1, T2, T3, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThan, field2);
        }

        /// <summary>
        /// Add the or less than statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> OrLessThan<T1, T2, T3, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThan, field2);
        }

        /// <summary>
        /// Add the and greate than or equal statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> AndGreaterThanOrEqual<T1, T2, T3, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        /// <summary>
        /// Add the or greate than or equal statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> OrGreaterThanOrEqual<T1, T2, T3, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        /// <summary>
        /// Add the and less than or equal statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> AndLessThanOrEqual<T1, T2, T3, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        /// <summary>
        /// Add the or less than or equal statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TQueryOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TQueryOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> OrLessThanOrEqual<T1, T2, T3, TReturn, TQueryOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TQueryOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, TQueryOptions>
            where TQueryOptions : QueryOptions
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        #endregion

    }
}