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
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="logicalOperador">logicalOperador</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="criteriaEnum">Join Criteria Type</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions>
            AddColumn<T1, T2, TReturn, TOptions, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith, string logicalOperador,
            Expression<Func<Join<T1, T2>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>>
        {
            if (joinQueryBuilderWith is IAddJoinCriteria<JoinModel> joinCriteria)
            {
                IAddJoinCriteriaExtension.AddColumnJoin(joinCriteria,logicalOperador, field1, criteriaEnum, field2);
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
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="criteriaEnum">Join Criteria Type</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions>
            AddColumn<T1, T2, TReturn, TOptions, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, null, field1, criteriaEnum, field2);
        }

        /// <summary>
        /// Add the and equals statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> AndEqual<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.Equal, field2);
        }

        /// <summary>
        /// Add the or equals statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> OrEqual<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.Equal, field2);
        }

        /// <summary>
        /// Add the and not equals statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> AndNotEqual<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.NotEqual, field2);
        }

        /// <summary>
        /// Add the or not equals statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> OrNotEqual<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.NotEqual, field2);
        }

        /// <summary>
        /// Add the and greater than statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> AndGreaterThan<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThan, field2);
        }

        /// <summary>
        /// Add the or greater than statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> OrGreaterThan<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThan, field2);
        }

        /// <summary>
        /// Add the and less than statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> AndLessThan<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThan, field2);
        }

        /// <summary>
        /// Add the or less than statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> OrLessThan<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThan, field2);
        }

        /// <summary>
        /// Add the and greater than or equal statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> AndGreaterThanOrEqual<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        /// <summary>
        /// Add the or greater than or equal statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> OrGreaterThanOrEqual<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        /// <summary>
        /// Add the and less than or equal statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> AndLessThanOrEqual<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        /// <summary>
        /// Add the or less than or equal statement to the query
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TReturn">Join Query</typeparam>
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> OrLessThanOrEqual<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>>
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
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="logicalOperador">logicalOperador</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="criteriaEnum">Join Criteria Type</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions>
            AddColumn<T1, T2, T3, TReturn, TOptions, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith, string logicalOperador,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
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
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="criteriaEnum">Join Criteria Type</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions>
            AddColumn<T1, T2, T3, TReturn, TOptions, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>>
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
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> AndEqual<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>>
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
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> OrEqual<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>>
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
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> AndNotEqual<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>>
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
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> OrNotEqual<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>>
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
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> AndGreaterThan<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>>
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
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> OrGreaterThan<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>>
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
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> AndLessThan<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>>
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
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> OrLessThan<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>>
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
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> AndGreaterThanOrEqual<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>>
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
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> OrGreaterThanOrEqual<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>>
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
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> AndLessThanOrEqual<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>>
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
        /// <typeparam name="TOptions">Options type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinQueryBuilderWith">Implementation of the IJoinQueryBuilderWithWhere interface</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="field2">Expression for field 2</param>
        /// <returns>IJoinQueryBuilderWithWhere&lt;<typeparamref name="T1"/>,<typeparamref name="T2"/>,<typeparamref name="T3"/>,<typeparamref name="TReturn"/>,<typeparamref name="TOptions"/>&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> OrLessThanOrEqual<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        #endregion

    }
}