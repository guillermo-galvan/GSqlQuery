using GSqlQuery.Queries;
using GSqlQuery.Test.Models;
using System;
using System.Collections.Generic;
using Xunit;
using GSqlQuery.Extensions;
using System.Reflection;
using System.Linq.Expressions;

namespace GSqlQuery.Test.Queries
{
    public class SelectQueryBuilderTest
    {
        private readonly QueryOptions _queryOptions;

        public SelectQueryBuilderTest()
        {
            _queryOptions = new QueryOptions(new DefaultFormats());
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            SelectQueryBuilder<Test1> queryBuilder = new SelectQueryBuilder<Test1>(dynamicQuery, _queryOptions);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.QueryOptions);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
        }

        [Fact]
        public void Properties_cannot_be_null_with_properties()
        {
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            SelectQueryBuilder<Test1> queryBuilder = new SelectQueryBuilder<Test1>(dynamicQuery, _queryOptions);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.QueryOptions);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            DynamicQuery dynamicQuery = null;
            Assert.Throws<ArgumentNullException>(() => new SelectQueryBuilder<Test1>(dynamicQuery, _queryOptions));

            dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            Assert.Throws<ArgumentNullException>(() => new SelectQueryBuilder<Test1>(dynamicQuery, null));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface()
        {
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            SelectQueryBuilder<Test1> queryBuilder = new SelectQueryBuilder<Test1>(dynamicQuery,  _queryOptions);
            IWhere<Test1, SelectQuery<Test1>, QueryOptions> where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_delete_query()
        {
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            SelectQueryBuilder<Test1> queryBuilder = new SelectQueryBuilder<Test1>(dynamicQuery, _queryOptions);
            IQuery<Test1, QueryOptions> query = queryBuilder.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.QueryOptions.Formats);
            Assert.NotNull(query.Criteria);
            Assert.Empty(query.Criteria);
        }
    }
}