using GSqlQuery.Queries;
using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using System;
using System.Data;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class SelectQueryBuilderTest
    {
        private readonly IFormats _formats;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;

        public SelectQueryBuilderTest()
        {
            _formats = new TestFormats();
            _connectionOptions = new ConnectionOptions<IDbConnection>(_formats, LoadGSqlQueryOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            SelectQueryBuilder<Test1, IDbConnection> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(dynamicQuery, _connectionOptions);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.QueryOptions);
            Assert.NotNull(queryBuilder.QueryOptions.Formats);
            Assert.NotNull(queryBuilder.QueryOptions.DatabaseManagement);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {

            DynamicQuery dynamicQuery = null;
            Assert.Throws<ArgumentNullException>(() => new SelectQueryBuilder<Test1>(dynamicQuery, _connectionOptions));

            dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            Assert.Throws<ArgumentNullException>(() => new SelectQueryBuilder<Test1>(dynamicQuery, null));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface()
        {
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            SelectQueryBuilder<Test1, IDbConnection> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(dynamicQuery, _connectionOptions);
            var where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_delete_query()
        {
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            SelectQueryBuilder<Test1, IDbConnection> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(dynamicQuery, _connectionOptions);
            SelectQuery<Test1, IDbConnection> query = queryBuilder.Build();
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
    }
}