using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;
using GSqlQuery.Extensions;

namespace GSqlQuery.Runner.Test.Queries
{
    public class CountQueryBuilderTest
    {
        private readonly IFormats _formats;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;

        public CountQueryBuilderTest()
        {
            _formats = new DefaultFormats();
            _connectionOptions = new ConnectionOptions<IDbConnection>(_formats, LoadGSqlQueryOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Should_return_an_count_query2()
        {
            IQueryBuilderWithWhere<Test3, SelectQuery<Test3, IDbConnection>, ConnectionOptions<IDbConnection>> queryBuilder = Test3.Select(_connectionOptions, x => x.Ids);
            var result = queryBuilder.Count();
            var query = result.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.QueryOptions);
            Assert.NotNull(query.QueryOptions.DatabaseManagement);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.Criteria);
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            var columsn = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(columsn, _connectionOptions);

            var result = queryBuilder.Count();

            Assert.NotNull(result);
            Assert.NotNull(result.QueryOptions);
            Assert.NotNull(result.QueryOptions.Formats);
            Assert.NotNull(result.QueryOptions.DatabaseManagement);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
            Assert.Equal(queryBuilder.Columns.Count(), result.Columns.Count());
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters2()
        {
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> queryBuilder = null;
            Assert.Throws<ArgumentNullException>(() => queryBuilder.Count());
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface2()
        {
            var columsn = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(columsn,  _connectionOptions);
            var result = queryBuilder.Count();
            var where = result.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface3()
        {
            var columsn = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(columsn, _connectionOptions);
            IQueryBuilderWithWhere<CountQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> result = queryBuilder.Count();
            IWhere<CountQuery<Test1, IDbConnection>> where = result.Where();
            Assert.NotNull(where);
        }
    }
}