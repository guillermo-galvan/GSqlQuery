using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class CountQueryBuilderTest
    {
        private readonly IStatements _stantements;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;

        public CountQueryBuilderTest()
        {
            _stantements = new Statements();
            _connectionOptions = new ConnectionOptions<IDbConnection>(_stantements, LoadGSqlQueryOptions.GetDatabaseManagmentMock());
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
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.DatabaseManagement);
            Assert.Null(query.Criteria);
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);

            var result = queryBuilder.Count();

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
            Assert.Throws<ArgumentNullException>(() => queryBuilder.Count());
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface2()
        {
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);
            var result = queryBuilder.Count();
            var where = result.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface3()
        {
            IQueryBuilderWithWhere<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);
            IQueryBuilderWithWhere<CountQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> result = queryBuilder.Count();
            IWhere<CountQuery<Test1, IDbConnection>> where = result.Where();
            Assert.NotNull(where);
        }
    }
}