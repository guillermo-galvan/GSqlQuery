using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using System.Data.Common;

namespace GSqlQuery.Runner.Test.Queries
{
    public class UpdateQueryBuilderTest
    {
        private readonly List<string> _columnsValue;
        private readonly IStatements _statements;
        private readonly ConnectionOptions<DbConnection> _connectionOptions;

        public UpdateQueryBuilderTest()
        {
            _columnsValue = new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) };
            _statements = new Statements();
            _connectionOptions = new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            UpdateQueryBuilder<Test1, DbConnection> queryBuilder = new(_connectionOptions, _columnsValue, string.Empty);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.ConnectionOptions);
            Assert.NotNull(queryBuilder.ConnectionOptions.Statements);
            Assert.NotNull(queryBuilder.ConnectionOptions.DatabaseManagment);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
            Assert.NotNull(queryBuilder.ColumnValues);
            Assert.NotEmpty(queryBuilder.ColumnValues);
            Assert.Equal(3, queryBuilder.ColumnValues.Count);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters2()
        {
            object? entity = null;
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1, DbConnection>(null, _columnsValue, string.Empty));
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1, DbConnection>(_connectionOptions, null, string.Empty));
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1, DbConnection>(_connectionOptions, entity, _columnsValue));
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1, DbConnection>(null, entity, _columnsValue));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface2()
        {
            UpdateQueryBuilder<Test1, DbConnection> queryBuilder = new(_connectionOptions, _columnsValue, string.Empty);
            var where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_update_query2()
        {
            UpdateQueryBuilder<Test3, DbConnection> queryBuilder = new(_connectionOptions, new List<string> { nameof(Test3.Ids), nameof(Test3.Names) },
                string.Empty);
            var query = queryBuilder.Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.DatabaseManagment);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }

        [Fact]
        public void Should_add_a_new_column_value_with_set_value2()
        {
            UpdateQueryBuilder<Test1, DbConnection> queryBuilder = new(_connectionOptions, new List<string> { nameof(Test1.Name) }, string.Empty);

            queryBuilder.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now);

            Assert.NotNull(queryBuilder.ColumnValues);
            Assert.NotEmpty(queryBuilder.ColumnValues);
            Assert.Equal(3, queryBuilder.ColumnValues.Count);
        }

        [Fact]
        public void Should_add_a_new_column_value_with_property2()
        {
            Test1 model = new(1, null, DateTime.Now, true);
            UpdateQueryBuilder<Test1, DbConnection> test = new(_connectionOptions, model, new List<string> { nameof(Test1.Name) });

            test.Set(x => x.Id).Set(x => x.Create);

            Assert.NotNull(test.ColumnValues);
            Assert.NotEmpty(test.ColumnValues);
            Assert.Equal(3, test.ColumnValues.Count);
        }

        [Fact]
        public void Should_generate_the_query3()
        {
            Test1 model = new(1, null, DateTime.Now, true);
            UpdateQueryBuilder<Test1, DbConnection> test = new(_connectionOptions, model, new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) });
            var query = test.Set(x => x.Id).Set(x => x.Create).Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.DatabaseManagment);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }


        [Fact]
        public void Should_generate_the_query4()
        {
            UpdateQueryBuilder<Test1, DbConnection> test = new(_connectionOptions, new List<string> { nameof(Test1.Name) }, string.Empty);
            var query = test.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now).Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.DatabaseManagment);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }

        [Fact]
        public void Should_get_the_where_query3()
        {
            Test1 model = new(1, null, DateTime.Now, true);
            UpdateQueryBuilder<Test1, DbConnection> test = new(_connectionOptions, model, new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) });
            var where = test.Set(x => x.Id).Set(x => x.Create).Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_get_the_where_query4()
        {
            UpdateQueryBuilder<Test1, DbConnection> test = new(_connectionOptions, new List<string> { nameof(Test1.Name) }, string.Empty);
            var where = test.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now).Where();
            Assert.NotNull(where);
        }

    }
}
