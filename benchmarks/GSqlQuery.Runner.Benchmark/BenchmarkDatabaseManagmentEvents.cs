using Moq;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GSqlQuery.Runner.Benchmark
{
    internal class BenchmarkDatabaseManagmentEvents : DatabaseManagementEvents
    {
        private readonly Mock<IDataParameter> _parameter;
        public BenchmarkDatabaseManagmentEvents()
        {
            _parameter = new Mock<IDataParameter>();
        }

        public override IEnumerable<IDataParameter> GetParameter<T>(IEnumerable<ParameterDetail> parameters)
        {
            return parameters.Select(x => _parameter.Object);
        }
    }
}
