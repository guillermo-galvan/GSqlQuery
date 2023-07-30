using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class OrderByQueryBuilderTest
    {
        private readonly IStatements _stantements;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;

        public OrderByQueryBuilderTest()
        {
            _stantements = new Statements();
            _connectionOptions = new ConnectionOptions<IDbConnection>(_stantements, LoadGSqlQueryOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Should_return_an_orderBy_query2()
        {
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> queryBuilder =
                new SelectQueryBuilder<Test1, IDbConnection>(new List<string> { nameof(Test1.Id) },
                _connectionOptions);
            var result = queryBuilder.OrderBy(x => x.Id, OrderBy.ASC).OrderBy(x => new { x.Name, x.Create }, OrderBy.DESC);
            var query = result.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.Statements);
            Assert.Null(query.Criteria);
        }

        [Fact]
        public void Should_return_an_orderBy_query_with_where2()
        {
            var queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(new List<string> { nameof(Test1.Id) }, _connectionOptions)
                            .Where().Equal(x => x.IsTest, true).OrEqual(x => x.IsTest, false);
            var result = queryBuilder.OrderBy(x => x.Id, OrderBy.ASC).OrderBy(x => new { x.Name, x.Create }, OrderBy.DESC);
            var query = result.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.Criteria);
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);

            var result = queryBuilder.OrderBy(x => x.Id, OrderBy.ASC);

            Assert.NotNull(result);
            Assert.NotNull(result.Options);
            Assert.NotNull(result.Options.Statements);
            Assert.NotNull(result.Options.DatabaseManagement);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
            Assert.Equal(queryBuilder.Columns.Count(), result.Columns.Count());
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters2()
        {
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> queryBuilder = null;
            Assert.Throws<ArgumentNullException>(() => queryBuilder.OrderBy(x => x.Id, OrderBy.ASC));
        }
    }
}