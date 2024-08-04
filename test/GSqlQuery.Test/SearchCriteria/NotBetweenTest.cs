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
    public class NotBetweenTest
    {
        private readonly PropertyOptions _columnAttribute;
        private readonly QueryOptions _queryOptions;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly ClassOptions _classOptions;
        private readonly ClassOptionsTupla<PropertyOptions> _classOptionsTupla;
        private uint _parameterId = 0;

        public NotBetweenTest()
        {
            _queryOptions = new QueryOptions(new DefaultFormats());
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            _queryBuilder = new SelectQueryBuilder<Test1>(dynamicQuery, _queryOptions);
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions[nameof(Test1.Id)];
            _classOptionsTupla = new ClassOptionsTupla<PropertyOptions>(_classOptions, _columnAttribute);
        }

        [Fact]
        public void Should_create_an_instance()
        {
            NotBetween<int> equal = new NotBetween<int>(_classOptionsTupla, new DefaultFormats(), 1, 2);

            Assert.NotNull(equal);
            Assert.NotNull(equal.Column);
            Assert.Equal(1, equal.Initial);
            Assert.Equal(2, equal.Final);
            Assert.Null(equal.LogicalOperator);
        }

        [Theory]
        [InlineData("AND", 4, 6)]
        [InlineData("OR", 5, 8)]
        public void Should_create_an_instance_1(string logicalOperator, int initValue, int finalValue)
        {
            NotBetween<int> equal = new NotBetween<int>(_classOptionsTupla, new DefaultFormats(), initValue, finalValue, logicalOperator);

            Assert.NotNull(equal);
            Assert.NotNull(equal.Column);
            Assert.Equal(initValue, equal.Initial);
            Assert.Equal(finalValue, equal.Final);
            Assert.NotNull(equal.LogicalOperator);
            Assert.Equal(logicalOperator, equal.LogicalOperator);
        }

        [Theory]
        [InlineData(null, 4, 9, "Test1.Id NOT BETWEEN @Param AND @Param")]
        [InlineData("AND", 4, 8, "AND Test1.Id NOT BETWEEN @Param AND @Param")]
        [InlineData("OR", 5, 7, "OR Test1.Id NOT BETWEEN @Param AND @Param")]
        public void Should_get_criteria_detail(string logicalOperator, int inicialValue, int finalValue, string querypart)
        {
            NotBetween<int> equal = new NotBetween<int>(_classOptionsTupla, new DefaultFormats(), inicialValue, finalValue, logicalOperator);
            var result = equal.GetCriteria(ref _parameterId);

            Assert.NotNull(result);
            Assert.NotNull(result.SearchCriteria);
            Assert.NotNull(result.SearchCriteria.Column);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            var parameter = result.Values.First();
            Assert.Equal(inicialValue, parameter.Value);
            Assert.NotNull(parameter.Name);
            Assert.NotEmpty(parameter.Name);
            Assert.Contains("@", parameter.Name);
            Assert.NotNull(result.QueryPart);
            Assert.NotEmpty(result.QueryPart);
            Assert.Equal(querypart, result.ParameterReplace());
        }

        [Fact]
        public void Should_add_the_Between_query()
        {
            AndOrBase<Test1, SelectQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, SelectQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            var andOr = where.NotBetween(x => x.Id, 1, 2);
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
            var andOr = where.NotBetween(x => x.Id, 1, 3).AndNotBetween(x => x.IsTest, true, false);
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
            var andOr = where.NotBetween(x => x.Id, 1, 5).OrNotBetween(x => x.IsTest, true, false);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}