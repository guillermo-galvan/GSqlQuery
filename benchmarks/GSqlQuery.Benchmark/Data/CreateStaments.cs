using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace GSqlQuery.Benchmarks.Data
{
    [SimpleJob(RuntimeMoniker.Net60, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net461)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public abstract class CreateStaments
    {
        protected readonly IStatements _statements;

        public CreateStaments()
        {
            _statements = new Statements();
        }
    }

}
