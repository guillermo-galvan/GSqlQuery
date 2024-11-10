using BenchmarkDotNet.Attributes;
using GSqlQuery.Runner.Benchmark.Data;
using GSqlQuery.Runner.Benchmark.Data.Table;

namespace GSqlQuery.Runner.Benchmark.Query
{
    public class Join : BenchmarkBase
    {
        public Join() : base()
        {
        }

        [Benchmark]
        public IQuery GenerateInnerJoinQuery_JoinTwoTables()
        {
            return Film.Select(_connections).InnerJoin<Inventory>().Equal(x => x.Table1.FilmId, x => x.Table2.FilmId).Build();
        }

        [Benchmark]
        public IQuery GenerateLeftJoinQuery_JoinTwoTables()
        {
            return Film.Select(_connections).LeftJoin<Inventory>().Equal(x => x.Table1.FilmId, x => x.Table2.FilmId).Build();
        }

        [Benchmark]
        public IQuery GenerateRightJoinQuery_JoinTwoTables()
        {
            return Film.Select(_connections).RightJoin<Inventory>().Equal(x => x.Table1.FilmId, x => x.Table2.FilmId).Build();
        }

        [Benchmark]
        public IQuery GenerateInnerJoinQuery_JoinThreeTables()
        {
            return Film.Select(_connections)
                       .InnerJoin<Inventory>().Equal(x => x.Table1.FilmId, x => x.Table2.FilmId)
                       .InnerJoin<Language>().Equal(x => x.Table1.LanguageId, x => x.Table3.LanguageId)
                       .Build();
        }

        [Benchmark]
        public IQuery GenerateLeftJoinQuery_JoinThreeTables()
        {
            return Film.Select(_connections)
                       .LeftJoin<Inventory>().Equal(x => x.Table1.FilmId, x => x.Table2.FilmId)
                       .LeftJoin<Language>().Equal(x => x.Table1.LanguageId, x => x.Table3.LanguageId)
                       .Build();
        }

        [Benchmark]
        public IQuery GenerateRightJoinQuery_JoinThreeTables()
        {
            return Film.Select(_connections)
                       .RightJoin<Inventory>().Equal(x => x.Table1.FilmId, x => x.Table2.FilmId)
                       .RightJoin<Language>().Equal(x => x.Table1.LanguageId, x => x.Table3.LanguageId)
                       .Build();
        }
    }
}
