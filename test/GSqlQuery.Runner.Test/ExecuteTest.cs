using GSqlQuery.Runner.Test.Models;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GSqlQuery.Runner.Test
{
    public class ExecuteTest
    {
        private readonly IFormats _formats;

        public ExecuteTest()
        {
            _formats = new TestFormats();
        }

        [Fact]
        public void Should_create_instance_of_BatchExecute()
        {
            var result = Execute.BatchExecuteFactory(new ConnectionOptions<IDbConnection>(_formats, LoadGSqlQueryOptions.GetDatabaseManagmentMock()));
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_add_bacth()
        {
            var result = Execute.BatchExecuteFactory(new ConnectionOptions<IDbConnection>(_formats, LoadGSqlQueryOptions.GetDatabaseManagmentMock()));
            Assert.NotNull(result);
            var result2 = result.Add((c) => Test6.Update(c, x => x.IsTests, true).Build());
            Assert.NotNull(result2);
        }

        [Fact]
        public void Should_execution()
        {
            var result = Execute.BatchExecuteFactory(new ConnectionOptions<IDbConnection>(_formats, LoadGSqlQueryOptions.GetDatabaseManagmentMock()))
                                .Add((c) => Test6.Update(c, x => x.IsTests, true).Build())
                                .Add((c) => Test3.Select(c).Build())
                                .Execute();
            Assert.Equal(0, result);
        }

        [Fact]
        public void Should_execution_with_Connection()
        {
            var result = Execute.BatchExecuteFactory(new ConnectionOptions<IDbConnection>(_formats, LoadGSqlQueryOptions.GetDatabaseManagmentMock()))
                                .Add((c) => Test6.Update(c, x => x.IsTests, true).Build())
                                .Add((c) => Test3.Select(c).Build())
                                .Execute(LoadGSqlQueryOptions.GetIDbConnection());
            Assert.Equal(0, result);
        }

        [Fact]
        public void Throw_exception_if_parameter_is_null2()
        {
            IDatabaseManagement<IDbConnection> databaseManagement = null;
            Assert.Throws<ArgumentNullException>(() => Execute.BatchExecuteFactory(new ConnectionOptions<IDbConnection>(null, LoadGSqlQueryOptions.GetDatabaseManagmentMock())));
            Assert.Throws<ArgumentNullException>(() => Execute.BatchExecuteFactory(new ConnectionOptions<IDbConnection>(_formats, databaseManagement)));
        }

        [Fact]
        public async Task Should_executionAsync()
        {
            var result = await Execute.BatchExecuteFactory(new ConnectionOptions<IDbConnection>(_formats, LoadGSqlQueryOptions.GetDatabaseManagmentMockAsync()))
                                .Add((c) => Test6.Update(c, x => x.IsTests, true).Build())
                                .Add((c) => Test3.Select(c).Build())
                                .ExecuteAsync(CancellationToken.None);
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task Should_executionAsync_with_Connection()
        {
            var result = await Execute.BatchExecuteFactory(new ConnectionOptions<IDbConnection>(_formats, LoadGSqlQueryOptions.GetDatabaseManagmentMockAsync()))
                                .Add((c) => Test6.Update(c, x => x.IsTests, true).Build())
                                .Add((c) => Test3.Select(c).Build())
                                .ExecuteAsync(LoadGSqlQueryOptions.GetIDbConnection(), CancellationToken.None);
            Assert.Equal(0, result);
        }
    }
}