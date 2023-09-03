using GSqlQuery.Runner.Test.Models;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GSqlQuery.Runner.Test.DataBase
{
    public class DatabaseManagementExtensionTest
    {
        [Fact]
        public void ExecuteWithTransaction()
        {
            Mock<ITransaction> mockITransaction = new Mock<ITransaction>();
            Mock<IConnection> mockIConnection = new Mock<IConnection>();

            mockIConnection.Setup(x => x.BeginTransaction()).Returns(mockITransaction.Object);
            mockITransaction.Setup(x => x.Connection).Returns(mockIConnection.Object);

            Mock<IDatabaseManagement<IConnection>> mockIDatabaseManagement = new Mock<IDatabaseManagement<IConnection>>();

            mockIDatabaseManagement.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mockIDatabaseManagement.Setup(x => x.GetConnection()).Returns(() => mockIConnection.Object);

            mockIDatabaseManagement.Setup(x => x.ExecuteReader(It.IsAny<IConnection>(), It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<IConnection, IQuery<Join<Test1, Test3>>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((c, q, p, pa) =>
                {
                    return Enumerable.Empty<Join<Test1, Test3>>();
                });

            ConnectionOptions<IConnection> connectionOptions = new ConnectionOptions<IConnection>(new Statements(), mockIDatabaseManagement.Object);

            var result = EntityExecute<Test1>.Select(connectionOptions).LeftJoin<Test3>().NotEqual(x => x.Table2.Ids, x => x.Table1.Id).Build().ExecuteWithTransaction();

            Assert.Empty(result);
            mockIDatabaseManagement.Verify(x => x.ExecuteReader(It.IsAny<IConnection>(), It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()));
        }

        [Fact]
        public void ExecuteWithTransaction_with_transaction()
        {
            Mock<ITransaction> mockITransaction = new Mock<ITransaction>();
            Mock<IConnection> mockIConnection = new Mock<IConnection>();

            mockIConnection.Setup(x => x.BeginTransaction()).Returns(mockITransaction.Object);
            mockITransaction.Setup(x => x.Connection).Returns(mockIConnection.Object);

            Mock<IDatabaseManagement<IConnection>> mockIDatabaseManagement = new Mock<IDatabaseManagement<IConnection>>();

            mockIDatabaseManagement.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mockIDatabaseManagement.Setup(x => x.GetConnection()).Returns(() => mockIConnection.Object);

            mockIDatabaseManagement.Setup(x => x.ExecuteReader(It.IsAny<IConnection>(), It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<IConnection, IQuery<Join<Test1, Test3>>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((c, q, p, pa) =>
                {
                    return Enumerable.Empty<Join<Test1, Test3>>();
                });

            ConnectionOptions<IConnection> connectionOptions = new ConnectionOptions<IConnection>(new Statements(), mockIDatabaseManagement.Object);

            using (var connection = connectionOptions.DatabaseManagement.GetConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    var result = EntityExecute<Test1>.Select(connectionOptions).LeftJoin<Test3>().NotEqual(x => x.Table2.Ids, x => x.Table1.Id).Build().ExecuteWithTransaction(transaction);
                    Assert.Empty(result);
                    mockIDatabaseManagement.Verify(x => x.ExecuteReader(It.IsAny<IConnection>(), It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()));
                }  
            }
        }

        [Fact]
        public async Task ExecuteWithTransactionAsync()
        {
            Mock<ITransaction> mockITransaction = new Mock<ITransaction>();
            Mock<IConnection> mockIConnection = new Mock<IConnection>();

            mockIConnection.Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockITransaction.Object));
            mockITransaction.Setup(x => x.Connection).Returns(mockIConnection.Object);

            Mock<IDatabaseManagement<IConnection>> mockIDatabaseManagement = new Mock<IDatabaseManagement<IConnection>>();

            mockIDatabaseManagement.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mockIDatabaseManagement.Setup(x => x.GetConnectionAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(mockIConnection.Object));

            mockIDatabaseManagement.Setup(x => x.ExecuteReaderAsync(It.IsAny<IConnection>(), It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<CancellationToken>()))
                .Returns<IConnection, IQuery<Join<Test1, Test3>>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>, CancellationToken>((c, q, p, pa, ca) =>
                {
                    return Task.FromResult(Enumerable.Empty<Join<Test1, Test3>>());
                });

            ConnectionOptions<IConnection> connectionOptions = new ConnectionOptions<IConnection>(new Statements(), mockIDatabaseManagement.Object);

            var result = await EntityExecute<Test1>.Select(connectionOptions).LeftJoin<Test3>().NotEqual(x => x.Table2.Ids, x => x.Table1.Id).Build().ExecuteWithTransactionAsync();

            Assert.Empty(result);
            mockIDatabaseManagement.Verify(x => x.ExecuteReaderAsync(It.IsAny<IConnection>(), It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task ExecuteWithTransactionAsync_with_transaction()
        {
            Mock<ITransaction> mockITransaction = new Mock<ITransaction>();
            Mock<IConnection> mockIConnection = new Mock<IConnection>();

            mockIConnection.Setup(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockITransaction.Object));
            mockITransaction.Setup(x => x.Connection).Returns(mockIConnection.Object);

            Mock<IDatabaseManagement<IConnection>> mockIDatabaseManagement = new Mock<IDatabaseManagement<IConnection>>();

            mockIDatabaseManagement.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mockIDatabaseManagement.Setup(x => x.GetConnectionAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(mockIConnection.Object));

            mockIDatabaseManagement.Setup(x => x.ExecuteReaderAsync(It.IsAny<IConnection>(), It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<CancellationToken>()))
                .Returns<IConnection, IQuery<Join<Test1, Test3>>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>, CancellationToken>((c, q, p, pa, ca) =>
                {
                    return Task.FromResult(Enumerable.Empty<Join<Test1, Test3>>());
                });

            ConnectionOptions<IConnection> connectionOptions = new ConnectionOptions<IConnection>(new Statements(), mockIDatabaseManagement.Object);

            using (var connection = await connectionOptions.DatabaseManagement.GetConnectionAsync())
            {
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    var result = await EntityExecute<Test1>.Select(connectionOptions).LeftJoin<Test3>().NotEqual(x => x.Table2.Ids, x => x.Table1.Id).Build().ExecuteWithTransactionAsync(transaction);
                    Assert.Empty(result);
                    mockIDatabaseManagement.Verify(x => x.ExecuteReaderAsync(It.IsAny<IConnection>(), It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<CancellationToken>()));
                }
            }
        }
    }
}
