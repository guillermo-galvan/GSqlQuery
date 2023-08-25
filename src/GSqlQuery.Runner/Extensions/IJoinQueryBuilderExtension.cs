using GSqlQuery.Queries;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class IJoinQueryBuilderExtension
    {
        #region JoinTwoTables

        internal static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            AddColumn<T1, T2, TDbConnection, TProperties>(
            IComparisonOperators<Join<T1, T2>, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>> comparisonOperators,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            JoinCriteriaType criteriaEnum,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            if (comparisonOperators is IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> result)
            {
                ComparisonOperatorsExtension.AddColumn(comparisonOperators, field1, criteriaEnum, field2);
                return result;
            }

            throw new InvalidOperationException();
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            Equal<T1, T2, TProperties, TDbConnection>
            (this IComparisonOperators<Join<T1, T2>, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>> comparisonOperators,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(comparisonOperators, field1, JoinCriteriaType.Equal, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            NotEqual<T1, T2, TProperties, TDbConnection>
            (this IComparisonOperators<Join<T1, T2>, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>> comparisonOperators,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(comparisonOperators, field1, JoinCriteriaType.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            GreaterThan<T1, T2, TProperties, TDbConnection>
            (this IComparisonOperators<Join<T1, T2>, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>> comparisonOperators,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(comparisonOperators, field1, JoinCriteriaType.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            LessThan<T1, T2, TProperties, TDbConnection>
            (this IComparisonOperators<Join<T1, T2>, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>> comparisonOperators,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(comparisonOperators, field1, JoinCriteriaType.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            GreaterThanOrEqual<T1, T2, TProperties, TDbConnection>
            (this IComparisonOperators<Join<T1, T2>, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>> comparisonOperators,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(comparisonOperators, field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            LessThanOrEqual<T1, T2, TProperties, TDbConnection>
            (this IComparisonOperators<Join<T1, T2>, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>> comparisonOperators,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(comparisonOperators, field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        #endregion

        #region JoinThreeTables

        internal static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            AddColumn<T1, T2, T3, TProperties, TDbConnection>(
            IComparisonOperators<Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>> comparisonOperators,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            if (comparisonOperators is IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinBuilder)
            {
                IJoinQueryBuilderWithWhereExtension.AddColumn(joinBuilder, field1, criteriaEnum, field2);
                return joinBuilder;
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            Equal<T1, T2, T3, TProperties, TDbConnection>
            (this IComparisonOperators<Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>> comparisonOperators,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(comparisonOperators, field1, JoinCriteriaType.Equal, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            NotEqual<T1, T2, T3, TProperties, TDbConnection>
            (this IComparisonOperators<Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>> comparisonOperators,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(comparisonOperators, field1, JoinCriteriaType.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            GreaterThan<T1, T2, T3, TProperties, TDbConnection>
            (this IComparisonOperators<Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>> comparisonOperators,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(comparisonOperators, field1, JoinCriteriaType.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            LessThan<T1, T2, T3, TProperties, TDbConnection>
            (this IComparisonOperators<Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>> comparisonOperators,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(comparisonOperators, field1, JoinCriteriaType.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            GreaterThanOrEqual<T1, T2, T3, TProperties, TDbConnection>
            (this IComparisonOperators<Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>> comparisonOperators,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(comparisonOperators, field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            LessThanOrEqual<T1, T2, T3, TProperties, TDbConnection>
            (this IComparisonOperators<Join<T1, T2, T3>, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>> comparisonOperators,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(comparisonOperators, field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        #endregion
    }
}