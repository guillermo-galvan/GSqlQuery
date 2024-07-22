using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Models;
using System;
using System.Linq;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class UpdateQueryTest
    {
        private readonly PropertyOptions _columnAttribute;
        private readonly Equal<int> _equal;
        private readonly QueryOptions _queryOptions;
        private readonly ClassOptions _classOptions;

        public UpdateQueryTest()
        {
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions[nameof(Test1.Id)];
            var classOptionsTupla = new ClassOptionsTupla<PropertyOptions>(_classOptions, _columnAttribute);
            _equal = new Equal<int>(classOptionsTupla, new DefaultFormats(), 1);
            _queryOptions = new QueryOptions(new DefaultFormats());
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            UpdateQuery<Test1> query = new UpdateQuery<Test1>("query", _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria() }, _queryOptions);

            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.QueryOptions.Formats);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new UpdateQuery<Test1>("query", null, [_equal.GetCriteria()], _queryOptions));
            Assert.Throws<ArgumentNullException>(() => new UpdateQuery<Test1>("query", _classOptions.PropertyOptions, [_equal.GetCriteria()], null));
            Assert.Throws<ArgumentNullException>(() => new UpdateQuery<Test1>(null, _classOptions.PropertyOptions, [_equal.GetCriteria()], _queryOptions));
        }
    }
}