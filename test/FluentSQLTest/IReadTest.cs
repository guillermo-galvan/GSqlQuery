using FluentSQL.Default;
using FluentSQLTest.Models;

namespace FluentSQLTest
{
    public class IReadTest
    {
        public IReadTest()
        {
            LoadFluentOptions.Load();
        }

        [Fact]
        public void Retrieve_all_properties_from_the_query()
        {
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1>> queryBuilder = IRead<Test1>.Select();
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
            Assert.Throws<InvalidOperationException>(() => IRead<Test1>.Select(x => x));
        }

        [Theory]
        [InlineData("Default", "SELECT Test1.Id,Test1.Name,Test1.Create,Test1.IsTest FROM Test1;")]
        [InlineData("My", "SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test1];")]
        public void Retrieve_all_properties_of_the_query_with_the_key(string key, string query)
        {
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1>> queryBuilder = IRead<Test1>.Select(key);
            Assert.NotNull(queryBuilder);
            var result = queryBuilder.Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Theory]
        [InlineData("Default", "SELECT Test1.Id,Test1.Name,Test1.Create FROM Test1;")]
        [InlineData("My", "SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create] FROM [Test1];")]
        public void Retrieve_some_properties_from_the_query_with_the_key(string key, string query)
        {
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1>> queryBuilder = IRead<Test1>.Select(key, x => new { x.Id, x.Name, x.Create });
            Assert.NotNull(queryBuilder);
            var result = queryBuilder.Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Fact]
        public void Throw_an_exception_if_the_class_has_no_properties()
        {
            Assert.Throws<Exception>(() => IRead<Test2>.Select(x => x));
        }

        [Theory]
        [InlineData("Default", "SELECT TableName.Id,TableName.Name,TableName.Create,TableName.IsTests FROM TableName;")]
        [InlineData("My", "SELECT [TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests] FROM [TableName];")]
        public void Retrieve_all_properties_from_the_query_with_attributes(string key, string query)
        {
            IQueryBuilderWithWhere<Test3, SelectQuery<Test3>> queryBuilder = IRead<Test3>.Select(key);
            Assert.NotNull(queryBuilder);
            var result = queryBuilder.Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Theory]
        [InlineData("Default", "SELECT Scheme.TableName.Id,Scheme.TableName.Name,Scheme.TableName.Create,Scheme.TableName.IsTests FROM Scheme.TableName;")]
        [InlineData("My", "SELECT [Scheme].[TableName].[Id],[Scheme].[TableName].[Name],[Scheme].[TableName].[Create],[Scheme].[TableName].[IsTests] FROM [Scheme].[TableName];")]
        public void Retrieve_all_properties_from_the_query_with_attributes_and_scheme(string key, string query)
        {
            IQueryBuilderWithWhere<Test4, SelectQuery<Test4>> queryBuilder = IRead<Test4>.Select(key);
            Assert.NotNull(queryBuilder);
            var result = queryBuilder.Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }
    }
}