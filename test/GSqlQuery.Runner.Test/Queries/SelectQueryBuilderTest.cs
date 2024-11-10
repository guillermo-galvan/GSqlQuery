using GSqlQuery.Extensions;
using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
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
        public void Properties_cannot_be_null2()
        {
            var columsn = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            SelectQueryBuilder<Test1, IDbConnection> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(columsn, _connectionOptions);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.QueryOptions);
            Assert.NotNull(queryBuilder.QueryOptions.Formats);
            Assert.NotNull(queryBuilder.QueryOptions.DatabaseManagement);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters2()
        {
            ClassOptionsTupla<IEnumerable<MemberInfo>> members = null;
            var columsn = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            Assert.Throws<ArgumentNullException>(() => new SelectQueryBuilder<Test1, IDbConnection>(members, _connectionOptions));
            Assert.Throws<ArgumentNullException>(() => new SelectQueryBuilder<Test1, IDbConnection>(columsn, null));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface2()
        {
            var columsn = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            SelectQueryBuilder<Test1, IDbConnection> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(columsn, _connectionOptions);
            var where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_delete_query2()
        {
            var columsn = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            SelectQueryBuilder<Test1, IDbConnection> queryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(columsn, _connectionOptions);
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