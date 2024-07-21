using GSqlQuery.Queries;
using GSqlQuery.Test.Models;
using System;
using System.Collections.Generic;
using Xunit;
using GSqlQuery.Extensions;
using System.Reflection;

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
            SelectQueryBuilder<Test1> queryBuilder = new SelectQueryBuilder<Test1>(ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create }),
                _queryOptions);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.QueryOptions);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
        }

        [Fact]
        public void Properties_cannot_be_null_with_properties()
        {
            var propertyOptions = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            SelectQueryBuilder<Test1> queryBuilder = new SelectQueryBuilder<Test1>(propertyOptions, _queryOptions);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.QueryOptions);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            ClassOptionsTupla<PropertyOptionsCollection> ddsds = null;
            Assert.Throws<ArgumentNullException>(() => new SelectQueryBuilder<Test1>(ddsds, _queryOptions));
            Assert.Throws<ArgumentNullException>(() => new SelectQueryBuilder<Test1>(ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create }), null));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface()
        {
            SelectQueryBuilder<Test1> queryBuilder = new SelectQueryBuilder<Test1>(ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create }),  _queryOptions);
            IWhere<Test1, SelectQuery<Test1>, QueryOptions> where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_delete_query()
        {
            SelectQueryBuilder<Test1> queryBuilder = new SelectQueryBuilder<Test1>(ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create }), _queryOptions);
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