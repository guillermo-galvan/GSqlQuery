using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using GSqlQuery.Test.Models;
using System;
using System.Linq;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class CountQueryBuilderTest
    {
        private readonly QueryOptions _queryOptions;

        public CountQueryBuilderTest()
        {
            _queryOptions = new QueryOptions(new DefaultFormats());
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            SelectQueryBuilder<Test1> queryBuilder = new SelectQueryBuilder<Test1>(ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create }), _queryOptions);

            var result = queryBuilder.Count();

            Assert.NotNull(result);
            Assert.NotNull(result.QueryOptions);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
            Assert.Equal(queryBuilder.Columns.Count, result.Columns.Count);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            SelectQueryBuilder<Test1> queryBuilder = null;
            Assert.Throws<ArgumentNullException>(() => queryBuilder.Count());
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface()
        {
            SelectQueryBuilder<Test1> queryBuilder = new SelectQueryBuilder<Test1>(ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create }), _queryOptions);
            var result = queryBuilder.Count();
            IWhere<Test1, CountQuery<Test1>, QueryOptions> where = result.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_count_query()
        {
            SelectQueryBuilder<Test1> queryBuilder = new SelectQueryBuilder<Test1>(ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id }),
                _queryOptions);
            var result = queryBuilder.Count();
            IQuery<Test1, QueryOptions> query = result.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.QueryOptions);
            Assert.NotNull(query.Criteria);
            Assert.Empty(query.Criteria);
        }
    }
}