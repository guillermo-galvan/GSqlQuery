using FluentSQL.Models;
using FluentSQLTest.Models;

namespace FluentSQLTest
{
    public class ConnectionCollectionTest
    {
        [Fact]
        public void Throw_exception_if_key_is_null()
        {
            ConnectionCollection collection = new();
            Assert.Throws<ArgumentNullException>(() => collection.Add(null, new ConnectionOptions(new Statements())));
        }

        [Fact]
        public void Throw_exception_if_statements_is_null()
        {
            ConnectionCollection collection = new();
            Assert.Throws<ArgumentNullException>(() => collection.Add("Default", null));
        }

        [Fact]
        public void Throw_exception_if_key_is_duplicate()
        {
            ConnectionCollection collection = new();
            collection.Add("Default", new ConnectionOptions(new Statements()));
            Assert.Throws<ArgumentException>(() => collection.Add("Default", new ConnectionOptions(new Statements())));
        }

        [Fact]
        public void Should_return_all_the_keys()
        {
            ConnectionCollection collection = new();
            collection.Add("Default", new ConnectionOptions(new Statements()));
            var result = collection.GetAllKeys();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_clean_all_the_keys()
        {
            ConnectionCollection collection = new();
            collection.Add("Default", new ConnectionOptions(new Statements()));
            collection.Clear();
            var result = collection.GetAllKeys();
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Should_retrieve_statements_by_key()
        {
            ConnectionCollection collection = new();
            collection.Add("Default", new ConnectionOptions(new Statements()));
            var result = collection["Default"];
            Assert.NotNull(result);
        }
    }
}
