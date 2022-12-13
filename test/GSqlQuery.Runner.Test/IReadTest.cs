using GSqlQuery.Runner.Models;
using GSqlQuery.Runner.Test.Data;
using GSqlQuery.Runner.Test.Models;
using System.Data.Common;

namespace GSqlQuery.Runner.Test
{
    public class IReadTest
    {
        private readonly ConnectionOptions<DbConnection> _connectionOptions;

        public IReadTest()
        {
            _connectionOptions = new ConnectionOptions<DbConnection>(new GSqlQuery.Default.Statements(), LoadFluentOptions.GetDatabaseManagmentMock());
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