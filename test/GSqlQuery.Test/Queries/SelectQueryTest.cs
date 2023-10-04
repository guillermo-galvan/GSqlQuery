using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Models;
using System;
using System.Linq;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class SelectQueryTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly Equal<int> _equal;
        private readonly IFormats _formats;
        private readonly ClassOptions _classOptions;

        public SelectQueryTest()
        {
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.First(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _equal = new Equal<int>(_tableAttribute, _columnAttribute, 1);
            _formats = new DefaultFormats();
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            SelectQuery<Test1> query = new SelectQuery<Test1>("query", _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_formats, _classOptions.PropertyOptions) }, _formats);

            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.Formats);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new SelectQuery<Test1>("query", null, new CriteriaDetail[] { _equal.GetCriteria(_formats, _classOptions.PropertyOptions) }, _formats));
            Assert.Throws<ArgumentNullException>(() => new SelectQuery<Test1>("query", _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_formats, _classOptions.PropertyOptions) }, null));
            Assert.Throws<ArgumentNullException>(() => new SelectQuery<Test1>(null, _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_formats, _classOptions.PropertyOptions) }, _formats));
        }
    }
}