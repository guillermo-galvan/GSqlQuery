using FluentSQLTest.Models;

namespace FluentSQLTest
{
    public class StatementsCollectionTest
    {
        [Fact]
        public void Throw_exception_if_key_is_null()
        {
            StatementsCollection statements = new();
            Assert.Throws<ArgumentNullException>(() => statements.Add(null, new Statements()));
        }

        [Fact]
        public void Throw_exception_if_statements_is_null()
        {
            StatementsCollection statements = new();
            Assert.Throws<ArgumentNullException>(() => statements.Add("Default", null));
        }

        [Fact]
        public void Throw_exception_if_key_is_duplicate()
        {
            StatementsCollection statements = new();
            statements.Add("Default", new Statements());
            Assert.Throws<ArgumentException>(() => statements.Add("Default", new Statements()));
        }

        [Fact]
        public void Should_return_all_the_keys()
        {
            StatementsCollection statements = new();
            statements.Add("Default", new Statements());
            var result = statements.GetAllKeys();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_clean_all_the_keys()
        {
            StatementsCollection statements = new();
            statements.Add("Default", new Statements());
            statements.Clear();
            var result = statements.GetAllKeys();
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Should_retrieve_statements_by_key()
        {
            StatementsCollection statements = new();
            statements.Add("Default", new Statements());
            var result = statements["Default"];
            Assert.NotNull(result);
        }
    }
}
