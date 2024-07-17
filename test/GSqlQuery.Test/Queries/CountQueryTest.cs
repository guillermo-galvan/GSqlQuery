using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Models;
using System;
using System.Linq;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class CountQueryTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly Equal<int> _equal;
        private readonly QueryOptions _queryOptions;
        private readonly ClassOptions _classOptions;
        private readonly ClassOptionsTupla<ColumnAttribute> _classOptionsTupla;

        public CountQueryTest()
        {
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions[nameof(Test1.Id)].ColumnAttribute;
            _classOptionsTupla = new ClassOptionsTupla<ColumnAttribute>(_classOptions, _columnAttribute);
            _equal = new Equal<int>(_classOptionsTupla, new DefaultFormats(), 1);
            _queryOptions = new QueryOptions(new DefaultFormats());
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            CountQuery<Test1> query = new CountQuery<Test1>("query", _classOptions.PropertyOptions, [_equal.GetCriteria()], _queryOptions);

            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.QueryOptions);
            Assert.NotNull(query.QueryOptions.Formats);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new CountQuery<Test1>("query", null, [_equal.GetCriteria()], _queryOptions));
            Assert.Throws<ArgumentNullException>(() => new CountQuery<Test1>("query", _classOptions.PropertyOptions, [_equal.GetCriteria()], null));
            Assert.Throws<ArgumentNullException>(() => new CountQuery<Test1>(null, _classOptions.PropertyOptions, [_equal.GetCriteria()], _queryOptions));
        }
    }
}