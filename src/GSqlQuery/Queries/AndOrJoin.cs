using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Queries
{
    internal class AndOrJoin<T1, T2, TJoin, TReturn, TOptions> : AndOrBase<TJoin, TReturn, TOptions>
        where T1 : class
        where T2 : class
        where TJoin : class
        where TReturn : IQuery<TJoin>
    {
        public AndOrJoin(IQueryBuilderWithWhere<TReturn, TOptions> queryBuilderWithWhere) :
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

    internal class AndOrJoin<T1, T2, T3, TJoin, TReturn, TOptions> : AndOrBase<TJoin, TReturn, TOptions>
        where T1 : class
        where T2 : class
        where T3 : class
        where TJoin : class
        where TReturn : IQuery<TJoin>
    {
        public AndOrJoin(IQueryBuilderWithWhere<TReturn, TOptions> queryBuilderWithWhere)
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