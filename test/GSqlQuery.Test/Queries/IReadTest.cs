using GSqlQuery.Test.Data;
using GSqlQuery.Test.Models;
using System;
using Xunit;

namespace GSqlQuery.Test
{
    public class IReadTest
    {
        private readonly IFormats _formats;
        public IReadTest()
        {
            _formats = new DefaultFormats();
        }

        [Fact]
        public void Retrieve_all_properties_from_the_query()
        {
            IQueryBuilderWithWhere<SelectQuery<Test1>, IFormats> queryBuilder = Entity<Test1>.Select(_formats);
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Build().Text);
            Assert.Equal("SELECT Test1.Id,Test1.Name,Test1.Create,Test1.IsTest FROM Test1;", queryBuilder.Build().Text);
        }

        [Fact]
        public void Throw_an_exception_if_null_key_is_passed()
        {
            Assert.Throws<ArgumentNullException>(() => Entity<Test1>.Select(null));
        }

        [Fact]
        public void Throw_an_exception_if_null_key_is_passed_1()
        {
            Assert.Throws<ArgumentNullException>(() => Entity<Test1>.Select(null, (x) => x.IsTest));
        }

        [Fact]
        public void Throw_exception_if_property_is_not_selected()
        {
            Assert.Throws<InvalidOperationException>(() => Entity<Test1>.Select(_formats, x => x));
        }

        [Theory]
        [ClassData(typeof(Select_Test1_TestData))]
        public void Retrieve_all_properties_of_the_query(IFormats formats, string query)
        {
            IQueryBuilderWithWhere<SelectQuery<Test1>, IFormats> queryBuilder = Entity<Test1>.Select(formats);
            Assert.NotNull(queryBuilder);
            var result = queryBuilder.Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Theory]
        [ClassData(typeof(Select_Test1_TestData2))]
        public void Retrieve_some_properties_from_the_query(IFormats formats, string query)
        {
            IQueryBuilderWithWhere<SelectQuery<Test1>, IFormats> queryBuilder = Entity<Test1>.Select(formats, x => new { x.Id, x.Name, x.Create });
            Assert.NotNull(queryBuilder);
            var result = queryBuilder.Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
        }

        [Fact]
        public void Throw_an_exception_if_the_class_has_no_properties()
        {
            Assert.Throws<Exception>(() => Entity<Test2>.Select(_formats, x => x));
        }

        [Theory]
        [ClassData(typeof(Select_Test3_TestData))]
        public void Retrieve_all_properties_from_the_query_with_attributes(IFormats formats, string query)
        {
            IJoinQueryBuilder<Test3, SelectQuery<Test3>, IFormats> queryBuilder = Entity<Test3>.Select(formats);
            Assert.NotNull(queryBuilder);
            var result = queryBuilder.Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Theory]
        [ClassData(typeof(Select_Test4_TestData))]
        public void Retrieve_all_properties_from_the_query_with_attributes_and_scheme(IFormats formats, string query)
        {
            IJoinQueryBuilder<Test4, SelectQuery<Test4>, IFormats> queryBuilder = Entity<Test4>.Select(formats);
            Assert.NotNull(queryBuilder);
            var result = queryBuilder.Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }
    }
}