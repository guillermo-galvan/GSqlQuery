using GSqlQuery.Runner.Test.Models;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace GSqlQuery.Runner.Test.Extensions
{
    public class IJoinQueryBuilderExtensionTest
    {
        [Fact]
        public void NotEqual_Join_Two_Table()
        {
            Mock<IDatabaseManagement<IDbConnection>> mock = new Mock<IDatabaseManagement<IDbConnection>>();
            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.GetConnection()).Returns(() => LoadGSqlQueryOptions.GetIDbConnection());

            mock.Setup(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1,Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<IQuery<Join<Test1, Test3>>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((q, p, pa) =>
                {
                    return Enumerable.Empty<Join<Test1, Test3>>();
                });
            ConnectionOptions<IDbConnection> connectionOptions = new ConnectionOptions<IDbConnection>(new Statements(), mock.Object);

            var query = EntityExecute<Test1>.Select(connectionOptions).LeftJoin<Test3>().NotEqual(x => x.Table2.Ids, x => x.Table1.Id).Build();
            var result = query.Execute();

            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id <> Test1.Id;", query.Text);
            Assert.Empty(result);
            mock.Verify(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()));
        }

        [Fact]
        public void GreaterThan_Join_Two_Table()
        {
            Mock<IDatabaseManagement<IDbConnection>> mock = new Mock<IDatabaseManagement<IDbConnection>>();
            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.GetConnection()).Returns(() => LoadGSqlQueryOptions.GetIDbConnection());

            mock.Setup(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<IQuery<Join<Test1, Test3>>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((q, p, pa) =>
                {
                    return Enumerable.Empty<Join<Test1, Test3>>();
                });
            ConnectionOptions<IDbConnection> connectionOptions = new ConnectionOptions<IDbConnection>(new Statements(), mock.Object);

            var query = EntityExecute<Test1>.Select(connectionOptions).LeftJoin<Test3>().GreaterThan(x => x.Table2.Ids, x => x.Table1.Id).Build();
            var result = query.Execute();

            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id > Test1.Id;", query.Text);
            Assert.Empty(result);
            mock.Verify(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()));
        }

        [Fact]
        public void LessThan_Join_Two_Table()
        {
            Mock<IDatabaseManagement<IDbConnection>> mock = new Mock<IDatabaseManagement<IDbConnection>>();
            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.GetConnection()).Returns(() => LoadGSqlQueryOptions.GetIDbConnection());

            mock.Setup(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<IQuery<Join<Test1, Test3>>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((q, p, pa) =>
                {
                    return Enumerable.Empty<Join<Test1, Test3>>();
                });
            ConnectionOptions<IDbConnection> connectionOptions = new ConnectionOptions<IDbConnection>(new Statements(), mock.Object);

            var query = EntityExecute<Test1>.Select(connectionOptions).LeftJoin<Test3>().LessThan(x => x.Table2.Ids, x => x.Table1.Id).Build();
            var result = query.Execute();

            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id < Test1.Id;", query.Text);
            Assert.Empty(result);
            mock.Verify(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()));
        }

        [Fact]
        public void GreaterThanOrEqual_Join_Two_Table()
        {
            Mock<IDatabaseManagement<IDbConnection>> mock = new Mock<IDatabaseManagement<IDbConnection>>();
            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.GetConnection()).Returns(() => LoadGSqlQueryOptions.GetIDbConnection());

            mock.Setup(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<IQuery<Join<Test1, Test3>>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((q, p, pa) =>
                {
                    return Enumerable.Empty<Join<Test1, Test3>>();
                });
            ConnectionOptions<IDbConnection> connectionOptions = new ConnectionOptions<IDbConnection>(new Statements(), mock.Object);

            var query = EntityExecute<Test1>.Select(connectionOptions).LeftJoin<Test3>().GreaterThanOrEqual(x => x.Table2.Ids, x => x.Table1.Id).Build();
            var result = query.Execute();

            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id >= Test1.Id;", query.Text);
            Assert.Empty(result);
            mock.Verify(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()));
        }

        [Fact]
        public void LessThanOrEqual_Join_Two_Table()
        {
            Mock<IDatabaseManagement<IDbConnection>> mock = new Mock<IDatabaseManagement<IDbConnection>>();
            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.GetConnection()).Returns(() => LoadGSqlQueryOptions.GetIDbConnection());

            mock.Setup(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<IQuery<Join<Test1, Test3>>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((q, p, pa) =>
                {
                    return Enumerable.Empty<Join<Test1, Test3>>();
                });
            ConnectionOptions<IDbConnection> connectionOptions = new ConnectionOptions<IDbConnection>(new Statements(), mock.Object);

            var query = EntityExecute<Test1>.Select(connectionOptions).LeftJoin<Test3>().LessThanOrEqual(x => x.Table2.Ids, x => x.Table1.Id).Build();
            var result = query.Execute();

            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id <= Test1.Id;", query.Text);
            Assert.Empty(result);
            mock.Verify(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()));
        }

        [Fact]
        public void NotEqual_Join_Three_Table()
        {
            Mock<IDatabaseManagement<IDbConnection>> mock = new Mock<IDatabaseManagement<IDbConnection>>();
            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.GetConnection()).Returns(() => LoadGSqlQueryOptions.GetIDbConnection());

            mock.Setup(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3, Test6>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<IQuery<Join<Test1, Test3, Test6>>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((q, p, pa) =>
                {
                    return Enumerable.Empty<Join<Test1, Test3, Test6>>();
                });
            ConnectionOptions<IDbConnection> connectionOptions = new ConnectionOptions<IDbConnection>(new Statements(), mock.Object);

            var query = EntityExecute<Test1>.Select(connectionOptions)
                                            .LeftJoin<Test3>().NotEqual(x => x.Table2.Ids, x => x.Table1.Id)
                                            .RightJoin<Test6>().NotEqual(x => x.Table3.Ids, x => x.Table1.Id).Build();
            var result = query.Execute();

            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id <> Test1.Id RIGHT JOIN TableName ON TableName.Id <> Test1.Id;", query.Text);
            Assert.Empty(result);
            mock.Verify(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3, Test6>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()));
        }

        [Fact]
        public void GreaterThan_Join_Three_Table()
        {
            Mock<IDatabaseManagement<IDbConnection>> mock = new Mock<IDatabaseManagement<IDbConnection>>();
            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.GetConnection()).Returns(() => LoadGSqlQueryOptions.GetIDbConnection());

            mock.Setup(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3, Test6>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<IQuery<Join<Test1, Test3, Test6>>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((q, p, pa) =>
                {
                    return Enumerable.Empty<Join<Test1, Test3, Test6>>();
                });
            ConnectionOptions<IDbConnection> connectionOptions = new ConnectionOptions<IDbConnection>(new Statements(), mock.Object);

            var query = EntityExecute<Test1>.Select(connectionOptions)
                                            .LeftJoin<Test3>().GreaterThan(x => x.Table2.Ids, x => x.Table1.Id)
                                            .RightJoin<Test6>().GreaterThan(x => x.Table3.Ids, x => x.Table1.Id).Build();
            var result = query.Execute();

            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id > Test1.Id RIGHT JOIN TableName ON TableName.Id > Test1.Id;", query.Text);
            Assert.Empty(result);
            mock.Verify(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3, Test6>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()));
        }

        [Fact]
        public void LessThan_Join_Three_Table()
        {
            Mock<IDatabaseManagement<IDbConnection>> mock = new Mock<IDatabaseManagement<IDbConnection>>();
            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.GetConnection()).Returns(() => LoadGSqlQueryOptions.GetIDbConnection());

            mock.Setup(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3, Test6>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<IQuery<Join<Test1, Test3, Test6>>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((q, p, pa) =>
                {
                    return Enumerable.Empty<Join<Test1, Test3, Test6>>();
                });
            ConnectionOptions<IDbConnection> connectionOptions = new ConnectionOptions<IDbConnection>(new Statements(), mock.Object);

            var query = EntityExecute<Test1>.Select(connectionOptions)
                                            .LeftJoin<Test3>().LessThan(x => x.Table2.Ids, x => x.Table1.Id)
                                            .RightJoin<Test6>().LessThan(x => x.Table3.Ids, x => x.Table1.Id).Build();
            var result = query.Execute();

            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id < Test1.Id RIGHT JOIN TableName ON TableName.Id < Test1.Id;", query.Text);
            Assert.Empty(result);
            mock.Verify(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3, Test6>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()));
        }

        [Fact]
        public void GreaterThanOrEqual_Join_Three_Table()
        {
            Mock<IDatabaseManagement<IDbConnection>> mock = new Mock<IDatabaseManagement<IDbConnection>>();
            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.GetConnection()).Returns(() => LoadGSqlQueryOptions.GetIDbConnection());

            mock.Setup(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3, Test6>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<IQuery<Join<Test1, Test3, Test6>>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((q, p, pa) =>
                {
                    return Enumerable.Empty<Join<Test1, Test3, Test6>>();
                });
            ConnectionOptions<IDbConnection> connectionOptions = new ConnectionOptions<IDbConnection>(new Statements(), mock.Object);

            var query = EntityExecute<Test1>.Select(connectionOptions)
                                            .LeftJoin<Test3>().GreaterThanOrEqual(x => x.Table2.Ids, x => x.Table1.Id)
                                            .RightJoin<Test6>().GreaterThanOrEqual(x => x.Table3.Ids, x => x.Table1.Id).Build();
            var result = query.Execute();

            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id >= Test1.Id RIGHT JOIN TableName ON TableName.Id >= Test1.Id;", query.Text);
            Assert.Empty(result);
            mock.Verify(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3, Test6>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()));
        }

        [Fact]
        public void LessThanOrEqual_Join_Three_Table()
        {
            Mock<IDatabaseManagement<IDbConnection>> mock = new Mock<IDatabaseManagement<IDbConnection>>();
            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.GetConnection()).Returns(() => LoadGSqlQueryOptions.GetIDbConnection());

            mock.Setup(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3, Test6>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<IQuery<Join<Test1, Test3, Test6>>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((q, p, pa) =>
                {
                    return Enumerable.Empty<Join<Test1, Test3, Test6>>();
                });
            ConnectionOptions<IDbConnection> connectionOptions = new ConnectionOptions<IDbConnection>(new Statements(), mock.Object);

            var query = EntityExecute<Test1>.Select(connectionOptions)
                                            .LeftJoin<Test3>().LessThanOrEqual(x => x.Table2.Ids, x => x.Table1.Id)
                                            .RightJoin<Test6>().LessThanOrEqual(x => x.Table3.Ids, x => x.Table1.Id).Build();
            var result = query.Execute();

            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id <= Test1.Id RIGHT JOIN TableName ON TableName.Id <= Test1.Id;", query.Text);
            Assert.Empty(result);
            mock.Verify(x => x.ExecuteReader(It.IsAny<IQuery<Join<Test1, Test3, Test6>>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()));
        }

    }
}
