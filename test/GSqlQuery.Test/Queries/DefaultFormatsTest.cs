using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class DefaultFormatsTest
    {
        [Fact]
        public void Declarations_cannot_be_null_or_different_than_default()
        {
            DefaultFormats formats = new DefaultFormats();

            Assert.NotNull(formats.Format);
            Assert.Equal("{0}", formats.Format);

            Assert.NotNull(formats.ValueAutoIncrementingQuery);
            Assert.Equal("", formats.ValueAutoIncrementingQuery);

            ColumnAttribute column = new ColumnAttribute("test");

            var columnName = formats.GetColumnName("table", column, QueryType.Read);

            Assert.NotNull(columnName);
            Assert.Equal("table.test", columnName);
        }
    }
}