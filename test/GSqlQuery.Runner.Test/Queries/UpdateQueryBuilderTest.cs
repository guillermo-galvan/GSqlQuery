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
    public class UpdateQueryBuilderTest
    {
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;

        public UpdateQueryBuilderTest()
        {
            _connectionOptions = new ConnectionOptions<IDbConnection>(new TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            ClassOptionsTupla<MemberInfo> columnsValue = ExpressionExtension.GetOptionsAndMember<Test1, string>((x) => x.Name);
            UpdateQueryBuilder<Test1, IDbConnection> queryBuilder = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, columnsValue, string.Empty);

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
            ClassOptionsTupla<MemberInfo> columnsValue = ExpressionExtension.GetOptionsAndMember<Test1, string>((x) => x.Name);
            UpdateQueryBuilder<Test1, IDbConnection> queryBuilder = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, columnsValue, string.Empty);
            var where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_update_query2()
        {
            ClassOptionsTupla<MemberInfo> columnsValue = ExpressionExtension.GetOptionsAndMember<Test1, string>((x) => x.Name);
            UpdateQueryBuilder<Test3, IDbConnection> queryBuilder = new UpdateQueryBuilder<Test3, IDbConnection>(_connectionOptions, columnsValue, string.Empty);
            var query = queryBuilder.Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.QueryOptions);
            Assert.NotNull(query.QueryOptions.Formats);
            Assert.NotNull(query.QueryOptions.DatabaseManagement);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }

        [Fact]
        public void Should_add_a_new_column_value_with_set_value2()
        {
            ClassOptionsTupla<MemberInfo> columnsValue = ExpressionExtension.GetOptionsAndMember<Test1, string>((x) => x.Name);
            UpdateQueryBuilder<Test1, IDbConnection> queryBuilder = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, columnsValue, string.Empty);

            queryBuilder.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now);
        }

        [Fact]
        public void Should_add_a_new_column_value_with_property2()
        {
            var columsn = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            Test1 model = new Test1(1, null, DateTime.Now, true);
            UpdateQueryBuilder<Test1, IDbConnection> test = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, model, columsn);

            test.Set(x => x.Id).Set(x => x.Create);
        }

        [Fact]
        public void Should_generate_the_query3()
        {
            var columsn = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            Test1 model = new Test1(1, null, DateTime.Now, true);
            UpdateQueryBuilder<Test1, IDbConnection> test = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, model, columsn);
            var query = test.Set(x => x.Id).Set(x => x.Create).Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.QueryOptions);
            Assert.NotNull(query.QueryOptions.Formats);
            Assert.NotNull(query.QueryOptions.DatabaseManagement);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }


        [Fact]
        public void Should_generate_the_query4()
        {
            ClassOptionsTupla<MemberInfo> columnsValue = ExpressionExtension.GetOptionsAndMember<Test1, string>((x) => x.Name);
            UpdateQueryBuilder<Test1, IDbConnection> test = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, columnsValue, string.Empty);
            var query = test.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now).Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.QueryOptions);
            Assert.NotNull(query.QueryOptions.Formats);
            Assert.NotNull(query.QueryOptions.DatabaseManagement);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }

        [Fact]
        public void Should_get_the_where_query3()
        {
            var columsn = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            Test1 model = new Test1(1, null, DateTime.Now, true);
            UpdateQueryBuilder<Test1, IDbConnection> test = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, model, columsn);
            var where = test.Set(x => x.Id).Set(x => x.Create).Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_get_the_where_query4()
        {
            ClassOptionsTupla<MemberInfo> columnsValue = ExpressionExtension.GetOptionsAndMember<Test1, string>((x) => x.Name);
            UpdateQueryBuilder<Test1, IDbConnection> test = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, columnsValue, string.Empty);
            var where = test.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now).Where();
            Assert.NotNull(where);
        }

    }
}