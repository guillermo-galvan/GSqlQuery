using System;

namespace GSqlQuery.Runner.Benchmark
{
    internal class BenchmarkDatabaseManagmentEvents : DatabaseManagementEvents
    {
        protected override ITypeHandler<TDbDataReader> GetTypeHandler<TDbDataReader>(Type property)
        {
            return null;
        }
    }
}
