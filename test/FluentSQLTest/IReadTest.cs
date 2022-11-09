using FluentSQL.Default;
using FluentSQL.Models;
using FluentSQLTest.Data;
using FluentSQLTest.Models;
using System.Data;
using System.Data.Common;

namespace FluentSQLTest
{
    public class IReadTest
    {
        private readonly IStatements _statements;
        private readonly ConnectionOptions<DbConnection> _connectionOptions;
        public IReadTest()
        {
            _statements = new FluentSQL.Default.Statements();
            _connectionOptions = new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Retrieve_all_properties_from_the_query()
        {
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1>> queryBuilder = IRead<Test1>.Select(_statements);
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Build().Text);
            Assert.Equal("SELECT Test1.Id,Test1.Name,Test1.Create,Test1.IsTest FROM Test1;", queryBuilder.Build().Text);
        }

        [Fact]
        public void Throw_an_exception_if_null_key_is_passed()
        {
            Assert.Throws<ArgumentNullException>(() => IRead<Test1>.Select(null));
        }

        [Fact]
        public void Throw_an_exception_if_null_key_is_passed_1()
        {
            Assert.Throws<ArgumentNullException>(() => IRead<Test1>.Select(null, (x) => x.IsTest));
        }

        [Fact]
        public void Throw_exception_if_property_is_not_selected()
        {
            Assert.Throws<InvalidOperationException>(() => IRead<Test1>.Select(_statements, x => x));
        }

        [Theory]
        [ClassData(typeof(Select_Test1_TestData))]
        public void Retrieve_all_properties_of_the_query(IStatements statements, string query)
        {
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1>> queryBuilder = IRead<Test1>.Select(statements);
            Assert.NotNull(queryBuilder);
            var result = queryBuilder.Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Theory]
        [ClassData(typeof(Select_Test1_TestData2))]
        public void Retrieve_some_properties_from_the_query(IStatements statements, string query)
        {
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1>> queryBuilder = IRead<Test1>.Select(statements, x => new { x.Id, x.Name, x.Create });
            Assert.NotNull(queryBuilder);
            var result = queryBuilder.Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
        }

        [Fact]
        public void Throw_an_exception_if_the_class_has_no_properties()
        {
            Assert.Throws<Exception>(() => IRead<Test2>.Select(_statements,x => x));
        }

        [Theory]
        [ClassData(typeof(Select_Test3_TestData))]
        public void Retrieve_all_properties_from_the_query_with_attributes(IStatements statements, string query)
        {
            IQueryBuilderWithWhere<Test3, SelectQuery<Test3>> queryBuilder = IRead<Test3>.Select(statements);
            Assert.NotNull(queryBuilder);
            var result = queryBuilder.Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Theory]
        [ClassData(typeof(Select_Test4_TestData))]
        public void Retrieve_all_properties_from_the_query_with_attributes_and_scheme(IStatements statements, string query)
        {
            IQueryBuilderWithWhere<Test4, SelectQuery<Test4>> queryBuilder = IRead<Test4>.Select(statements);
            Assert.NotNull(queryBuilder);
            var result = queryBuilder.Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Fact]
        public void Retrieve_all_properties_from_the_query2()
        {
            var queryBuilder = IRead<Test1>.Select(_connectionOptions);
            Assert.NotNull(queryBuilder);            
            Assert.NotEmpty(queryBuilder.Build().Text);
            Assert.Equal("SELECT Test1.Id,Test1.Name,Test1.Create,Test1.IsTest FROM Test1;", queryBuilder.Build().Text);
        }

        [Fact]
        public void Throw_an_exception_if_null_connectionoptions_is_passed()
        {
            ConnectionOptions<DbConnection> connectionOptions = null;
            Assert.Throws<ArgumentNullException>(() => IRead<Test1>.Select(connectionOptions));
        }

        [Fact]
        public void Throw_an_exception_if_null_connectionoptions_is_passed_1()
        {
            ConnectionOptions<DbConnection> connectionOptions = null;
            Assert.Throws<ArgumentNullException>(() => IRead<Test1>.Select(connectionOptions, (x) => x.IsTest));
        }

        [Fact]
        public void Throw_exception_if_property_is_not_selected2()
        {
            ConnectionOptions<DbConnection> connectionOptions = null;
            Assert.Throws<InvalidOperationException>(() => IRead<Test1>.Select(_connectionOptions, x => x));
        }

        [Theory]
        [ClassData(typeof(Select_Test1_TestData_ConnectionOptions))]
        public void Retrieve_all_properties_of_the_query2(ConnectionOptions<DbConnection> connectionOptions, string query)
        {
            var queryBuilder = IRead<Test1>.Select(connectionOptions);
            Assert.NotNull(queryBuilder);
            var result = queryBuilder.Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Theory]
        [ClassData(typeof(Select_Test1_TestData2_ConnectionOptions))]
        public void Retrieve_some_properties_from_the_query2(ConnectionOptions<DbConnection> connectionOptions, string query)
        {
            var queryBuilder = IRead<Test1>.Select(connectionOptions, x => new { x.Id, x.Name, x.Create });
            Assert.NotNull(queryBuilder);
            var result = queryBuilder.Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
        }

        [Fact]
        public void Throw_an_exception_if_the_class_has_no_properties2()
        {
            Assert.Throws<Exception>(() => IRead<Test2>.Select(_connectionOptions, x => x));
        }

        [Theory]
        [ClassData(typeof(Select_Test3_TestData_ConnectionOptions))]
        public void Retrieve_all_properties_from_the_query_with_attributes2(ConnectionOptions<DbConnection> connectionOptions, string query)
        {
            var queryBuilder = IRead<Test3>.Select(connectionOptions);
            Assert.NotNull(queryBuilder);
            var result = queryBuilder.Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Theory]
        [ClassData(typeof(Select_Test4_TestData_ConnectionOptions))]
        public void Retrieve_all_properties_from_the_query_with_attributes_and_scheme2(ConnectionOptions<DbConnection> connectionOptions, string query)
        {
            var queryBuilder = IRead<Test4>.Select(connectionOptions);
            Assert.NotNull(queryBuilder);
            var result = queryBuilder.Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }
    }
}