using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class IJoinQueryBuilderWithWhereExtension
    {
        #region JoinTwoTables
        internal static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>>
            AddColumn<T1, T2, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1, JoinCriteriaEnum criteriaEnum, Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, null, field1, criteriaEnum, field2);
        }

        internal static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>>
            AddColumn<T1, T2, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith, string logicalOperador,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1, JoinCriteriaEnum criteriaEnum, Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            if (joinQueryBuilderWith is IAddJoinCriteria<JoinModel> joinCriteria)
            {
                joinCriteria.AddColumnJoin(logicalOperador, field1, criteriaEnum, field2);
                return joinQueryBuilderWith;
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> AndEqual<T1, T2, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith,"AND", field1, JoinCriteriaEnum.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> OrEqual<T1, T2, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> AndNotEqual<T1, T2, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> OrNotEqual<T1, T2, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> AndGreaterThan<T1, T2, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> OrGreaterThan<T1, T2, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> AndLessThan<T1, T2, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> OrLessThan<T1, T2, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> AndGreaterThanOrEqual<T1, T2, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> OrGreaterThanOrEqual<T1, T2, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> AndLessThanOrEqual<T1, T2, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.LessThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> OrLessThanOrEqual<T1, T2, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<JoinTwoTables<T1, T2>>> joinQueryBuilderWith,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field1,
            Expression<Func<JoinTwoTables<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.LessThanOrEqual, field2);
        }
        #endregion

        #region ThreeTable
        internal static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>>
            AddColumn<T1, T2, T3, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1, JoinCriteriaEnum criteriaEnum, Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, null, field1, criteriaEnum, field2);
        }

        internal static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>>
            AddColumn<T1, T2, T3,TProperties>(IJoinQueryBuilderWithWhere<T1, T2,T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith, string logicalOperador,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1, JoinCriteriaEnum criteriaEnum, Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            if (joinQueryBuilderWith is IAddJoinCriteria<JoinModel> joinCriteria)
            {
                joinCriteria.AddColumnJoin(logicalOperador, field1, criteriaEnum, field2);
                return joinQueryBuilderWith;
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> AndEqual<T1, T2, T3, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> OrEqual<T1, T2, T3, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> AndNotEqual<T1, T2, T3, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> OrNotEqual<T1, T2, T3, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> AndGreaterThan<T1, T2, T3, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> OrGreaterThan<T1, T2, T3, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> AndLessThan<T1, T2, T3, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> OrLessThan<T1, T2, T3, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> AndGreaterThanOrEqual<T1, T2, T3, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> OrGreaterThanOrEqual<T1, T2, T3, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> AndLessThanOrEqual<T1, T2, T3, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.LessThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> OrLessThanOrEqual<T1, T2, T3, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<JoinThreeTables<T1, T2, T3>>> joinQueryBuilderWith,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field1,
            Expression<Func<JoinThreeTables<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.LessThanOrEqual, field2);
        }

        #endregion

    }
}
