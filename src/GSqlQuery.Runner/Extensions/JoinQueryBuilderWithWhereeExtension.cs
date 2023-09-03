using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class JoinQueryBuilderWithWhereeExtension
    {
        #region JoinTwoTables

        internal static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            AddColumn<T1, T2, TProperties, TDbConnection>
            (IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            string logicalOperador, Expression<Func<Join<T1, T2>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            if (joinQueryBuilderWith is IJoinQueryBuilderWithWhere<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>> tmp)
            {
                IJoinQueryBuilderWithWhereExtension.AddColumn(tmp, logicalOperador, field1, criteriaEnum, field2);
                return joinQueryBuilderWith;
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
             AndEqual<T1, T2, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.Equal, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            OrEqual<T1, T2, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.Equal, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            AndNotEqual<T1, T2, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            OrNotEqual<T1, T2, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            AndGreaterThan<T1, T2, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            OrGreaterThan<T1, T2, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            AndLessThan<T1, T2, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            OrLessThan<T1, T2, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            AndGreaterThanOrEqual<T1, T2, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            OrGreaterThanOrEqual<T1, T2, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            AndLessThanOrEqual<T1, T2, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            OrLessThanOrEqual<T1, T2, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, JoinQuery<Join<T1, T2>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }
        #endregion

        #region ThreeTable

        internal static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            AddColumn<T1, T2, T3, TProperties, TDbConnection>
            (IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            string logicalOperador, Expression<Func<Join<T1, T2, T3>, TProperties>> field1, JoinCriteriaType criteriaEnum, Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            if (joinQueryBuilderWith is IJoinQueryBuilderWithWhere<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>> tmp)
            {
                IJoinQueryBuilderWithWhereExtension.AddColumn(tmp, logicalOperador, field1, criteriaEnum, field2);
                return joinQueryBuilderWith;
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            AndEqual<T1, T2, T3, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.Equal, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            OrEqual<T1, T2, T3, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.Equal, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            AndNotEqual<T1, T2, T3, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            OrNotEqual<T1, T2, T3, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            AndGreaterThan<T1, T2, T3, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            OrGreaterThan<T1, T2, T3, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            AndLessThan<T1, T2, T3, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            OrLessThan<T1, T2, T3, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            AndGreaterThanOrEqual<T1, T2, T3, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            OrGreaterThanOrEqual<T1, T2, T3, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            AndLessThanOrEqua<T1, T2, T3, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection>
            OrLessThanOrEqual<T1, T2, T3, TProperties, TDbConnection>
            (this IJoinQueryBuilderWithWheree<T1, T2, T3, JoinQuery<Join<T1, T2, T3>, TDbConnection>, ConnectionOptions<TDbConnection>, TDbConnection> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaType.LessThanOrEqual, field2);
        }

        #endregion
    }
}