﻿using GSqlQuery.Extensions;
using GSqlQuery.Test.Models;
using System;
using Xunit;

namespace GSqlQuery.Test
{
    public class ColumnAttributeTest
    {
        [Theory]
        [InlineData("Default")]
        [InlineData("My")]
        public void Default_values_with_column_name_in_the_Constructor(string name)
        {
            ColumnAttribute column = new ColumnAttribute(name);

            Assert.NotNull(column);
            Assert.NotNull(column.Name);
            Assert.False(column.IsPrimaryKey);
            Assert.False(column.IsAutoIncrementing);
            Assert.Equal(0, column.Size);
            Assert.Equal(name, column.Name);
        }

        [Theory]
        [InlineData("Default", 45)]
        [InlineData("My", 69)]
        public void Default_values_with_column_name_and_size_in_the_Constructor(string name, int size)
        {
            ColumnAttribute column = new ColumnAttribute(name) { Size = size };

            Assert.NotNull(column);
            Assert.NotNull(column.Name);
            Assert.False(column.IsPrimaryKey);
            Assert.False(column.IsAutoIncrementing);
            Assert.Equal(size, column.Size);
            Assert.Equal(name, column.Name);
            Assert.Equal($"Column Name: {name}", column.ToString());
        }

        [Theory]
        [InlineData("Default", 45, true)]
        [InlineData("My", 69, false)]
        public void Default_values_with_column_name_size_and_isprimarykey_in_the_Constructor(string name, int size, bool isPrimaryKey)
        {
            ColumnAttribute column = new ColumnAttribute(name) { Size = size, IsPrimaryKey = isPrimaryKey };

            Assert.NotNull(column);
            Assert.NotNull(column.Name);
            Assert.False(column.IsAutoIncrementing);
            Assert.Equal(isPrimaryKey, column.IsPrimaryKey);
            Assert.Equal(size, column.Size);
            Assert.Equal(name, column.Name);
            Assert.Equal($"Column Name: {name}", column.ToString());
        }

        [Theory]
        [InlineData("Default", 45, true, false)]
        [InlineData("My", 69, false, true)]
        public void Default_values_with_column_name_size_isprimarykey_and_isautoincrementing_in_the_Constructor(string name, int size, bool isPrimaryKey, bool isAutoIncrementing)
        {
            ColumnAttribute column = new ColumnAttribute(name) { Size = size, IsPrimaryKey = isPrimaryKey, IsAutoIncrementing = isAutoIncrementing };

            Assert.NotNull(column);
            Assert.NotNull(column.Name);
            Assert.Equal(isAutoIncrementing, column.IsAutoIncrementing);
            Assert.Equal(isPrimaryKey, column.IsPrimaryKey);
            Assert.Equal(size, column.Size);
            Assert.Equal(name, column.Name);
            Assert.Equal($"Column Name: {name}", column.ToString());
        }

        [Fact]
        public void Should_get_the_column_name()
        {
            ColumnAttribute column = new ColumnAttribute("Test");
            var result = column.GetColumnName("Test", new DefaultFormats(), QueryType.Read);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal("Test.Test", result);
            Assert.Equal($"Column Name: Test", column.ToString());
        }

        [Fact]
        public void Throw_exception_if_any_null_parameters_are_passed()
        {
            ColumnAttribute column = new ColumnAttribute("Test");

            Assert.Throws<ArgumentNullException>(() => column.GetColumnName(null, new DefaultFormats(), QueryType.Read));
            Assert.Throws<ArgumentNullException>(() => column.GetColumnName("Test", null, QueryType.Read));
            column = null;
            Assert.Throws<ArgumentNullException>(() => column.GetColumnName("Test", new DefaultFormats(), QueryType.Read));
        }

        [Theory]
        [InlineData(typeof(Test1))]
        [InlineData(typeof(Test3))]
        [InlineData(typeof(Test4))]
        [InlineData(typeof(Test6))]
        public void Should_get_property_options(Type type)
        {
            ClassOptions classOptions = ClassOptionsFactory.GetClassOptions(type);

            foreach (var item in classOptions.PropertyOptions)
            {
                var tmp = item.ColumnAttribute.GetPropertyOptions(classOptions.PropertyOptions);
                Assert.Equal(item.ColumnAttribute, tmp.ColumnAttribute);
            }
        }
        
        [Fact]
        public void Throw_exception_if_name_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new ColumnAttribute(null));
        }
    }
}