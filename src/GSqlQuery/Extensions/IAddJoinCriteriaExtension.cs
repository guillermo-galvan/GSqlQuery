using GSqlQuery.Queries;
using System;
using System.Linq.Expressions;

namespace GSqlQuery.Extensions
{
    /// <summary>
    /// IAddJoinCriteria Extension
    /// </summary>
    internal static class IAddJoinCriteriaExtension
    {
        /// <summary>
        /// Add JoinModel to columns
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinCriteria">Implementation of the IAddJoinCriteria interface</param>
        /// <param name="logicalOperador">Logical Operador</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="joinCriteriaEnum">Join Criteria Type</param>
        /// <param name="field2">Expression for field 2</param>
        internal static void AddColumnJoin<T1, T2, TProperties>(
            IAddJoinCriteria<JoinModel> joinCriteria, string logicalOperador,
            Expression<Func<Join<T1, T2>, TProperties>> field1,
            JoinCriteriaType joinCriteriaEnum,
            Expression<Func<Join<T1, T2>, TProperties>> field2)
            where T1 : class
            where T2 : class
        {
            Type entity = typeof(Join<T1, T2>);
            Type properties = typeof(TProperties);

            JoinCriteriaPart joinCriteria1 = new JoinCriteriaPart(new DynamicQuery(entity, properties), field1);
            JoinCriteriaPart joinCriteria2 = new JoinCriteriaPart(new DynamicQuery(entity, properties), field2);
            JoinModel joinModel = new JoinModel(logicalOperador, joinCriteria1, joinCriteriaEnum, joinCriteria2);
            joinCriteria.AddColumns(joinModel);
        }

        /// <summary>
        /// Add JoinModel to columns
        /// </summary>
        /// <typeparam name="T1">Table type</typeparam>
        /// <typeparam name="T2">Table type</typeparam>
        /// <typeparam name="T3">Table type</typeparam>
        /// <typeparam name="TProperties">Property type</typeparam>
        /// <param name="joinCriteria">Implementation of the IAddJoinCriteria interface</param>
        /// <param name="logicalOperador">Logical Operador</param>
        /// <param name="field1">Expression for field 1</param>
        /// <param name="joinCriteriaEnum">Join Criteria Type</param>
        /// <param name="field2">Expression for field 2</param>
        internal static void AddColumnJoin<T1, T2, T3, TProperties>(
            IAddJoinCriteria<JoinModel> joinCriteria, string logicalOperador,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field1,
            JoinCriteriaType joinCriteriaEnum,
            Expression<Func<Join<T1, T2, T3>, TProperties>> field2)
            where T1 : class
            where T2 : class
            where T3 : class
        {
            Type entity = typeof(Join<T1, T2, T3>);
            Type properties = typeof(TProperties);

            JoinCriteriaPart joinCriteria1 = new JoinCriteriaPart(new DynamicQuery(entity, properties), field1);
            JoinCriteriaPart joinCriteria2 = new JoinCriteriaPart(new DynamicQuery(entity, properties), field2);
            JoinModel joinModel = new JoinModel(logicalOperador, joinCriteria1, joinCriteriaEnum, joinCriteria2);

            joinCriteria.AddColumns(joinModel);
        }
    }
}