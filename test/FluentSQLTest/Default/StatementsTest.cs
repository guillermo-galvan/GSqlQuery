using FluentSQL.Default;

namespace FluentSQLTest.Default
{
    public class StatementsTest
    {
        [Fact]
        public void Declarations_cannot_be_null_or_different_than_default()
        {
            Statements statements = new();

            Assert.NotNull(statements.Format);
            Assert.Equal("{0}", statements.Format);

            Assert.NotNull(statements.Select);
            Assert.Equal("SELECT {0} FROM {1};", statements.Select);

            Assert.NotNull(statements.SelectWhere);
            Assert.Equal("SELECT {0} FROM {1} WHERE {2};", statements.SelectWhere);

            Assert.NotNull(statements.Insert);
            Assert.Equal("INSERT INTO {0} ({1}) VALUES ({2});", statements.Insert);

            Assert.NotNull(statements.Update);
            Assert.Equal("UPDATE {0} SET {1} WHERE {2};", statements.Update);

            Assert.NotNull(statements.DeleteWhere);
            Assert.Equal("DELETE FROM {0} WHERE {1};", statements.DeleteWhere);
        }
    }
}
