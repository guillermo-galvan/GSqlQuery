using GSqlQuery;
using GSqlQuery.Runner;
using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class CountQueryBuilderTest
    {
        private readonly IStatements _stantements;
        private readonly ConnectionOptions<DbConnection> _connectionOptions;

        public CountQueryBuilderTest()
        {
            _stantements = new Statements();
            _connectionOptions = new ConnectionOptions<DbConnection>(_stantements, LoadFluentOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Should_return_an_count_query2()
        {
            SelectQueryBuilder<Test1, DbConnection> queryBuilder = new SelectQueryBuilder<Test1, DbConnection>(new List<string> { nameof(Test1.Id) },
                _connectionOptions);
            var result = queryBuilder.Count();
            var query = result.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.DatabaseManagement);
            Assert.Null(query.Criteria);
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            SelectQueryBuilder<Test1, DbConnection> queryBuilder = new SelectQueryBuilder<Test1, DbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);

            var result = queryBuilder.Count();

            Assert.NotNull(result);
            Assert.NotNull(result.ConnectionOptions);
            Assert.NotNull(result.ConnectionOptions.Statements);
            Assert.NotNull(result.ConnectionOptions.DatabaseManagement);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
            Assert.Equal(queryBuilder.Columns.Count(), result.Columns.Count());
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters2()
        {
            SelectQueryBuilder<Test1, DbConnection> queryBuilder = null;
            Assert.Throws<ArgumentNullException>(() => queryBuilder.Count());
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface2()
        {
            SelectQueryBuilder<Test1, DbConnection> queryBuilder = new SelectQueryBuilder<Test1, DbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);
            var result = queryBuilder.Count();
            var where = result.Where();
            Assert.NotNull(where);
        }
    }
}
