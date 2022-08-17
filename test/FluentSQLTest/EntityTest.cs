using FluentSQL.SearchCriteria;
using FluentSQLTest.Default;
using FluentSQLTest.Extensions;
using FluentSQLTest.Models;

namespace FluentSQLTest
{
    public class EntityTest
    {
        public EntityTest()
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
            IQueryBuilder<Test3> queryBuilder = Test3.Select();
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Build().Text);
            Assert.Equal("SELECT Id,Name,Create,IsTests FROM TableName;", queryBuilder.Build().Text);
        }

        [Fact]
        public void Throw_an_exception_if_null_key_is_passed()
        {
            Assert.Throws<ArgumentNullException>(() => Test3.Select(null));
        }

        [Fact]
        public void Throw_an_exception_if_null_key_is_passed_1()
        {
            Assert.Throws<ArgumentNullException>(() => Test3.Select(null, (x) => x.IsTests));
        }

        [Fact]
        public void Throw_exception_if_property_is_not_selected()
        {
            Assert.Throws<InvalidOperationException>(() => Test3.Select(x => x));
        }

        [Theory]
        [InlineData("Default", "SELECT Id,Name,Create,IsTests FROM TableName;")]
        [InlineData("My", "SELECT [Id],[Name],[Create],[IsTests] FROM [TableName];")]
        public void Retrieve_all_properties_of_the_query_with_the_key(string key, string query)
        {
            IQueryBuilder<Test3> queryBuilder = Test3.Select(key);
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Build().Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Theory]
        [InlineData("Default", "SELECT Id,Name,Create FROM TableName;")]
        [InlineData("My", "SELECT [Id],[Name],[Create] FROM [TableName];")]
        public void Retrieve_some_properties_from_the_query_with_the_key(string key, string query)
        {
            IQueryBuilder<Test3> queryBuilder = Test3.Select(key, x => new { x.Ids, x.Names, x.Creates });
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Build().Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Theory]
        [InlineData("Default", "SELECT Id,Name,Create FROM TableName WHERE TableName.IsTests = @Param AND TableName.Id = @Param;")]
        [InlineData("My", "SELECT [Id],[Name],[Create] FROM [TableName] WHERE [TableName].[IsTests] = @Param AND [TableName].[Id] = @Param;")]
        public void Should_return_the_query_with_where(string key, string queryText)
        {
            var query = Test3.Select(key, x => new { x.Ids, x.Names, x.Creates }).Where().Equal(x => x.IsTests, true).AndEqual(x => x.Ids, 12).Build();
            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }

            Assert.Equal(queryText, result);
        }

        [Theory]
        [InlineData("Default", "INSERT INTO TableName (Id,Name,Create,IsTests) VALUES (@Param,@Param,@Param,@Param);")]
        [InlineData("My", "INSERT INTO [TableName] ([Id],[Name],[Create],[IsTests]) VALUES (@Param,@Param,@Param,@Param);")]
        public void Should_generate_the_insert_query_with_key(string key, string queryResult)
        {
            Test3 test = new (1, null, DateTime.Now, true);
            var query = test.Insert(key);

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }
            Assert.Equal(queryResult, result);
        }

        [Theory]
        [InlineData("INSERT INTO TableName (Id,Name,Create,IsTests) VALUES (@Param,@Param,@Param,@Param);")]        
        public void Should_generate_the_insert_query(string queryResult)
        {
            Test3 test = new (1, null, DateTime.Now, true);
            var query = test.Insert();

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }
            Assert.Equal(queryResult, result);
        }

    }
}
