using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using GSqlQuery.Extensions;
using GSqlQuery.Queries;

namespace GSqlQuery
{
    public static class JoinQueryBuilderWithWhereExtension
    {
        #region JoinTwoTables
        internal static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> AddColumn<T1, T2, TReturn, TDbConnection, TProperties>(Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> joinQueryBuilderWith, string logicalOperador, Expression<Func<Join<T1, T2>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2>, TProperties>> field2)
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

            if (joinQueryBuilderWith is IAddJoinCriteria<JoinModel> joinCriteria)
            {
                IAddJoinCriteriaExtension.AddColumnJoin(joinCriteria, logicalOperador, field1, criteriaEnum, field2);
                return joinQueryBuilderWith;
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        internal static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection>
            AddColumn<T1, T2, TReturn, TDbConnection, TProperties>(Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn<T1,T2, TReturn, TDbConnection, TProperties>(joinQueryBuilderWith, null, field1, criteriaEnum, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> AndEqual<T1, T2, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class 
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.Equal, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> OrEqual<T1, T2, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.Equal, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> AndNotEqual<T1, T2, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.NotEqual, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> OrNotEqual<T1, T2, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.NotEqual, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> AndGreaterThan<T1, T2, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThan, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> OrGreaterThan<T1, T2, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThan, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> AndLessThan<T1, T2, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThan, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> OrLessThan<T1, T2, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThan, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> AndGreaterThanOrEqual<T1, T2, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> OrGreaterThanOrEqual<T1, T2, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> AndLessThanOrEqual<T1, T2, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> OrLessThanOrEqual<T1, T2, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where TReturn : IQuery<Join<T1, T2>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }
#endregion

        #region ThreeTable

        internal static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection>
            AddColumn<T1, T2, T3, TReturn, TDbConnection, TProperties>(Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> joinQueryBuilderWith, string logicalOperador,
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

            if (joinQueryBuilderWith is IAddJoinCriteria<JoinModel> joinCriteria)
            {
                IAddJoinCriteriaExtension.AddColumnJoin(joinCriteria, logicalOperador, field1, criteriaEnum, field2);
                return joinQueryBuilderWith;
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        internal static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection>
            AddColumn<T1, T2, T3, TReturn, TDbConnection, TProperties>(Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, null, field1, criteriaEnum, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> AndEqual<T1, T2, T3, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.Equal, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> OrEqual<T1, T2, T3, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.Equal, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> AndNotEqual<T1, T2, T3, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.NotEqual, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> OrNotEqual<T1, T2, T3, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.NotEqual, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> AndGreaterThan<T1, T2, T3, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThan, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> OrGreaterThan<T1, T2, T3, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThan, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> AndLessThan<T1, T2, T3, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThan, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> OrLessThan<T1, T2, T3, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThan, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> AndGreaterThanOrEqual<T1, T2, T3, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> OrGreaterThanOrEqual<T1, T2, T3, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> AndLessThanOrEqual<T1, T2, T3, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        public static Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> OrLessThanOrEqual<T1, T2, T3, TReturn, TDbConnection, TProperties>(
            this Runner.IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
            where TReturn : IQuery<Join<T1, T2, T3>, ConnectionOptions<TDbConnection>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        #endregion
    }
}
