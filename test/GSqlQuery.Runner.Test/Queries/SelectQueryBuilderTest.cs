using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class SelectQueryBuilderTest
    {
        private readonly IStatements _stantements;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;

        public SelectQueryBuilderTest()
        {
            _stantements = new Statements();
            _connectionOptions = new ConnectionOptions<IDbConnection>(_stantements, LoadGSqlQueryOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            SelectQueryBuilder<Test1, IDbConnection> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.Options);
            Assert.NotNull(queryBuilder.Options.Statements);
            Assert.NotNull(queryBuilder.Options.DatabaseManagement);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters2()
        {
            Assert.Throws<ArgumentNullException>(() => new SelectQueryBuilder<Test1, IDbConnection>(null, _connectionOptions));
            Assert.Throws<ArgumentNullException>(() => new SelectQueryBuilder<Test1, IDbConnection>(
                new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, null));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface2()
        {
            SelectQueryBuilder<Test1, IDbConnection> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);
            var where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_delete_query2()
        {
            SelectQueryBuilder<Test1, IDbConnection> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);
            SelectQuery<Test1, IDbConnection> query = queryBuilder.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.Statements);
            Assert.Null(query.Criteria);
        }
    }
}