using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Models;
using System;
using System.Linq;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class InsertQueryTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly Equal<int> _equal;
        private readonly IFormats _statements;
        private readonly ClassOptions _classOptions;
        private readonly Test1 _test1;

        public InsertQueryTest()
        {
            _statements = new DefaultFormats();
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _equal = new Equal<int>(_tableAttribute, _columnAttribute, 1);
            _test1 = new Test1(1, "name", DateTime.Now, true);
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            InsertQuery<Test1> query = new InsertQuery<Test1>("query", _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _statements, _test1);

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
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>("query", _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, null, _test1));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>(null, _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _statements, _test1));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>(null, _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _statements, null));
        }
    }
}