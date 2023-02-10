using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    internal class AndOrJoin<T1, T2> : AndOrBase<JoinTwoTables<T1, T2>, JoinQuery<JoinTwoTables<T1, T2>>>
        where T1 : class, new()
        where T2 : class, new()
    {
        public AndOrJoin(IQueryBuilderWithWhere<JoinTwoTables<T1, T2>, JoinQuery<JoinTwoTables<T1, T2>>> queryBuilderWithWhere) :
            base(queryBuilderWithWhere, false)
        {
        }

        public override IEnumerable<CriteriaDetail> BuildCriteria(IStatements statements)
        {
            ClassOptions[] classOptions = new ClassOptions[]
            {
                ClassOptionsFactory.GetClassOptions(typeof(T1)),
                ClassOptionsFactory.GetClassOptions(typeof(T2))
            };

            return _searchCriterias.Select(x => x.GetCriteria(statements,
                classOptions.First(y => y.Table.Scheme == x.Table.Scheme && y.Table.Name == x.Table.Name).PropertyOptions)).ToArray();
        }
    }

    internal class AndOrJoin<T1, T2, T3> : AndOrBase<JoinThreeTables<T1, T2, T3>, JoinQuery<JoinThreeTables<T1, T2, T3>>>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
    {
        public AndOrJoin(IQueryBuilderWithWhere<JoinThreeTables<T1, T2, T3>, JoinQuery<JoinThreeTables<T1, T2, T3>>> queryBuilderWithWhere)
            : base(queryBuilderWithWhere, false)
        {
        }

        public override IEnumerable<CriteriaDetail> BuildCriteria(IStatements statements)
        {
            ClassOptions[] classOptions = new ClassOptions[]
            {
                ClassOptionsFactory.GetClassOptions(typeof(T1)),
                ClassOptionsFactory.GetClassOptions(typeof(T2)),
                ClassOptionsFactory.GetClassOptions(typeof(T3)),
            };

            return _searchCriterias.Select(x =>
                x.GetCriteria(statements, classOptions.First(y => y.Table.Scheme == x.Table.Scheme && y.Table.Name == x.Table.Name).PropertyOptions)).ToArray();
        }
    }
}
