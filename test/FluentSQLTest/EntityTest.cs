using FluentSQLTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            IQueryBuilder queryBuilder = Test3.Select();
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Text);
            Assert.Equal("SELECT Id,Name,Create,IsTests FROM TableName;", queryBuilder.Text);
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
            IQueryBuilder queryBuilder = Test3.Select(key);
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Text);
            Assert.Equal(query, queryBuilder.Text);
        }

        [Theory]
        [InlineData("Default", "SELECT Id,Name,Create FROM TableName;")]
        [InlineData("My", "SELECT [Id],[Name],[Create] FROM [TableName];")]
        public void Retrieve_some_properties_from_the_query_with_the_key(string key, string query)
        {
            IQueryBuilder queryBuilder = Test3.Select(key, x => new { x.Ids, x.Names, x.Creates });
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Text);
            Assert.Equal(query, queryBuilder.Text);
        }
    }
}
