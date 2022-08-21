using FluentSQL.Default;
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
    public class SelectQueryTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly Equal<int> _equal;
        private readonly ConnectionOptions _connectionOptions;

        public SelectQueryTest()
        {
            _columnAttribute = new ColumnAttribute("Id");
            _tableAttribute = new TableAttribute("Test1");
            _equal = new Equal<int>(_tableAttribute, _columnAttribute, 1);
            _connectionOptions = new ConnectionOptions(new FluentSQL.Default.Statements());
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            SelectQuery<Test1> query = new("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements) }, _connectionOptions);

            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.ConnectionOptions);
            Assert.NotNull(query.ConnectionOptions.Statements);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new SelectQuery<Test1>("query", null, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements) }, _connectionOptions));
            Assert.Throws<ArgumentNullException>(() => new SelectQuery<Test1>("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements) }, null));
            Assert.Throws<ArgumentNullException>(() => new SelectQuery<Test1>(null, new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_connectionOptions.Statements) }, _connectionOptions));
        }
    }
}
