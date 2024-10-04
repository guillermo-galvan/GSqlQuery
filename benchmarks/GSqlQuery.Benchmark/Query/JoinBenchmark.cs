using BenchmarkDotNet.Attributes;
using GSqlQuery.Benchmark.Data;
using GSqlQuery.Benchmarks.Data;

namespace GSqlQuery.Benchmark.Query
{
    public class Join : CreateStaments
    {
        public Join() : base()
        {
        }

        [Benchmark]
        public IQuery GenerateInnerJoinQuery_JoinTwoTables()
        {
            return User.Select(_queryOptions).InnerJoin<UserRequest>().Equal(x => x.Table1.Id, x => x.Table2.UserId).Build();
        }

        [Benchmark]
        public IQuery GenerateInnerJoinQuery_JoinThreeTables()
        {
            return User.Select(_queryOptions)
                       .InnerJoin<UserRequest>().Equal(x => x.Table1.Id, x => x.Table2.UserId)
                       .InnerJoin<Request>().Equal(x => x.Table2.RequestId, x => x.Table3.Id)
                       .Build();
        }

        [Benchmark]
        public IQuery GenerateOrderByQuery_JoinTowTables()
        {
            return User.Select(_queryOptions)
                       .RightJoin<UserRequest>().Equal(x => x.Table1.Id, x => x.Table2.UserId)
                       .OrderBy(x => new { x.Table1.Id }, OrderBy.ASC)
                       .Build();
        }

        [Benchmark]
        public IQuery GenerateOrderByQuery_JoinTowTables_WithWhere()
        {
            return User.Select(_queryOptions)
                       .RightJoin<UserRequest>().Equal(x => x.Table1.Id, x => x.Table2.UserId)
                       .Where()
                       .Equal(x => x.Table1.Id, 1)
                       .OrderBy(x => new { x.Table1.Id }, OrderBy.ASC)
                       .Build();
        }

        [Benchmark]
        public IQuery GenerateOrderByQuery_JoinThreeTables()
        {
            return User.Select(_queryOptions)
                       .RightJoin<UserRequest>().Equal(x => x.Table1.Id, x => x.Table2.UserId)
                       .RightJoin<Request>().Equal(x => x.Table2.RequestId, x => x.Table3.Id)
                       .OrderBy(x => new { x.Table1.Id }, OrderBy.ASC)
                       .Build();
        }
    }
}