using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Extensions;
using GSqlQuery.Test.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GSqlQuery.Test.SearchCriteria
{
    public class GreaterThanTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly IFormats _formats;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly ClassOptions _classOptions;
        private readonly ClassOptionsTupla<ColumnAttribute> _classOptionsTupla;

        public GreaterThanTest()
        {
            _formats = new DefaultFormats();
            _queryBuilder = new SelectQueryBuilder<Test1>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new DefaultFormats());
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _classOptionsTupla = new ClassOptionsTupla<ColumnAttribute>(_classOptions, _columnAttribute);
        }

        [Fact]
        public void Should_create_an_instance()
        {
            GreaterThan<int> equal = new GreaterThan<int>(_classOptionsTupla, new DefaultFormats(), 1);

            Assert.NotNull(equal);
            Assert.NotNull(equal.Table);
            Assert.NotNull(equal.Column);
            Assert.Equal(1, equal.Value);
            Assert.Null(equal.LogicalOperator);
        }

        [Theory]
        [InlineData("AND", 4)]
        [InlineData("OR", 5)]
        public void Should_create_an_instance_1(string logicalOperator, int value)
        {
            GreaterThan<int> equal = new GreaterThan<int>(_classOptionsTupla, new DefaultFormats(), value, logicalOperator);

            Assert.NotNull(equal);
            Assert.NotNull(equal.Table);
            Assert.NotNull(equal.Column);
            Assert.Equal(value, equal.Value);
            Assert.NotNull(equal.LogicalOperator);
            Assert.Equal(logicalOperator, equal.LogicalOperator);
        }

        [Theory]
        [InlineData(null, 4, "Test1.Id > @Param")]
        [InlineData("AND", 4, "AND Test1.Id > @Param")]
        [InlineData("OR", 5, "OR Test1.Id > @Param")]
        public void Should_get_criteria_detail(string logicalOperator, int value, string querypart)
        {
            GreaterThan<int> equal = new GreaterThan<int>(_classOptionsTupla, new DefaultFormats(), value, logicalOperator);
            var result = equal.GetCriteria(_formats, _classOptions.PropertyOptions);

            Assert.NotNull(result);
            Assert.NotNull(result.SearchCriteria);
            Assert.NotNull(result.SearchCriteria.Column);
            Assert.NotNull(result.SearchCriteria.Table);
            Assert.NotNull(result.ParameterDetails);
            Assert.NotEmpty(result.ParameterDetails);
            var parameter = result.ParameterDetails.ElementAt(0);
            Assert.Equal(value, parameter.Value);
            Assert.NotNull(parameter.Name);
            Assert.NotEmpty(parameter.Name);
            Assert.NotNull(parameter.PropertyOptions);
            Assert.Equal(_columnAttribute.Name, parameter.PropertyOptions.ColumnAttribute.Name);
            Assert.NotNull(result.QueryPart);
            Assert.NotEmpty(result.QueryPart);
            Assert.Equal(querypart, result.ParameterReplace());
        }

        [Fact]
        public void Should_add_the_equality_query()
        {
            AndOrBase<Test1, SelectQuery<Test1>, IFormats> where = new AndOrBase<Test1, SelectQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            var andOr = where.GreaterThan(x => x.Id, 1);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Options);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_add_the_equality_query_with_and()
        {
            AndOrBase<Test1, SelectQuery<Test1>, IFormats> where = new AndOrBase<Test1, SelectQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            var andOr = where.GreaterThan(x => x.Id, 1).AndGreaterThan(x => x.IsTest, true);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Options);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_add_the_equality_query_with_or()
        {
            AndOrBase<Test1, SelectQuery<Test1>, IFormats> where = new AndOrBase<Test1, SelectQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            var andOr = where.GreaterThan(x => x.Id, 1).OrGreaterThan(x => x.IsTest, true);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Options);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}