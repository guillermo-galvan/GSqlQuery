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
        public void Should_get_the_list_of_Test1()
        {
            SelectQuery<Test1> query = new("SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test1];", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) }, _connectionOptions);
            var result = query.Exec();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Throw_exception_if_DatabaseManagment_not_found()
        {
            SelectQuery<Test1> query = new("SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test1];", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) }, new ConnectionOptions(new FluentSQL.Default.Statements()));
            Assert.Throws<ArgumentNullException>(() => query.Exec());
        }

    }
}
