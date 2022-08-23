using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;
using FluentSQL.SearchCriteria;
using FluentSQLTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace FluentSQLTest.Extensions
{
    public class QueryExtensionTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly Equal<int> _equal;
        private readonly ConnectionOptions _connectionOptions;
        private readonly ClassOptions _classOptions;

        public QueryExtensionTest()
        {
            _connectionOptions = new ConnectionOptions(new FluentSQL.Default.Statements(), LoadFluentOptions.GetDatabaseManagmentMock());
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _equal = new Equal<int>(_tableAttribute, _columnAttribute, 1);
        }

        [Fact]
        public void Should_get_the_list_of_Test1_in_the_select_query()
        {
            SelectQuery<Test1> query = new("SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test1];", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) }, _connectionOptions);
            var result = query.Exec();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Throw_exception_if_DatabaseManagment_not_found_in_the_select_query()
        {
            SelectQuery<Test1> query = new("SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test1];", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) }, new ConnectionOptions(new FluentSQL.Default.Statements()));
            Assert.Throws<ArgumentNullException>(() => query.Exec());
        }

        [Fact]
        public void Should_get_the_list_of_Test3_in_the_insert_query()
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
        public void Should_get_the_list_of_Test6_in_the_insert_query()
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
        public void Throw_exception_if_DatabaseManagment_not_found_in_the_insert_query()
        {
            InsertQuery<Test1> query = new("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])", 
                new ColumnAttribute[] { _columnAttribute }, 
                new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) }, 
                new ConnectionOptions(new FluentSQL.Default.Statements()), new Test6(1, null, DateTime.Now, true));
            Assert.Throws<ArgumentNullException>(() => query.Exec());
        }

        [Fact]
        public void Should_get_the_list_of_Test3_in_the_update_query()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            UpdateQuery<Test3> query = new("UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param;", 
                new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, classOption.PropertyOptions) }, 
                _connectionOptions);
            var result = query.Exec();
            Assert.Equal(1,result);
        }

        [Fact]
        public void Throw_exception_if_DatabaseManagment_not_found_in_the_update_query()
        {
            UpdateQuery<Test1> query = new("UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param;", 
                new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) }, 
                new ConnectionOptions(new FluentSQL.Default.Statements()));
            Assert.Throws<ArgumentNullException>(() => query.Exec());
        }

        [Fact]
        public void Should_get_the_list_of_Test3_in_the_delete_query()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            DeleteQuery<Test3> query = new("DELETE FROM [TableName];",
                new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, classOption.PropertyOptions) },
                _connectionOptions);
            var result = query.Exec();
            Assert.Equal(1, result);
        }

        [Fact]
        public void Throw_exception_if_DatabaseManagment_not_found_in_the_delete_query()
        {
            DeleteQuery<Test1> query = new("DELETE FROM [TableName];",
                new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) },
                new ConnectionOptions(new FluentSQL.Default.Statements()));
            Assert.Throws<ArgumentNullException>(() => query.Exec());
        }
    }
}
