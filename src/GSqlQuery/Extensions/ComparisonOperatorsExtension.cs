using GSqlQuery.Queries;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class ComparisonOperatorsExtension
    {
        #region JoinTwoTables

        internal static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> 
            AddColumn<T1, T2, TProperties>(IComparisonOperators<JoinTwoTables<T1, T2>, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1, JoinCriteriaEnum criteriaEnum, Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            if (joinQueryBuilderWith is IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> joinBuilder)
            {
                return IJoinQueryBuilderWithWhereExtension.AddColumn(joinBuilder, field1, criteriaEnum,field2);
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> Equal<T1, T2, TProperties>
            (this IComparisonOperators<JoinTwoTables<T1, T2>, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith,field1, JoinCriteriaEnum.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> NotEqual<T1, T2, TProperties>
            (this IComparisonOperators<JoinTwoTables<T1, T2>, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> GreaterThan<T1, T2, TProperties>
            (this IComparisonOperators<JoinTwoTables<T1, T2>, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> LessThan<T1, T2, TProperties>
            (this IComparisonOperators<JoinTwoTables<T1, T2>, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> GreaterThanOrEqual<T1, T2, TProperties>
            (this IComparisonOperators<JoinTwoTables<T1, T2>, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> LessThanOrEqual<T1, T2, TProperties>
            (this IComparisonOperators<JoinTwoTables<T1, T2>, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.LessThanOrEqual, field2);
        }
        #endregion

        #region JoinThreeTables

        internal static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>>
            AddColumn<T1, T2, T3, TProperties>(IComparisonOperators<JoinThreeTables<T1, T2, T3>, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1, JoinCriteriaEnum criteriaEnum, Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            if (joinQueryBuilderWith is IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinBuilder)
            {
                return IJoinQueryBuilderWithWhereExtension.AddColumn(joinBuilder, field1, criteriaEnum, field2);
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> Equal<T1, T2, T3, TProperties>
            (this IComparisonOperators<JoinThreeTables<T1, T2, T3>, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
             Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
             Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> NotEqual<T1, T2, T3, TProperties>
            (this IComparisonOperators<JoinThreeTables<T1, T2, T3>, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
             Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
             Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> GreaterThan<T1, T2, T3, TProperties>
            (this IComparisonOperators<JoinThreeTables<T1, T2, T3>, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
             Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
             Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> LessThan<T1, T2, T3, TProperties>
            (this IComparisonOperators<JoinThreeTables<T1, T2, T3>, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
             Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
             Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> GreaterThanOrEqual<T1, T2, T3, TProperties>
            (this IComparisonOperators<JoinThreeTables<T1, T2, T3>, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
             Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
             Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> LessThanOrEqual<T1, T2, T3, TProperties>
            (this IComparisonOperators<JoinThreeTables<T1, T2, T3>, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
             Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
             Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, field1, JoinCriteriaEnum.LessThanOrEqual, field2);
        }

        #endregion
    }
}
