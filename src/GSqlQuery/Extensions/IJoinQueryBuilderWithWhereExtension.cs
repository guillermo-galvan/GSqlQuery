using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class IJoinQueryBuilderWithWhereExtension
    {
        #region JoinTwoTables
        internal static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions>
            AddColumn<T1, T2, TReturn, TOptions, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, null, field1, criteriaEnum, field2);
        }

        internal static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions>
            AddColumn<T1, T2, TReturn, TOptions, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith, string logicalOperador,
            Expression<Func<Join<T1, T2>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            if (joinQueryBuilderWith is IAddJoinCriteria<JoinModel> joinCriteria)
            {
                joinCriteria.AddColumnJoin(logicalOperador, field1, criteriaEnum, field2);
                return joinQueryBuilderWith;
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> AndEqual<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> OrEqual<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> AndNotEqual<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> OrNotEqual<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> AndGreaterThan<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> OrGreaterThan<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> AndLessThan<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> OrLessThan<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> AndGreaterThanOrEqual<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> OrGreaterThanOrEqual<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> AndLessThanOrEqual<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> OrLessThanOrEqual<T1, T2, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }
        #endregion

        #region ThreeTable
        internal static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions>
            AddColumn<T1, T2, T3, TReturn, TOptions, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, null, field1, criteriaEnum, field2);
        }

        internal static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions>
            AddColumn<T1, T2, T3, TReturn, TOptions, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith, string logicalOperador,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            if (joinQueryBuilderWith is IAddJoinCriteria<JoinModel> joinCriteria)
            {
                joinCriteria.AddColumnJoin(logicalOperador, field1, criteriaEnum, field2);
                return joinQueryBuilderWith;
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> AndEqual<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> OrEqual<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> AndNotEqual<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> OrNotEqual<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> AndGreaterThan<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> OrGreaterThan<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> AndLessThan<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> OrLessThan<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> AndGreaterThanOrEqual<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> OrGreaterThanOrEqual<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> AndLessThanOrEqual<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> OrLessThanOrEqual<T1, T2, T3, TReturn, TOptions, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        #endregion

    }
}