using GSqlQuery.Queries;
using System;
using System.Linq.Expressions;

namespace GSqlQuery.Extensions
{
    internal static class IAddJoinCriteriaExtension
    {
        internal static void AddColumnJoin<T1, T2, TProperties>(
            this IAddJoinCriteria<JoinModel> joinCriteria, string logicalOperador,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            JoinCriteriaType joinCriteriaEnum,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
        {

            joinCriteria.AddColumns(new JoinModel
            {
                LogicalOperator = logicalOperador,
                JoinModel1 = field1.GetJoinColumn(),
                JoinCriteria = joinCriteriaEnum,
                JoinModel2 = field2.GetJoinColumn(),
            });
        }

        internal static void AddColumnJoin<T1, T2, T3, TProperties>(
            this IAddJoinCriteria<JoinModel> joinCriteria, string logicalOperador,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            JoinCriteriaType joinCriteriaEnum,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {

            joinCriteria.AddColumns(new JoinModel
            {
                LogicalOperator = logicalOperador,
                JoinModel1 = field1.GetJoinColumn(),
                JoinCriteria = joinCriteriaEnum,
                JoinModel2 = field2.GetJoinColumn(),
            });
        }
    }
}