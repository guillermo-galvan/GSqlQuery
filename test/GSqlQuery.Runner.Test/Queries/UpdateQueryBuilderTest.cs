using GSqlQuery.Extensions;
using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
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
        public void Properties_cannot_be_null()
        {
            Expression<Func<Test1, string>> expression = (x) => x.Name;
            UpdateQueryBuilder<Test1, IDbConnection> queryBuilder = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, expression, string.Empty);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.QueryOptions);
            Assert.NotNull(queryBuilder.QueryOptions.Formats);
            Assert.NotNull(queryBuilder.QueryOptions.DatabaseManagement);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface()
        {
            Expression<Func<Test1, string>> expression = (x) => x.Name;
            UpdateQueryBuilder<Test1, IDbConnection> queryBuilder = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, expression, string.Empty);
            var where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_update_query()
        {
            Expression<Func<Test1, string>> expression = (x) => x.Name;
            UpdateQueryBuilder<Test3, IDbConnection> queryBuilder = new UpdateQueryBuilder<Test3, IDbConnection>(_connectionOptions, expression, string.Empty);
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
        public void Should_add_a_new_column_value_with_set_value()
        {
            Expression<Func<Test1, string>> expression = (x) => x.Name;
            UpdateQueryBuilder<Test1, IDbConnection> queryBuilder = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, expression, string.Empty);

            queryBuilder.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now);
        }

        [Fact]
        public void Should_add_a_new_column_value_with_property()
        {
            Test1 model = new Test1(1, null, DateTime.Now, true);
            Expression<Func<Test1, object>> expression = (x) => new { x.Id, x.Name, x.Create };
            UpdateQueryBuilder<Test1, IDbConnection> test = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, model, expression);

            test.Set(x => x.Id).Set(x => x.Create);
        }

        [Fact]
        public void Should_generate_the_query3()
        {
            Test1 model = new Test1(1, null, DateTime.Now, true);
            Expression<Func<Test1, object>> expression = (x) => new { x.Id, x.Name, x.Create };
            UpdateQueryBuilder<Test1, IDbConnection> test = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, model, expression);
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
            Expression<Func<Test1, string>> expression = (x) => x.Name;
            UpdateQueryBuilder<Test1, IDbConnection> test = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, expression, string.Empty);
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
            Test1 model = new Test1(1, null, DateTime.Now, true);
            Expression<Func<Test1, object>> expression = (x) => new { x.Id, x.Name, x.Create };
            UpdateQueryBuilder<Test1, IDbConnection> test = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, model, expression);
            var where = test.Set(x => x.Id).Set(x => x.Create).Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_get_the_where_query4()
        {
            Expression<Func<Test1, string>> expression = (x) => x.Name;
            UpdateQueryBuilder<Test1, IDbConnection> test = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, expression, string.Empty);
            var where = test.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now).Where();
            Assert.NotNull(where);
        }

    }
}