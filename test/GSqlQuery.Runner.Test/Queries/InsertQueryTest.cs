using GSqlQuery.Runner.Test.Models;
using GSqlQuery.SearchCriteria;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class InsertQueryTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly Equal<int> _equal;
        private readonly IStatements _statements;
        private readonly ClassOptions _classOptions;
        private readonly Test1 _test1;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;
        private readonly ConnectionOptions<IDbConnection> _connectionOptionsAsync;

        public InsertQueryTest()
        {
            _statements = new Statements();
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _equal = new Equal<int>(_tableAttribute, _columnAttribute, 1);
            _test1 = new Test1();
            _connectionOptions = new ConnectionOptions<IDbConnection>(_statements, LoadGSqlQueryOptions.GetDatabaseManagmentMock());
            _connectionOptionsAsync = new ConnectionOptions<IDbConnection>(_statements, LoadGSqlQueryOptions.GetDatabaseManagmentMockAsync());
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            InsertQuery<Test1, IDbConnection> query = new InsertQuery<Test1, IDbConnection>("query", _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions, _test1, null);

            Assert.NotNull(query);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
        }

        [Fact]
        public void Should_execute_the_query()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            InsertQuery<Test3, IDbConnection> query = new InsertQuery<Test3, IDbConnection>("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions,
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptions, new Test3(0, null, DateTime.Now, true), classOption.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.IsAutoIncrementing));
            var result = query.Execute();
            Assert.NotNull(result);
            Assert.Equal(1, result.Ids);
        }

        [Fact]
        public void Should_execute_the_query2()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test6));

            InsertQuery<Test6, IDbConnection> query = new InsertQuery<Test6, IDbConnection>("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions,
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptions, new Test6(1, null, DateTime.Now, true), classOption.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.IsAutoIncrementing));
            var result = query.Execute();
            Assert.NotNull(result);
        }

        [Fact]
        public void Throw_exception_if_DatabaseManagment_not_found()
        {
            InsertQuery<Test1, IDbConnection> query = new InsertQuery<Test1, IDbConnection>("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                _classOptions.PropertyOptions,
                new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) },
               _connectionOptions, new Test6(1, null, DateTime.Now, true), null);
            Assert.Throws<ArgumentNullException>(() => query.Execute(null));
        }

        [Fact]
        public void Should_execute_the_query3()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            InsertQuery<Test3, IDbConnection> query = new InsertQuery<Test3, IDbConnection>("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions,
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptions, new Test3(0, null, DateTime.Now, true), classOption.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.IsAutoIncrementing));
            var result = query.Execute(LoadGSqlQueryOptions.GetIDbConnection());
            Assert.NotNull(result);
            Assert.Equal(1, result.Ids);
        }

        [Fact]
        public void Should_execute_the_query4()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test6));

            InsertQuery<Test6, IDbConnection> query = new InsertQuery<Test6, IDbConnection>("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions,
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptions, new Test6(1, null, DateTime.Now, true), classOption.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.IsAutoIncrementing));
            var result = query.Execute(LoadGSqlQueryOptions.GetIDbConnection());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_executeAsync_the_query()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            InsertQuery<Test3, IDbConnection> query = new InsertQuery<Test3, IDbConnection>("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions,
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptionsAsync, new Test3(0, null, DateTime.Now, true), classOption.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.IsAutoIncrementing));
            var result = await query.ExecuteAsync(CancellationToken.None);
            Assert.NotNull(result);
            Assert.Equal(1, result.Ids);
        }

        [Fact]
        public async Task Should_executeAsync_the_query2()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test6));

            InsertQuery<Test6, IDbConnection> query = new InsertQuery<Test6, IDbConnection>("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions,
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptionsAsync, new Test6(1, null, DateTime.Now, true), classOption.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.IsAutoIncrementing));
            var result = await query.ExecuteAsync(CancellationToken.None);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Throw_exception_if_DatabaseManagment_not_found_Async()
        {
            InsertQuery<Test1, IDbConnection> query = new InsertQuery<Test1, IDbConnection>("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                _classOptions.PropertyOptions,
                new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) },
               _connectionOptionsAsync, new Test6(1, null, DateTime.Now, true), null);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await query.ExecuteAsync(null, CancellationToken.None));
        }

        [Fact]
        public async Task Should_executeAsync_the_query3()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            InsertQuery<Test3, IDbConnection> query = new InsertQuery<Test3, IDbConnection>("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions,
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptionsAsync, new Test3(0, null, DateTime.Now, true), classOption.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.IsAutoIncrementing));
            var result = await query.ExecuteAsync(LoadGSqlQueryOptions.GetIDbConnection(), CancellationToken.None);
            Assert.NotNull(result);
            Assert.Equal(1, result.Ids);
        }

        [Fact]
        public async Task Should_executeAsync_the_query4()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test6));

            InsertQuery<Test6, IDbConnection> query = new InsertQuery<Test6, IDbConnection>("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions,
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptions, new Test6(1, null, DateTime.Now, true), classOption.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.IsAutoIncrementing));
            var result = await query.ExecuteAsync(LoadGSqlQueryOptions.GetIDbConnection(), CancellationToken.None);
            Assert.NotNull(result);
        }
    }
}