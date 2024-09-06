using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Models;
using System;
using System.Linq.Expressions;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class DeleteQueryTest
    {
        private readonly PropertyOptions _propertyOptions;
        private readonly Equal<Test1, int> _equal;
        private readonly QueryOptions _queryOptions;
        private readonly ClassOptions _classOptions;
        private readonly ClassOptionsTupla<PropertyOptions> _classOptionsTupla;
        private uint _parameterId = 0;
        public DeleteQueryTest()
        {
            _queryOptions = new QueryOptions(new DefaultFormats());
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _propertyOptions = _classOptions.PropertyOptions[nameof(Test1.Id)];
            _classOptionsTupla = new ClassOptionsTupla<PropertyOptions>(_classOptions, _propertyOptions);
            Expression<Func<Test1, int>> expression = (x) => x.Id;
            _equal = new Equal<Test1, int>(_classOptionsTupla.ClassOptions, new DefaultFormats(), 1, null, ref expression );
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            DeleteQuery<Test1> query = new DeleteQuery<Test1>("query", _classOptions.FormatTableName.Table, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _queryOptions);

            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.QueryOptions);
            Assert.NotNull(query.QueryOptions.Formats);
            Assert.NotNull(query.Table);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new DeleteQuery<Test1>("query", _classOptions.FormatTableName.Table, null, [_equal.GetCriteria(ref _parameterId)], _queryOptions));
            Assert.Throws<ArgumentNullException>(() => new DeleteQuery<Test1>("query", null, null, [_equal.GetCriteria(ref _parameterId)], _queryOptions));
            Assert.Throws<ArgumentNullException>(() => new DeleteQuery<Test1>("query", _classOptions.FormatTableName.Table, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], null));
            Assert.Throws<ArgumentNullException>(() => new DeleteQuery<Test1>(null, _classOptions.FormatTableName.Table, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _queryOptions));
        }
    }
}