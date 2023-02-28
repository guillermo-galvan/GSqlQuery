﻿using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using System;
using System.Data.Common;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class DeleteQueryBuilderTest
    {
        private readonly IStatements _statements;
        private readonly ConnectionOptions<DbConnection> _connectionOptions;
        public DeleteQueryBuilderTest()
        {
            _statements = new Statements();
            _connectionOptions = new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            DeleteQueryBuilder<Test1, DbConnection> queryBuilder = new DeleteQueryBuilder<Test1, DbConnection>(_connectionOptions);

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
            Assert.Throws<ArgumentNullException>(() => new DeleteQueryBuilder<Test1, DbConnection>(null));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface2()
        {
            DeleteQueryBuilder<Test1, DbConnection> queryBuilder = new DeleteQueryBuilder<Test1, DbConnection>(_connectionOptions);
            var where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_delete_query2()
        {
            DeleteQueryBuilder<Test1, DbConnection> queryBuilder = new DeleteQueryBuilder<Test1, DbConnection>(_connectionOptions);
            var query = queryBuilder.Build();
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
