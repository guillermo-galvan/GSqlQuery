using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using System;
using System.Linq.Expressions;

namespace GSqlQuery
{
    public static class IJoinQueryBuilderWithWhereExtension
    {
        #region JoinTwoTables
        internal static IJoinQueryBuilderWithWhere<T1, T2, TReturn>
            AddColumn<T1, T2, TReturn, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1, JoinCriteriaEnum criteriaEnum, Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, null, field1, criteriaEnum, field2);
        }

        internal static IJoinQueryBuilderWithWhere<T1, T2, TReturn>
            AddColumn<T1, T2, TReturn, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, TReturn> joinQueryBuilderWith, string logicalOperador,
            Expression<Func<Join<T1, T2>, TProperties>> field1, JoinCriteriaEnum criteriaEnum, Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            if (joinQueryBuilderWith is IAddJoinCriteria<JoinModel> joinCriteria)
            {
                joinCriteria.AddColumnJoin(logicalOperador, field1, criteriaEnum, field2);
                return joinQueryBuilderWith;
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> AndEqual<T1, T2, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith,"AND", field1, JoinCriteriaEnum.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> OrEqual<T1, T2, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> AndNotEqual<T1, T2, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> OrNotEqual<T1, T2, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> AndGreaterThan<T1, T2, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> OrGreaterThan<T1, T2, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> AndLessThan<T1, T2, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> OrLessThan<T1, T2, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> AndGreaterThanOrEqual<T1, T2, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> OrGreaterThanOrEqual<T1, T2, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> AndLessThanOrEqual<T1, T2, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.LessThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, TReturn> OrLessThanOrEqual<T1, T2, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.LessThanOrEqual, field2);
        }
        #endregion

        #region ThreeTable
        internal static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn>
            AddColumn<T1, T2, T3, TReturn, TProperties>(IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1, JoinCriteriaEnum criteriaEnum, Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, null, field1, criteriaEnum, field2);
        }

        internal static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn>
            AddColumn<T1, T2, T3,TReturn,TProperties>(IJoinQueryBuilderWithWhere<T1, T2,T3, TReturn> joinQueryBuilderWith, string logicalOperador,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1, JoinCriteriaEnum criteriaEnum, Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            if (joinQueryBuilderWith is IAddJoinCriteria<JoinModel> joinCriteria)
            {
                joinCriteria.AddColumnJoin(logicalOperador, field1, criteriaEnum, field2);
                return joinQueryBuilderWith;
            }

            throw new InvalidOperationException("Could not add search criteria");
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> AndEqual<T1, T2, T3, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> OrEqual<T1, T2, T3, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.Equal, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> AndNotEqual<T1, T2, T3, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> OrNotEqual<T1, T2, T3, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.NotEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> AndGreaterThan<T1, T2, T3, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> OrGreaterThan<T1, T2, T3, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.GreaterThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> AndLessThan<T1, T2, T3, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> OrLessThan<T1, T2, T3, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.LessThan, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> AndGreaterThanOrEqual<T1, T2, T3, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> OrGreaterThanOrEqual<T1, T2, T3, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.GreaterThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> AndLessThanOrEqual<T1, T2, T3, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "AND", field1, JoinCriteriaEnum.LessThanOrEqual, field2);
        }

        public static IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> OrLessThanOrEqual<T1, T2, T3, TReturn, TProperties>(
            this IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> joinQueryBuilderWith,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where TReturn : IQuery
        {
            return AddColumn(joinQueryBuilderWith, "OR", field1, JoinCriteriaEnum.LessThanOrEqual, field2);
        }

        #endregion

    }
}
