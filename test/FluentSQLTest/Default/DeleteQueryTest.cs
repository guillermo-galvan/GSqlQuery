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
    public class DeleteQueryTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly Equal<int> _equal;
        private readonly IStatements _statements;
        private readonly ClassOptions _classOptions;
        private readonly ConnectionOptions<DbConnection> _connectionOptions;

        public DeleteQueryTest()
        {
            _statements = new FluentSQL.Default.Statements();
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _equal = new Equal<int>(_tableAttribute, _columnAttribute, 1);
            _connectionOptions = new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            DeleteQuery<Test1> query = new("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _statements);

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
            Assert.Throws<ArgumentNullException>(() => new DeleteQuery<Test1>("query", null, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _statements));
            Assert.Throws<ArgumentNullException>(() => new DeleteQuery<Test1>("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, null));
            Assert.Throws<ArgumentNullException>(() => new DeleteQuery<Test1>(null, new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _statements));
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            DeleteQuery<Test1,DbConnection> query = new("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions);

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
            Assert.Throws<ArgumentNullException>(() => new DeleteQuery<Test1,DbConnection>("query", null, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions));
            Assert.Throws<ArgumentNullException>(() => new DeleteQuery<Test1,DbConnection>("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, null));
            Assert.Throws<ArgumentNullException>(() => new DeleteQuery<Test1, DbConnection>(null, new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions));
        }

        [Fact]
        public void Should_execute_the_query()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            DeleteQuery<Test3,DbConnection> query = new("DELETE FROM [TableName];",
                new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptions);
            var result = query.Execute();
            Assert.Equal(1, result);
        }

        [Fact]
        public void Throw_exception_if_DatabaseManagment_not_found()
        {
            DeleteQuery<Test1,DbConnection> query = new("DELETE FROM [TableName];",
                new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) },
                _connectionOptions);            
            Assert.Throws<ArgumentNullException>(() => query.Execute(null));
        }

        [Fact]
        public void Should_execute_the_query2()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            DeleteQuery<Test3,DbConnection> query = new("DELETE FROM [TableName];",
                new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptions);
            var result = query.Execute(LoadFluentOptions.GetDbConnection());
            Assert.Equal(1, result);
        }
    }
}
