using GSqlQuery.Queries;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class ComparisonOperatorsExtension
    {
        #region JoinTwoTables

        internal static IJoinQueryBuilderWithWhere<T1, T2, TReturn> 
            AddColumn<T1, T2, TReturn,TProperties>(IComparisonOperators<Join<T1, T2>, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1, JoinCriteriaEnum criteriaEnum, Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            if (joinQueryBuilderWith is IJoinQueryBuilderWithWhere<T1, T2, TReturn> joinBuilder)
            {
                return IJoinQueryBuilderWithWhereExtension.AddColumn(joinBuilder, field1, criteriaEnum,field2);
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> Equal<T1, T2, TReturn, TProperties>
            (this IComparisonOperators<Join<T1, T2>, TReturn> comparisonOperators,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(comparisonOperators, field1, JoinCriteriaEnum.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> NotEqual<T1, T2, TReturn, TProperties>
            (this IComparisonOperators<Join<T1, T2>, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> GreaterThan<T1, T2, TReturn, TProperties>
            (this IComparisonOperators<Join<T1, T2>, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> LessThan<T1, T2, TReturn, TProperties>
            (this IComparisonOperators<Join<T1, T2>, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> GreaterThanOrEqual<T1, T2, TReturn, TProperties>
            (this IComparisonOperators<Join<T1, T2>, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> LessThanOrEqual<T1, T2, TReturn, TProperties>
            (this IComparisonOperators<Join<T1, T2>, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.LessThanOrEqual, field2);
        }
        #endregion

        #region JoinThreeTables

        internal static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn>
            AddColumn<T1, T2, T3,TReturn, TProperties>(IComparisonOperators<Join<T1, T2, T3>, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1, JoinCriteriaEnum criteriaEnum, Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            if (joinQueryBuilderWith is IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> joinBuilder)
            {
                return IJoinQueryBuilderWithWhereExtension.AddColumn(joinBuilder, field1, criteriaEnum, field2);
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> Equal<T1, T2, T3, TReturn, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> NotEqual<T1, T2, T3, TReturn, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> GreaterThan<T1, T2, T3, TReturn, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> LessThan<T1, T2, T3, TReturn, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> GreaterThanOrEqual<T1, T2, T3, TReturn, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> LessThanOrEqual<T1, T2, T3, TReturn, TProperties>
            (this IComparisonOperators<Join<T1, T2, T3>, TReturn> joinQueryBuilderWith,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
             Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.LessThanOrEqual, field2);
        }

        #endregion
    }
}
