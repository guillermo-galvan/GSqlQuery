using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Moq;
using System.Data;

namespace GSqlQuery.Runner.Benchmark.Data
{
    [SimpleJob(RuntimeMoniker.Net90, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [SimpleJob(RuntimeMoniker.Net462)]
    public abstract class BenchmarkBase
    {
        protected readonly ConnectionOptions<IDbConnection> _connections;
        Mock<IDatabaseManagement<IDbConnection>> mock = new Mock<IDatabaseManagement<IDbConnection>>();

        public BenchmarkBase()
        {
            var events = new BenchmarkDatabaseManagmentEvents();
            mock.Setup(x => x.Events).Returns(() => events);
            _connections = new ConnectionOptions<IDbConnection>(new DefaultFormats(), mock.Object);
        }
    }
}
