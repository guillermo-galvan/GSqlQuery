using BenchmarkDotNet.Attributes;
using GSqlQuery.Runner.Benchmark.Data;
using GSqlQuery.Runner.Benchmark.Data.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Runner.Benchmark.Query
{
    public abstract class SelectBenchmark : BenchmarkBase
    {
        public SelectBenchmark() : base()
        {
        }

        [Benchmark]
        public IQuery GenerateAllColumnsQuery()
        {
            return Film.Select(_connections).Build();
        }

        [Benchmark]
        public IQuery GenerateManyColumnsQuery()
        {
            return Film.Select(_connections, x => new { x.FilmId, x.Title, x.Description }).Build();
        }

        [Benchmark]
        public IQuery GenerateEqualWhereQuery()
        {
            return Film.Select(_connections).Where().Equal(x => x.FilmId, 1).Build();
        }

        [Benchmark]
        public IQuery GenerateBetweenWhereQuery()
        {
            return Film.Select(_connections).Where().Between(x => x.LastUpdate, DateTime.Now.AddDays(30), DateTime.Now).Build();
        }

        [Benchmark]
        public IQuery GenerateLikeWhereQuery()
        {
            return Film.Select(_connections).Where().Like(x => x.Title, "ACE GOLDFINGER").Build();
        }

        [Benchmark]
        public IQuery GenerateIsNullWhereQuery()
        {
            return Film.Select(_connections).Where().IsNull(x => x.Title).Build();
        }
    }

    public class Select : SelectBenchmark
    {
        private readonly IEnumerable<long> _ids;
        public Select() : base()
        {
            _ids = Enumerable.Range(0,10).Select(x => (long)x);
        }

        [Benchmark]
        public IQuery GenerateInWhereQuery()
        {
            return Film.Select(_connections).Where().In(x => x.FilmId, _ids).Build();
        }

        [Benchmark]
        public IQuery GenerateFiveWhereQuery()
        {
            return Film.Select(_connections).Where()
                       .In(x => x.FilmId, _ids)
                       .AndEqual(x => x.Title, "ACE GOLDFINGER")
                       .OrBetween(x => x.LastUpdate, DateTime.Now.AddDays(30), DateTime.Now)
                       .AndIsNull(x => x.SpecialFeatures)
                       .AndNotLike(x => x.ReplacementCost, ".gob")
                       .Build();
        }

    }
}
