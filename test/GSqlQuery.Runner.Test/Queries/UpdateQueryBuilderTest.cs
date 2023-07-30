using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class UpdateQueryBuilderTest
    {
        private readonly List<string> _columnsValue;
        private readonly IStatements _statements;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;

        public UpdateQueryBuilderTest()
        {
            _columnsValue = new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) };
            _statements = new Statements();
            _connectionOptions = new ConnectionOptions<IDbConnection>(_statements, LoadGSqlQueryOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            UpdateQueryBuilder<Test1, IDbConnection> queryBuilder = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, _columnsValue, string.Empty);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.Options);
            Assert.NotNull(queryBuilder.Options.Statements);
            Assert.NotNull(queryBuilder.Options.DatabaseManagement);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface2()
        {
            UpdateQueryBuilder<Test1, IDbConnection> queryBuilder = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, _columnsValue, string.Empty);
            var where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_update_query2()
        {
            UpdateQueryBuilder<Test3, IDbConnection> queryBuilder = new UpdateQueryBuilder<Test3, IDbConnection>(_connectionOptions, new List<string> { nameof(Test3.Ids), nameof(Test3.Names) },
                string.Empty);
            var query = queryBuilder.Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }

        [Fact]
        public void Should_add_a_new_column_value_with_set_value2()
        {
            UpdateQueryBuilder<Test1, IDbConnection> queryBuilder = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, new List<string> { nameof(Test1.Name) }, string.Empty);

            queryBuilder.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now);
        }

        [Fact]
        public void Should_add_a_new_column_value_with_property2()
        {
            Test1 model = new Test1(1, null, DateTime.Now, true);
            UpdateQueryBuilder<Test1, IDbConnection> test = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, model, new List<string> { nameof(Test1.Name) });

            test.Set(x => x.Id).Set(x => x.Create);
        }

        [Fact]
        public void Should_generate_the_query3()
        {
            Test1 model = new Test1(1, null, DateTime.Now, true);
            UpdateQueryBuilder<Test1, IDbConnection> test = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, model, new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) });
            var query = test.Set(x => x.Id).Set(x => x.Create).Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }


        [Fact]
        public void Should_generate_the_query4()
        {
            UpdateQueryBuilder<Test1, IDbConnection> test = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, new List<string> { nameof(Test1.Name) }, string.Empty);
            var query = test.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now).Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }

        [Fact]
        public void Should_get_the_where_query3()
        {
            Test1 model = new Test1(1, null, DateTime.Now, true);
            UpdateQueryBuilder<Test1, IDbConnection> test = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, model, new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) });
            var where = test.Set(x => x.Id).Set(x => x.Create).Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_get_the_where_query4()
        {
            UpdateQueryBuilder<Test1, IDbConnection> test = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, new List<string> { nameof(Test1.Name) }, string.Empty);
            var where = test.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now).Where();
            Assert.NotNull(where);
        }

    }
}