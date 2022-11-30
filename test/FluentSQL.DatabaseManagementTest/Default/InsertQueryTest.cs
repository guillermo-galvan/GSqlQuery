using FluentSQL.DatabaseManagement.Default;
using FluentSQL.DatabaseManagement.Models;
using FluentSQL.DatabaseManagementTest.Models;
using FluentSQL.Helpers;
using FluentSQL.Models;
using FluentSQL.SearchCriteria;
using System.Data.Common;

namespace FluentSQL.DatabaseManagementTest.Default
{
    public class InsertQueryTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly Equal<int> _equal;
        private readonly IStatements _statements;
        private readonly ClassOptions _classOptions;
        private readonly Test1 _test1;
        private readonly ConnectionOptions<DbConnection> _connectionOptions;
        private readonly ConnectionOptions<DbConnection> _connectionOptionsAsync;

        public InsertQueryTest()
        {
            _statements = new FluentSQL.Default.Statements();
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _equal = new Equal<int>(_tableAttribute, _columnAttribute, 1);
            _test1 = new Test1();
            _connectionOptions = new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock());
            _connectionOptionsAsync = new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMockAsync());
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            InsertQuery<Test1, DbConnection> query = new("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions, _test1);

            Assert.NotNull(query);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.DatabaseManagment);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters2()
        {
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1, DbConnection>("query", null, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions, _test1));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1, DbConnection>("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, null, _test1));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1, DbConnection>(null, new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions, _test1));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1, DbConnection>(null, new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions, null));
        }

        [Fact]
        public void Should_execute_the_query()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            InsertQuery<Test3, DbConnection> query = new("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions.Select(x => x.ColumnAttribute),
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptions, new Test3(0, null, DateTime.Now, true));
            var result = query.Execute();
            Assert.NotNull(result);
            Assert.Equal(1, result.Ids);
        }

        [Fact]
        public void Should_execute_the_query2()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test6));

            InsertQuery<Test6, DbConnection> query = new("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions.Select(x => x.ColumnAttribute),
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptions, new Test6(1, null, DateTime.Now, true));
            var result = query.Execute();
            Assert.NotNull(result);
        }

        [Fact]
        public void Throw_exception_if_DatabaseManagment_not_found()
        {
            InsertQuery<Test1, DbConnection> query = new("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                new ColumnAttribute[] { _columnAttribute },
                new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) },
               _connectionOptions, new Test6(1, null, DateTime.Now, true));
            Assert.Throws<ArgumentNullException>(() => query.Execute(null));
        }

        [Fact]
        public void Should_execute_the_query3()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            InsertQuery<Test3, DbConnection> query = new("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions.Select(x => x.ColumnAttribute),
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptions, new Test3(0, null, DateTime.Now, true));
            var result = query.Execute(LoadFluentOptions.GetDbConnection());
            Assert.NotNull(result);
            Assert.Equal(1, result.Ids);
        }

        [Fact]
        public void Should_execute_the_query4()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test6));

            InsertQuery<Test6, DbConnection> query = new("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions.Select(x => x.ColumnAttribute),
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptions, new Test6(1, null, DateTime.Now, true));
            var result = query.Execute(LoadFluentOptions.GetDbConnection());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_executeAsync_the_query()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            InsertQuery<Test3, DbConnection> query = new("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions.Select(x => x.ColumnAttribute),
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptionsAsync, new Test3(0, null, DateTime.Now, true));
            var result = await query.ExecuteAsync(CancellationToken.None);
            Assert.NotNull(result);
            Assert.Equal(1, result.Ids);
        }

        [Fact]
        public async Task Should_executeAsync_the_query2()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test6));

            InsertQuery<Test6, DbConnection> query = new("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions.Select(x => x.ColumnAttribute),
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptionsAsync, new Test6(1, null, DateTime.Now, true));
            var result = await query.ExecuteAsync(CancellationToken.None);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Throw_exception_if_DatabaseManagment_not_found_Async()
        {
            InsertQuery<Test1, DbConnection> query = new("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                new ColumnAttribute[] { _columnAttribute },
                new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) },
               _connectionOptionsAsync, new Test6(1, null, DateTime.Now, true));
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await query.ExecuteAsync(null, CancellationToken.None));
        }

        [Fact]
        public async Task Should_executeAsync_the_query3()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            InsertQuery<Test3, DbConnection> query = new("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions.Select(x => x.ColumnAttribute),
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptionsAsync, new Test3(0, null, DateTime.Now, true));
            var result = await query.ExecuteAsync(LoadFluentOptions.GetDbConnection(), CancellationToken.None);
            Assert.NotNull(result);
            Assert.Equal(1, result.Ids);
        }

        [Fact]
        public async Task Should_executeAsync_the_query4()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test6));

            InsertQuery<Test6, DbConnection> query = new("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions.Select(x => x.ColumnAttribute),
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptions, new Test6(1, null, DateTime.Now, true));
            var result = await query.ExecuteAsync(LoadFluentOptions.GetDbConnection(), CancellationToken.None);
            Assert.NotNull(result);
        }
    }
}
