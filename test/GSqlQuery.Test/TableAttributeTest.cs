using GSqlQuery.Extensions;
using System;
using Xunit;

namespace GSqlQuery.Test
{
    public class TableAttributeTest
    {
        [Theory]
        [InlineData("Default", "table")]
        [InlineData("My", "table1")]
        public void Default_values_with_secheme_name_and_table_name_in_the_Constructor(string scheme, string name)
        {
            TableAttribute table = new TableAttribute(scheme, name);

            Assert.NotNull(table);
            Assert.NotNull(table.Name);
            Assert.NotEmpty(table.Name);
            Assert.Equal(name, table.Name);
            Assert.NotNull(table.Scheme);
            Assert.NotEmpty(table.Scheme);
            Assert.Equal(scheme, table.Scheme);
            Assert.NotEmpty(table.ToString());
        }

        [Theory]
        [InlineData("Default")]
        [InlineData("My")]
        public void Default_values_with_table_name_in_the_Constructor(string name)
        {
            TableAttribute table = new TableAttribute(name);

            Assert.NotNull(table);
            Assert.NotNull(table.Name);
            Assert.NotEmpty(table.Name);
            Assert.Equal(name, table.Name);
            Assert.Null(table.Scheme);
            Assert.NotEmpty(table.ToString());
        }

        [Theory]
        [InlineData("Default", "table")]
        [InlineData("My", "table1")]
        public void Should_get_the_table_name(string scheme, string name)
        {
            TableAttribute table = new TableAttribute(scheme, name);
            var result = table.GetTableName(new DefaultFormats());
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal($"{scheme}.{name}", result);
            Assert.NotEmpty(table.ToString());
        }

        [Fact]
        public void Throw_exception_if_any_null_parameters_are_passed()
        {
            Assert.Throws<ArgumentNullException>(() => new TableAttribute(null));
            Assert.Throws<ArgumentNullException>(() => new TableAttribute(null, null));
            Assert.Throws<ArgumentNullException>(() => new TableAttribute(null, "Test"));

            TableAttribute table = new TableAttribute("Test");
            Assert.Throws<ArgumentNullException>(() => table.GetTableName(null));
            table = null;
            Assert.Throws<ArgumentNullException>(() => table.GetTableName(new DefaultFormats()));
        }
    }
}