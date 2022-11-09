using FluentSQL.Default;
using FluentSQL.Helpers;
using FluentSQL.Models;
using FluentSQL.SearchCriteria;
using FluentSQLTest.Models;
using System.Data.Common;

namespace FluentSQLTest.Default
{
    public class OrderByQueryTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly Equal<int> _equal;
        private readonly IStatements _statements;
        private readonly ClassOptions _classOptions;
        private readonly ConnectionOptions<DbConnection> _connectionOptions;
        private readonly ConnectionOptions<DbConnection> _connectionOptionsAsync;

        public OrderByQueryTest()
        {
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.First(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _equal = new Equal<int>(_tableAttribute, _columnAttribute, 1);
            _statements = new FluentSQL.Default.Statements();
            _connectionOptions = new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock());
            _connectionOptionsAsync = new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMockAsync());
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            OrderByQuery<Test1> query = new("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _statements);

            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.Statements);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new OrderByQuery<Test1>("query", null, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _statements));
            Assert.Throws<ArgumentNullException>(() => new OrderByQuery<Test1>("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, null));
            Assert.Throws<ArgumentNullException>(() => new OrderByQuery<Test1>(null, new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _statements));
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            OrderByQuery<Test1, DbConnection> query = new("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions);

            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.DatabaseManagment);
            Assert.NotNull(query.Statements);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters2()
        {
            Assert.Throws<ArgumentNullException>(() => new OrderByQuery<Test1, DbConnection>("query", null, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions));
            Assert.Throws<ArgumentNullException>(() => new OrderByQuery<Test1, DbConnection>("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, null));
            Assert.Throws<ArgumentNullException>(() => new OrderByQuery<Test1, DbConnection>(null, new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions));
        }

        [Fact]
        public void Should_execute_the_query()
        {
            OrderByQuery<Test1, DbConnection> query = new("SELECT Test1.Id FROM Test1 ORDER BY Test1.Id ASC,Test1.Name,Test1.Create DESC;", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions);
            var result = query.Execute();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }


        [Fact]
        public void Throw_exception_if_DatabaseManagment_not_found()
        {
            OrderByQuery<Test1, DbConnection> query = new("SELECT Test1.Id FROM Test1 ORDER BY Test1.Id ASC,Test1.Name,Test1.Create DESC;", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions);
            Assert.Throws<ArgumentNullException>(() => query.Execute(null));
        }

        [Fact]
        public void Should_execute_the_query1()
        {
            OrderByQuery<Test1, DbConnection> query = new("SELECT Test1.Id FROM Test1 ORDER BY Test1.Id ASC,Test1.Name,Test1.Create DESC;", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions);
            var result = query.Execute(LoadFluentOptions.GetDbConnection());
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task Should_executeAsync_the_query()
        {
            OrderByQuery<Test1, DbConnection> query = new("SELECT Test1.Id FROM Test1 ORDER BY Test1.Id ASC,Test1.Name,Test1.Create DESC;", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptionsAsync);
            var result = await query.ExecuteAsync(CancellationToken.None);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }


        [Fact]
        public async Task Throw_exception_if_DatabaseManagment_not_found_Async()
        {
            OrderByQuery<Test1, DbConnection> query = new("SELECT Test1.Id FROM Test1 ORDER BY Test1.Id ASC,Test1.Name,Test1.Create DESC;", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptionsAsync);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await query.ExecuteAsync(null, CancellationToken.None));
        }

        [Fact]
        public async Task Should_executeAsync_the_query1()
        {
            OrderByQuery<Test1, DbConnection> query = new("SELECT Test1.Id FROM Test1 ORDER BY Test1.Id ASC,Test1.Name,Test1.Create DESC;", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptionsAsync);
            var result = await query.ExecuteAsync(LoadFluentOptions.GetDbConnection(), CancellationToken.None);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
