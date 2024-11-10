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
    public class UpdateQueryTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly Data.SearchCriteria _equal;
        private readonly IFormats _formats;
        private readonly ClassOptions _classOptions;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;
        private readonly ConnectionOptions<IDbConnection> _connectionOptionsAsync;

        public UpdateQueryTest()
        {
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _equal = new Data.SearchCriteria(_formats, _tableAttribute, _columnAttribute);
            _formats = new TestFormats();
            _connectionOptions = new ConnectionOptions<IDbConnection>(_formats, LoadGSqlQueryOptions.GetDatabaseManagmentMock());
            _connectionOptionsAsync = new ConnectionOptions<IDbConnection>(_formats, LoadGSqlQueryOptions.GetDatabaseManagmentMockAsync());
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            UpdateQuery<Test1, IDbConnection> query = new UpdateQuery<Test1, IDbConnection>("query", _classOptions.PropertyOptions,
                new CriteriaDetail[] { _equal.GetCriteria(_formats, _classOptions.PropertyOptions) }, _connectionOptions);

            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.QueryOptions);
            Assert.NotNull(query.QueryOptions.Formats);
            Assert.NotNull(query.QueryOptions.DatabaseManagement);
        }

        [Fact]
        public void Should_execute_the_query()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            UpdateQuery<Test3, IDbConnection> query = new UpdateQuery<Test3, IDbConnection>("UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param;",
                _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_formats, classOption.PropertyOptions) },
                _connectionOptions);
            var result = query.Execute();
            Assert.Equal(1, result);
        }

        [Fact]
        public void Throw_exception_if_connection_not_found()
        {
            UpdateQuery<Test1, IDbConnection> query = new UpdateQuery<Test1, IDbConnection>("UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param;",
                _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_formats, _classOptions.PropertyOptions) },
                _connectionOptions);
            Assert.Throws<ArgumentNullException>(() => query.Execute(null));
        }

        [Fact]
        public void Should_execute_the_query1()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            UpdateQuery<Test3, IDbConnection> query = new UpdateQuery<Test3, IDbConnection>("UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param;",
                _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_formats, classOption.PropertyOptions) },
                _connectionOptions);
            var result = query.Execute(LoadGSqlQueryOptions.GetIDbConnection());
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Should_executeAsync_the_query()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            UpdateQuery<Test3, IDbConnection> query = new UpdateQuery<Test3, IDbConnection>("UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param;",
                _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_formats, classOption.PropertyOptions) },
                _connectionOptionsAsync);
            var result = await query.ExecuteAsync(CancellationToken.None);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Throw_exception_if_connection_not_found_Async()
        {
            UpdateQuery<Test1, IDbConnection> query = new UpdateQuery<Test1, IDbConnection>("UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param;",
                _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_formats, _classOptions.PropertyOptions) },
                _connectionOptionsAsync);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await query.ExecuteAsync(null, CancellationToken.None));
        }

        [Fact]
        public async Task Should_executeAsync_the_query1()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            UpdateQuery<Test3, IDbConnection> query = new UpdateQuery<Test3, IDbConnection>("UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param;",
                _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_formats, classOption.PropertyOptions) },
                _connectionOptionsAsync);
            var result = await query.ExecuteAsync(LoadGSqlQueryOptions.GetIDbConnection(), CancellationToken.None);
            Assert.Equal(1, result);
        }
    }
}