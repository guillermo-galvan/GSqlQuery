using GSqlQuery.Queries;
using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using System;
using System.Data;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class OrderByQueryBuilderTest
    {
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;

        public OrderByQueryBuilderTest()
        {
            _connectionOptions = new ConnectionOptions<IDbConnection>(new TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Should_return_an_orderBy_query2()
        {
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(dynamicQuery, _connectionOptions);
            var result = queryBuilder.OrderBy(x => x.Id, OrderBy.ASC).OrderBy(x => new { x.Name, x.Create }, OrderBy.DESC);
            var query = result.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.QueryOptions);
            Assert.NotNull(query.QueryOptions.Formats);
            Assert.NotNull(query.QueryOptions.DatabaseManagement);
            Assert.NotNull(query.Criteria);
        }

        [Fact]
        public void Should_return_an_orderBy_query_with_where2()
        {
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            var queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(dynamicQuery, _connectionOptions).Where()
                                                                                                        .Equal(x => x.IsTest, true)
                                                                                                        .OrEqual(x => x.IsTest, false);
            var result = queryBuilder.OrderBy(x => x.Id, OrderBy.ASC).OrderBy(x => new { x.Name, x.Create }, OrderBy.DESC);
            var query = result.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.DatabaseManagement); 
            Assert.NotNull(query.QueryOptions);
            Assert.NotNull(query.QueryOptions.Formats);
            Assert.NotNull(query.QueryOptions.DatabaseManagement);
            Assert.NotNull(query.Criteria);
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(dynamicQuery, _connectionOptions);

            var result = queryBuilder.OrderBy(x => x.Id, OrderBy.ASC);

            Assert.NotNull(result);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters2()
        {
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> queryBuilder = null;
            Assert.Throws<ArgumentNullException>(() => queryBuilder.OrderBy(x => x.Id, OrderBy.ASC));
        }
    }
}