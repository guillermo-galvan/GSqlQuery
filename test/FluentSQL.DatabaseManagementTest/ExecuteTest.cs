using FluentSQL.DatabaseManagement;
using FluentSQL.DatabaseManagement.Models;
using FluentSQL.DatabaseManagementTest.Models;
using System.Data.Common;

namespace FluentSQL.DatabaseManagementTest
{
    public class ExecuteTest
    {
        private readonly IStatements _statements;
        
        public ExecuteTest()
        {
            _statements = new FluentSQL.Default.Statements();
        }

        [Fact]
        public void Should_create_instance_of_BatchExecute()
        {
            var result = Execute.BatchExecuteFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()));
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_add_bacth()
        {
            var result = Execute.BatchExecuteFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()));
            Assert.NotNull(result);
            var result2 = result.Add((c) => Test6.Update(c, x => x.IsTests, true).Build());
            Assert.NotNull(result2);
        }

        [Fact]
        public void Should_execution()
        {
            var result = Execute.BatchExecuteFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()))
                                .Add((c) => Test6.Update(c, x => x.IsTests, true).Build())
                                .Add((c) => Test3.Select(c).Build())
                                .Execute();
            Assert.Equal(0, result);
        }

        [Fact]
        public void Should_execution_with_Connection()
        {
            var result = Execute.BatchExecuteFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()))
                                .Add((c) => Test6.Update(c, x => x.IsTests, true).Build())
                                .Add((c) => Test3.Select(c).Build())
                                .Execute(LoadFluentOptions.GetDbConnection());
            Assert.Equal(0, result);
        }

        [Fact]
        public void Throw_exception_if_parameter_is_null2()
        {
            IDatabaseManagement<DbConnection> databaseManagement = null;
            Assert.Throws<ArgumentNullException>(() => Execute.BatchExecuteFactory(new ConnectionOptions<DbConnection>(null, LoadFluentOptions.GetDatabaseManagmentMock())));
            Assert.Throws<ArgumentNullException>(() => Execute.BatchExecuteFactory(new ConnectionOptions<DbConnection>(_statements, databaseManagement)));
        }

        [Fact]
        public async Task Should_executionAsync()
        {
            var result = await Execute.BatchExecuteFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMockAsync()))
                                .Add((c) => Test6.Update(c, x => x.IsTests, true).Build())
                                .Add((c) => Test3.Select(c).Build())
                                .ExecuteAsync(CancellationToken.None);
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task Should_executionAsync_with_Connection()
        {
            var result = await Execute.BatchExecuteFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMockAsync()))
                                .Add((c) => Test6.Update(c, x => x.IsTests, true).Build())
                                .Add((c) => Test3.Select(c).Build())
                                .ExecuteAsync(LoadFluentOptions.GetDbConnection(), CancellationToken.None);
            Assert.Equal(0, result);
        }
    }
}
