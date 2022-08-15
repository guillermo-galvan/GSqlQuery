﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentSQL.Default;
using FluentSQL.Extensions; 

namespace FluentSQLTest
{
    public class TableAttributeTest
    {
        [Theory]
        [InlineData("Default","table")]
        [InlineData("My","table1")]
        public void Default_values_with_secheme_name_and_table_name_in_the_Constructor(string scheme,string name)
        {
            TableAttribute table = new(scheme,name);

            Assert.NotNull(table);
            Assert.NotNull(table.Name);
            Assert.NotEmpty(table.Name);
            Assert.Equal(name, table.Name);
            Assert.NotNull(table.Scheme);
            Assert.NotEmpty(table.Scheme);
            Assert.Equal(scheme, table.Scheme);
        }

        [Theory]
        [InlineData("Default")]
        [InlineData("My")]
        public void Default_values_with_table_name_in_the_Constructor(string name)
        {
            TableAttribute table = new(name);

            Assert.NotNull(table);
            Assert.NotNull(table.Name);
            Assert.NotEmpty(table.Name);
            Assert.Equal(name, table.Name);
            Assert.Null(table.Scheme);
        }

        [Theory]
        [InlineData("Default", "table")]
        [InlineData("My", "table1")]
        public void Should_get_the_table_name(string scheme, string name)
        {
            TableAttribute table = new(scheme, name);
            var result = table.GetTableName(new Statements());
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal($"{scheme}.{name}", result);
        }

        [Fact]
        public void Throw_exception_if_any_null_parameters_are_passed()
        {
            TableAttribute table = new("Test");
            Assert.Throws<ArgumentNullException>(() => table.GetTableName(null));
            table = null;
            Assert.Throws<ArgumentNullException>(() => table.GetTableName(new Statements()));
        }
    }
}