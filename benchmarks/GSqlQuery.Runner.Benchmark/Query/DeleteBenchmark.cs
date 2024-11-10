using BenchmarkDotNet.Attributes;
using GSqlQuery.Runner.Benchmark.Data;
using GSqlQuery.Runner.Benchmark.Data.Table;
using System;
using System.Collections.Generic;
using System.Linq;


namespace GSqlQuery.Runner.Benchmark.Query
{
    public abstract class DeleteBenchmark : BenchmarkBase
    {
        public DeleteBenchmark() : base()
        {
        }

        [Benchmark]
        public IQuery GenerateQuery()
        {
            return Film.Delete(_connections).Build();
        }

        [Benchmark]
        public IQuery GenerateEqualWhereQuery()
        {
            return Film.Delete(_connections).Where().Equal(x => x.FilmId, 1).Build();
        }

        [Benchmark]
        public IQuery GenerateBetweenWhereQuery()
        {
            return Film.Delete(_connections).Where().Between(x => x.LastUpdate, DateTime.Now.AddDays(-30), DateTime.Now).Build();
        }

        [Benchmark]
        public IQuery GenerateLikeWhereQuery()
        {
            return Film.Delete(_connections).Where().Like(x => x.Title, "23").Build();
        }

        [Benchmark]
        public IQuery GenerateIsNullWhereQuery()
        {
            return Film.Delete(_connections).Where().IsNull(x => x.Description).Build();
        }
    }

    public class Delete : DeleteBenchmark
    {
        private readonly IEnumerable<long> _ids;
        public Delete() : base()
        {
            _ids = Enumerable.Range(0, 10).Select(x => (long)x);
        }

        [Benchmark]
        public IQuery GenerateInWhereQuery()
        {
            return Film.Delete(_connections).Where().In(x => x.FilmId, _ids).Build();
        }

        [Benchmark]
        public IQuery GenerateFiveWhereQuery()
        {
            return Film.Delete(_connections).Where()
                       .In(x => x.FilmId, _ids)
                       .AndEqual(x => x.Title, "nombre")
                       .OrBetween(x => x.LastUpdate, DateTime.Now.AddDays(-30), DateTime.Now)
                       .AndIsNull(x => x.Description)
                       .AndNotLike(x => x.SpecialFeatures, ".gob")
                       .Build();
        }
    }
}
