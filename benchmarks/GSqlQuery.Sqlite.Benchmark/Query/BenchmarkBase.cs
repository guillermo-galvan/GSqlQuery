using BenchmarkDotNet.Attributes;

namespace GSqlQuery.Sqlite.Benchmark.Query
{
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public abstract class BenchmarkBase
    {
        protected readonly SqliteConnectionOptions _connectionOptions;

        public BenchmarkBase()
        {
            _connectionOptions = CreateTable.GetConnectionOptions();
        }

        [Params(true, false)]
        public bool Async { get; set; }
    }
}
