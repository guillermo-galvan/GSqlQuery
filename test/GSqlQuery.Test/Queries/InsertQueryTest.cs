using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Models;
using System;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class InsertQueryTest
    {
        private readonly PropertyOptions _columnAttribute;
        private readonly Equal<int> _equal;
        private readonly QueryOptions _queryOptions;
        private readonly ClassOptions _classOptions;
        private uint _parameterId = 0;

        public InsertQueryTest()
        {
            _queryOptions = new QueryOptions(new DefaultFormats());
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions[nameof(Test1.Id)];
            var classOptionsTupla = new ClassOptionsTupla<PropertyOptions>(_classOptions, _columnAttribute);
            _equal = new Equal<int>(classOptionsTupla, new DefaultFormats(), 1);
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            InsertQuery<Test1> query = new InsertQuery<Test1>("query", _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _queryOptions);

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
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>("query", null, [_equal.GetCriteria(ref _parameterId)], _queryOptions));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>("query", _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], null));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>(null, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _queryOptions));
            Assert.Throws<ArgumentNullException>(() => new InsertQuery<Test1>(null, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _queryOptions));
        }
    }
}