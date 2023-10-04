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
            return User.Select(_formats).InnerJoin<UserRequest>().Equal(x => x.Table1.Id, x => x.Table2.UserId).Build();
        }

        [Benchmark]
        public IQuery GenerateLeftJoinQuery_JoinTwoTables()
        {
            return User.Select(_formats).LeftJoin<UserRequest>().Equal(x => x.Table1.Id, x => x.Table2.UserId).Build();
        }

        [Benchmark]
        public IQuery GenerateRightJoinQuery_JoinTwoTables()
        {
            return User.Select(_formats).RightJoin<UserRequest>().Equal(x => x.Table1.Id, x => x.Table2.UserId).Build();
        }

        [Benchmark]
        public IQuery GenerateInnerJoinQuery_JoinThreeTables()
        {
            return User.Select(_formats)
                       .InnerJoin<UserRequest>().Equal(x => x.Table1.Id, x => x.Table2.UserId)
                       .InnerJoin<Request>().Equal(x => x.Table2.RequestId, x => x.Table3.Id)
                       .Build();
        }

        [Benchmark]
        public IQuery GenerateLeftJoinQuery_JoinThreeTables()
        {
            return User.Select(_formats)
                       .LeftJoin<UserRequest>().Equal(x => x.Table1.Id, x => x.Table2.UserId)
                       .LeftJoin<Request>().Equal(x => x.Table2.RequestId, x => x.Table3.Id)
                       .Build();
        }

        [Benchmark]
        public IQuery GenerateRightJoinQuery_JoinThreeTables()
        {
            return User.Select(_formats)
                       .RightJoin<UserRequest>().Equal(x => x.Table1.Id, x => x.Table2.UserId)
                       .RightJoin<Request>().Equal(x => x.Table2.RequestId, x => x.Table3.Id)
                       .Build();
        }
    }
}