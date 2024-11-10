using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using System;
using System.Data;
using System.Data.Common;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class DeleteQueryBuilderTest
    {
        private readonly IFormats _formats;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;
        public DeleteQueryBuilderTest()
        {
            _formats = new TestFormats();
            _connectionOptions = new ConnectionOptions<IDbConnection>(_formats, LoadGSqlQueryOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            DeleteQueryBuilder<Test1, IDbConnection> queryBuilder = new DeleteQueryBuilder<Test1, IDbConnection>(_connectionOptions);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.QueryOptions);
            Assert.NotNull(queryBuilder.QueryOptions.Formats);
            Assert.NotNull(queryBuilder.QueryOptions.DatabaseManagement);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface2()
        {
            DeleteQueryBuilder<Test1, IDbConnection> queryBuilder = new DeleteQueryBuilder<Test1, IDbConnection>(_connectionOptions);
            var where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_delete_query2()
        {
            DeleteQueryBuilder<Test1, IDbConnection> queryBuilder = new DeleteQueryBuilder<Test1, IDbConnection>(_connectionOptions);
            var query = queryBuilder.Build();
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
        public void Should_return_an_implementation_of_the_IWhere_interface3()
        {
            IQueryBuilderWithWhere<DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> queryBuilder = 
                new DeleteQueryBuilder<Test1, IDbConnection>(_connectionOptions);
            var where = queryBuilder.Where();
            Assert.NotNull(where);
        }
    }
}