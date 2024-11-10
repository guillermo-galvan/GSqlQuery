using GSqlQuery.Runner.Test.Models;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class JoinQueryTest
    {
        [Fact]
        public void Execute()
        {
            Mock<IDatabaseManagement<IDbConnection>> mock = new Mock<IDatabaseManagement<IDbConnection>>();
            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.GetConnection()).Returns(() => LoadGSqlQueryOptions.GetIDbConnection());

            mock.Setup(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<IQuery<Join<Test1, Test3>>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((q, p, pa) =>
                {
                    return Enumerable.Empty<Join<Test1, Test3>>();
                });
            ConnectionOptions<IDbConnection> connectionOptions = new ConnectionOptions<IDbConnection>(new TestFormats(), mock.Object);

            var result = EntityExecute<Test1>.Select(connectionOptions).LeftJoin<Test3>().NotEqual(x => x.Table2.Ids, x => x.Table1.Id).Build().Execute();

            Assert.Empty(result);
            mock.Verify(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()));
        }

        [Fact]
        public void Execute_with_connection()
        {
            Mock<IDatabaseManagement<IDbConnection>> mock = new Mock<IDatabaseManagement<IDbConnection>>();
            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.GetConnection()).Returns(() => LoadGSqlQueryOptions.GetIDbConnection());

            mock.Setup(x => x.ExecuteReader(It.IsAny<IDbConnection>(),It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns(Enumerable.Empty<Join<Test1, Test3>>);

            ConnectionOptions<IDbConnection> connectionOptions = new ConnectionOptions<IDbConnection>(new TestFormats(), mock.Object);

            using var connection = connectionOptions.DatabaseManagement.GetConnection();

            var result = EntityExecute<Test1>.Select(connectionOptions).LeftJoin<Test3>().NotEqual(x => x.Table2.Ids, x => x.Table1.Id).Build().Execute(connection);

            Assert.Empty(result);
            mock.Setup(x => x.ExecuteReader(It.IsAny<IDbConnection>(), It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()));
        }

        [Fact]
        public async Task ExecuteAsync()
        {
            Mock<IDatabaseManagement<IDbConnection>> mock = new Mock<IDatabaseManagement<IDbConnection>>();
            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.GetConnection()).Returns(() => LoadGSqlQueryOptions.GetIDbConnection());

            mock.Setup(x => x.ExecuteReaderAsync(It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<CancellationToken>()))
                .Returns(() =>
                {
                    return Task.FromResult(Enumerable.Empty<Join<Test1, Test3>>());
                });
            ConnectionOptions<IDbConnection> connectionOptions = new ConnectionOptions<IDbConnection>(new TestFormats(), mock.Object);

            var result = await EntityExecute<Test1>.Select(connectionOptions).LeftJoin<Test3>().NotEqual(x => x.Table2.Ids, x => x.Table1.Id).Build().ExecuteAsync();

            Assert.Empty(result);
            mock.Setup(x => x.ExecuteReaderAsync(It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task ExecuteAsync_with_connection()
        {
            Mock<IDatabaseManagement<IDbConnection>> mock = new Mock<IDatabaseManagement<IDbConnection>>();
            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.GetConnection()).Returns(() => LoadGSqlQueryOptions.GetIDbConnection());

            mock.Setup(x => x.ExecuteReaderAsync(It.IsAny<IDbConnection>(), It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<CancellationToken>()))
                .Returns(() =>
                {
                    return Task.FromResult(Enumerable.Empty<Join<Test1, Test3>>());
                });
            ConnectionOptions<IDbConnection> connectionOptions = new ConnectionOptions<IDbConnection>(new TestFormats(), mock.Object);
            using var connection = connectionOptions.DatabaseManagement.GetConnection();

            var result = await EntityExecute<Test1>.Select(connectionOptions).LeftJoin<Test3>().NotEqual(x => x.Table2.Ids, x => x.Table1.Id).Build().ExecuteAsync(connection);

            Assert.Empty(result);
            mock.Setup(x => x.ExecuteReaderAsync(It.IsAny<IDbConnection>(), It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<CancellationToken>()));
        }
    }
}
