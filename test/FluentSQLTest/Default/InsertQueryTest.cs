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

        public InsertQueryTest()
        {
            _connectionOptions = new ConnectionOptions(new FluentSQL.Default.Statements());
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _equal = new Equal<int>(_tableAttribute, _columnAttribute, 1);
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            InsertQuery<Test1> query = new("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) }, _connectionOptions);

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
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>("query", null, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) }, _connectionOptions));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) }, null));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>(null, new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements, _classOptions.PropertyOptions) }, _connectionOptions));
        }
    }
}
