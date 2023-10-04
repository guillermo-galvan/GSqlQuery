using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace GSqlQuery.Benchmarks.Data
{
    [SimpleJob(RuntimeMoniker.Net70, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net462)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public abstract class CreateStaments
    {
        protected readonly IFormats _formats;

        public CreateStaments()
        {
            _formats = new DefaultFormats();
        }
    }

}