using GSqlQuery.Extensions;
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
        private readonly Equal<int> _equal;
        private readonly QueryOptions _queryOptions;
        private readonly ClassOptions _classOptions;

        public InsertQueryTest()
        {
            _queryOptions = new QueryOptions(new DefaultFormats());
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            var classOptionsTupla = new ClassOptionsTupla<ColumnAttribute>(_classOptions, _columnAttribute);
            _equal = new Equal<int>(classOptionsTupla, new DefaultFormats(), 1);
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            InsertQuery<Test1> query = new InsertQuery<Test1>("query", _classOptions.PropertyOptions, [_equal.GetCriteria(_queryOptions.Formats, _classOptions.PropertyOptions)], _queryOptions);

            Assert.NotNull(query);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.QueryOptions);
            Assert.NotNull(query.QueryOptions.Formats);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>("query", null, [_equal.GetCriteria(_queryOptions.Formats, _classOptions.PropertyOptions)], _queryOptions));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>("query", _classOptions.PropertyOptions, [_equal.GetCriteria(_queryOptions.Formats, _classOptions.PropertyOptions)], null));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>(null, _classOptions.PropertyOptions, [_equal.GetCriteria(_queryOptions.Formats, _classOptions.PropertyOptions)], _queryOptions));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>(null, _classOptions.PropertyOptions, [_equal.GetCriteria(_queryOptions.Formats, _classOptions.PropertyOptions)], _queryOptions));
        }
    }
}