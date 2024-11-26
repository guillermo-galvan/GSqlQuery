using GSqlQuery.Queries;
using GSqlQuery.Test.Models;
using System;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class OrderByQueryBuilderTest
    {
        private readonly QueryOptions _queryOptions;

        public OrderByQueryBuilderTest()
        {
            _queryOptions = new QueryOptions(new DefaultFormats());
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            SelectQueryBuilder<Test1> queryBuilder = new SelectQueryBuilder<Test1>(dynamicQuery, _queryOptions);

            var result = queryBuilder.OrderBy(x => x.Id, OrderBy.ASC);

            Assert.NotNull(result);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            SelectQueryBuilder<Test1> queryBuilder = null;
            Assert.Throws<ArgumentNullException>(() => queryBuilder.OrderBy(x => x.Id, OrderBy.ASC));
        }

        [Fact]
        public void Should_return_an_orderBy_query()
        {
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id });
            SelectQueryBuilder<Test1> queryBuilder = new SelectQueryBuilder<Test1>(dynamicQuery, _queryOptions);
            var result = queryBuilder.OrderBy(x => new { x.Id }, OrderBy.ASC).OrderBy(x => new { x.Name, x.Create }, OrderBy.DESC);
            IQuery<Test1, QueryOptions> query = result.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.QueryOptions);
            Assert.NotNull(query.Criteria);
            Assert.Empty(query.Criteria);
        }

        [Fact]
        public void Should_return_an_orderBy_query_with_where()
        {
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id });
            var queryBuilder = new SelectQueryBuilder<Test1>(dynamicQuery, _queryOptions)
                                   .Where().Equal(x => x.IsTest, true).OrEqual(x => x.IsTest, false);
            var result = queryBuilder.OrderBy(x => new { x.Id }, OrderBy.ASC).OrderBy(x => new { x.Name, x.Create }, OrderBy.DESC);
            IQuery<Test1, QueryOptions> query = result.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.QueryOptions.Formats);
            Assert.NotNull(query.Criteria);
        }
    }
}