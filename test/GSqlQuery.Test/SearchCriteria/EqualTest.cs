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
    public class EqualTest
    {
        private readonly PropertyOptions _columnAttribute;
        private readonly QueryOptions _queryOptions;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly ClassOptions _classOptions;
        private readonly ClassOptionsTupla<PropertyOptions> _classOptionsTupla;

        public EqualTest()
        {
            _queryOptions = new QueryOptions(new DefaultFormats());
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            _queryBuilder = new SelectQueryBuilder<Test1>(dynamicQuery, new QueryOptions(new DefaultFormats()));
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions[nameof(Test1.Id)];
            _classOptionsTupla = new ClassOptionsTupla<PropertyOptions>(_classOptions, _columnAttribute);
        }

        [Fact]
        public void Should_create_an_instance()
        {
            Equal<int> test = new Equal<int>(_classOptionsTupla, new DefaultFormats(), 1);

            Assert.NotNull(test);
            Assert.NotNull(test.Table);
            Assert.NotNull(test.Column);
            Assert.Equal(1, test.Value);
            Assert.Null(test.LogicalOperator);
        }

        [Theory]
        [InlineData("AND", 4)]
        [InlineData("OR", 5)]
        public void Should_create_an_instance_1(string logicalOperator, int value)
        {
            Equal<int> test = new Equal<int>(_classOptionsTupla, new DefaultFormats(), value, logicalOperator);

            Assert.NotNull(test);
            Assert.NotNull(test.Table);
            Assert.NotNull(test.Column);
            Assert.Equal(value, test.Value);
            Assert.NotNull(test.LogicalOperator);
            Assert.Equal(logicalOperator, test.LogicalOperator);
        }

        [Theory]
        [InlineData(null, 4, "Test1.Id = @Param")]
        [InlineData("AND", 4, "AND Test1.Id = @Param")]
        [InlineData("OR", 5, "OR Test1.Id = @Param")]
        public void Should_get_criteria_detail(string logicalOperator, int value, string querypart)
        {
            Equal<int> test = new Equal<int>(_classOptionsTupla, new DefaultFormats(), value, logicalOperator);
            var result = test.GetCriteria();

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
            Assert.Contains("@", parameter.Name);
            Assert.NotNull(result.QueryPart);
            Assert.NotEmpty(result.QueryPart);
            Assert.Equal(querypart, result.ParameterReplace());
        }

        [Fact]
        public void Should_add_the_equality_query()
        {
            AndOrBase<Test1, SelectQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, SelectQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            var andOr = where.Equal(x => x.Id, 1);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_add_the_equality_query_with_and()
        {
            AndOrBase<Test1, SelectQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, SelectQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            var andOr = where.Equal(x => x.Id, 1).AndEqual(x => x.IsTest, true);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_add_the_equality_query_with_or()
        {
            AndOrBase<Test1, SelectQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, SelectQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            var andOr = where.Equal(x => x.Id, 1).OrEqual(x => x.IsTest, true);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}