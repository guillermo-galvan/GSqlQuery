using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class DefaultFormatsTest
    {
        [Fact]
        public void Declarations_cannot_be_null_or_different_than_default()
        {
            DefaultFormats statements = new DefaultFormats();

            Assert.NotNull(statements.Format);
            Assert.Equal("{0}", statements.Format);

            Assert.NotNull(statements.ValueAutoIncrementingQuery);
            Assert.Equal("", statements.ValueAutoIncrementingQuery);

            ColumnAttribute column = new ColumnAttribute("test");

            var columnName = statements.GetColumnName("table", column, QueryType.Read);

            Assert.NotNull(columnName);
            Assert.Equal("table.test", columnName);
        }
    }
}