using GSqlQuery.Queries;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class ComparisonOperatorsExtension
    {
        #region JoinTwoTables

        internal static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions>
            AddColumn<T1, T2, TReturn, TOptions, TProperties>(IComparisonOperators<Join<T1, T2>, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            if (joinQueryBuilderWith is IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> joinBuilder)
            {
                return IJoinQueryBuilderWithWhereExtension.AddColumn(joinBuilder, field1, criteriaEnum, field2);
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> Equal<T1, T2, TReturn, TOptions, TProperties>
            (this IComparisonOperators<Join<T1, T2>, TReturn, TOptions> comparisonOperators,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(comparisonOperators, field1, JoinCriteriaType.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> NotEqual<T1, T2, TReturn, TOptions, TProperties>
            (this IComparisonOperators<Join<T1, T2>, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> GreaterThan<T1, T2, TReturn, TOptions, TProperties>
            (this IComparisonOperators<Join<T1, T2>, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> LessThan<T1, T2, TReturn, TOptions, TProperties>
            (this IComparisonOperators<Join<T1, T2>, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> GreaterThanOrEqual<T1, T2, TReturn, TOptions, TProperties>
            (this IComparisonOperators<Join<T1, T2>, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn, TOptions> LessThanOrEqual<T1, T2, TReturn, TOptions, TProperties>
            (this IComparisonOperators<Join<T1, T2>, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery<Join<T1, T2>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.LessThanOrEqual, field2);
        }
        #endregion

        #region JoinThreeTables

        internal static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions>
            AddColumn<T1, T2, T3, TReturn, TOptions, TProperties>(IComparisonOperators<Join<T1, T2, T3>, TReturn, TOptions> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            if (joinQueryBuilderWith is IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> joinBuilder)
            {
                return IJoinQueryBuilderWithWhereExtension.AddColumn(joinBuilder, field1, criteriaEnum, field2);
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> Equal<T1, T2, T3, TReturn, TOptions, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn, TOptions> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> NotEqual<T1, T2, T3, TReturn, TOptions, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn, TOptions> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> GreaterThan<T1, T2, T3, TReturn, TOptions, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn, TOptions> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> LessThan<T1, T2, T3, TReturn, TOptions, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn, TOptions> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> GreaterThanOrEqual<T1, T2, T3, TReturn, TOptions, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn, TOptions> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn, TOptions> LessThanOrEqual<T1, T2, T3, TReturn, TOptions, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn, TOptions> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery<Join<T1, T2, T3>>
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        #endregion
    }
}