using BenchmarkDotNet.Attributes;
using GSqlQuery.Runner.Benchmark.Data;
using GSqlQuery.Runner.Benchmark.Data.Table;

namespace GSqlQuery.Runner.Benchmark.Query
{
    public class Insert : BenchmarkBase
    {
        protected Film _film;

        public Insert() : base()
        {
            _film = new Film();
        }

        [Benchmark]
        public IQuery GenerateQuery()
        {
            return _film.Insert(_connections).Build();
        }
    }
}
