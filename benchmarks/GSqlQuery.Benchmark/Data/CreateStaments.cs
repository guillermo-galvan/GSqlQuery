using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace GSqlQuery.Benchmarks.Data
{
    [SimpleJob(RuntimeMoniker.Net90, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [SimpleJob(RuntimeMoniker.Net462)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public abstract class CreateStaments
    {
        protected readonly QueryOptions _queryOptions;

        public CreateStaments()
        {
            _queryOptions = new QueryOptions(new DefaultFormats());
        }
    }

}