using FluentSQL.Default;
using FluentSQL.Helpers;
using FluentSQL.Models;
using FluentSQL.SearchCriteria;
using FluentSQLTest.Models;
using System;
using System.Collections.Generic;
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
        private readonly ConnectionOptions _connectionOptions;
        private readonly ClassOptions _classOptions;
        private readonly Test1 _test1;

        public InsertQueryTest()
        {
            _connectionOptions = new ConnectionOptions(new FluentSQL.Default.Statements(), LoadFluentOptions.GetDatabaseManagmentMock());
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _equal = new Equal<int>(_tableAttribute, _columnAttribute, 1);
            _test1 = new Test1();
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            InsertQuery<Test1> query = new("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) }, _connectionOptions, _test1);

            Assert.NotNull(query);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.ConnectionOptions);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>("query", null, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) }, _connectionOptions, _test1));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) }, null, _test1));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>(null, new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) }, _connectionOptions, _test1));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>(null, new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) }, _connectionOptions, null));
        }

        [Fact]
        public void Should_execute_the_query()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            InsertQuery<Test3> query = new("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions.Select(x => x.ColumnAttribute),
                new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, classOption.PropertyOptions) },
                _connectionOptions, new Test3(0, null, DateTime.Now, true));
            var result = query.Exec();
            Assert.NotNull(result);
            Assert.Equal(1, result.Ids);
        }

        [Fact]
        public void Should_execute_the_query2()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test6));

            InsertQuery<Test6> query = new("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions.Select(x => x.ColumnAttribute),
                new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, classOption.PropertyOptions) },
                _connectionOptions, new Test6(1, null, DateTime.Now, true));
            var result = query.Exec();
            Assert.NotNull(result);
        }

        [Fact]
        public void Throw_exception_if_DatabaseManagment_not_found()
        {
            InsertQuery<Test1> query = new("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                new ColumnAttribute[] { _columnAttribute },
                new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) },
                new ConnectionOptions(new FluentSQL.Default.Statements()), new Test6(1, null, DateTime.Now, true));
            Assert.Throws<ArgumentNullException>(() => query.Exec());
        }

        [Fact]
        public void Should_execute_the_query3()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            InsertQuery<Test3> query = new("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions.Select(x => x.ColumnAttribute),
                new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, classOption.PropertyOptions) },
                _connectionOptions, new Test3(0, null, DateTime.Now, true));
            var result = query.Exec(LoadFluentOptions.GetDbConnection());
            Assert.NotNull(result);
            Assert.Equal(1, result.Ids);
        }

        [Fact]
        public void Should_execute_the_query4()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test6));

            InsertQuery<Test6> query = new("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions.Select(x => x.ColumnAttribute),
                new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, classOption.PropertyOptions) },
                _connectionOptions, new Test6(1, null, DateTime.Now, true));
            var result = query.Exec(LoadFluentOptions.GetDbConnection());
            Assert.NotNull(result);
        }

        [Fact]
        public void Throw_exception_if_DatabaseManagment_not_found1()
        {
            InsertQuery<Test1> query = new("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                new ColumnAttribute[] { _columnAttribute },
                new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) },
                new ConnectionOptions(new FluentSQL.Default.Statements()), new Test6(1, null, DateTime.Now, true));
            Assert.Throws<ArgumentNullException>(() => query.Exec(LoadFluentOptions.GetDbConnection()));
        }
    }
}
