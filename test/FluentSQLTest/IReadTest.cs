using FluentSQLTest.Models;

namespace FluentSQLTest
{
    public class IReadTest
    {
        public IReadTest()
        {
            if (!FluentSQLManagement.Options.StatementsCollection.GetAllKeys().Any())
            {
                FluentSQLOptions options = new();
                options.StatementsCollection.Add("Default", new FluentSQL.Default.Statements());
                options.StatementsCollection.Add("My", new Statements());
                FluentSQLManagement.SetOptions(options);
            }
        }

        [Fact]
        public void Retrieve_all_properties_from_the_query()
        {
            IQueryBuilder queryBuilder = IRead<Test1>.Select();
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Build());
            Assert.Equal("SELECT Id,Name,Create,IsTest FROM Test1;", queryBuilder.Build());
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
        [InlineData("Default", "SELECT Id,Name,Create,IsTest FROM Test1;")]
        [InlineData("My", "SELECT [Id],[Name],[Create],[IsTest] FROM [Test1];")]        
        public void Retrieve_all_properties_of_the_query_with_the_key(string key, string query)
        {
            IQueryBuilder queryBuilder = IRead<Test1>.Select(key);
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Build());
            Assert.Equal(query, queryBuilder.Build());
        }

        [Theory]
        [InlineData("Default", "SELECT Id,Name,Create FROM Test1;")]
        [InlineData("My", "SELECT [Id],[Name],[Create] FROM [Test1];")]
        public void Retrieve_some_properties_from_the_query_with_the_key(string key, string query)
        {
            IQueryBuilder queryBuilder = IRead<Test1>.Select(key, x => new { x.Id, x.Name, x.Create });
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Build());
            Assert.Equal(query, queryBuilder.Build());
        }

        [Fact]
        public void Throw_an_exception_if_the_class_has_no_properties()
        {
            Assert.Throws<Exception>(() => IRead<Test2>.Select(x => x));
        }

        [Theory]
        [InlineData("Default", "SELECT Id,Name,Create,IsTests FROM TableName;")]
        [InlineData("My", "SELECT [Id],[Name],[Create],[IsTests] FROM [TableName];")]
        public void Retrieve_all_properties_from_the_query_with_attributes(string key, string query)
        {
            IQueryBuilder queryBuilder = IRead<Test3>.Select(key);
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Build());
            Assert.Equal(query, queryBuilder.Build());
        }

        [Theory]
        [InlineData("Default", "SELECT Id,Name,Create,IsTests FROM Scheme.TableName;")]
        [InlineData("My", "SELECT [Id],[Name],[Create],[IsTests] FROM [Scheme].[TableName];")]
        public void Retrieve_all_properties_from_the_query_with_attributes_and_scheme(string key, string query)
        {
            IQueryBuilder queryBuilder = IRead<Test4>.Select(key);
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Build());
            Assert.Equal(query, queryBuilder.Build());
        }
    }
}