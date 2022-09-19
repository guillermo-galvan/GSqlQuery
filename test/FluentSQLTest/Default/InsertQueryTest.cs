using FluentSQL.Default;
using FluentSQL.Helpers;
using FluentSQL.Models;
using FluentSQL.SearchCriteria;
using FluentSQLTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest.Default
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

        public InsertQueryTest()
        {
            _statements = new FluentSQL.Default.Statements();
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _equal = new Equal<int>(_tableAttribute, _columnAttribute, 1);
            _test1 = new Test1();
            _connectionOptions = new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            InsertQuery<Test1> query = new("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _statements, _test1);

            Assert.NotNull(query);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>("query", null, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _statements, _test1));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, null, _test1));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>(null, new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _statements, _test1));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>(null, new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _statements, null));
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            InsertQuery<Test1,DbConnection> query = new("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions, _test1);

            Assert.NotNull(query);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.ConnectionOptions);
            Assert.NotNull(query.ConnectionOptions.Statements);
            Assert.NotNull(query.ConnectionOptions.DatabaseManagment);
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

            InsertQuery<Test3,DbConnection> query = new("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions.Select(x => x.ColumnAttribute),
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptions, new Test3(0, null, DateTime.Now, true));
            var result = query.Exec();
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
            var result = query.Exec();
            Assert.NotNull(result);
        }

        [Fact]
        public void Throw_exception_if_DatabaseManagment_not_found()
        {
            InsertQuery<Test1,DbConnection> query = new("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                new ColumnAttribute[] { _columnAttribute },
                new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) },
               _connectionOptions, new Test6(1, null, DateTime.Now, true));
            Assert.Throws<ArgumentNullException>(() => query.Exec(null));
        }

        [Fact]
        public void Should_execute_the_query3()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            InsertQuery<Test3,DbConnection> query = new("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions.Select(x => x.ColumnAttribute),
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptions, new Test3(0, null, DateTime.Now, true));
            var result = query.Exec(LoadFluentOptions.GetDbConnection());
            Assert.NotNull(result);
            Assert.Equal(1, result.Ids);
        }

        [Fact]
        public void Should_execute_the_query4()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test6));

            InsertQuery<Test6,DbConnection> query = new("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions.Select(x => x.ColumnAttribute),
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptions, new Test6(1, null, DateTime.Now, true));
            var result = query.Exec(LoadFluentOptions.GetDbConnection());
            Assert.NotNull(result);
        }
    }
}
