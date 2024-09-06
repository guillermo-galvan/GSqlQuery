using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Extensions;
using GSqlQuery.Test.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace GSqlQuery.Test.SearchCriteria
{
    public class BetweenTest
    {
        private readonly PropertyOptions _columnAttribute;
        private readonly QueryOptions _queryOptions;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly ClassOptions _classOptions;
        private readonly ClassOptionsTupla<PropertyOptions> _classOptionsTupla;
        private uint _parameterId = 0;
        private readonly Expression<Func<Test1, int>> _dynamicQuery;

        public BetweenTest()
        {
            _queryOptions = new QueryOptions(new DefaultFormats());
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            _queryBuilder = new SelectQueryBuilder<Test1>(dynamicQuery, new QueryOptions(new DefaultFormats()));
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions[nameof(Test1.Id)];
            _classOptionsTupla = new ClassOptionsTupla<PropertyOptions>(_classOptions, _columnAttribute);
            _dynamicQuery = (x) => x.Id;
        }

        [Fact]
        public void Should_create_an_instance()
        {
            Between<Test1, int> equal = new Between<Test1, int>(_classOptionsTupla.ClassOptions, new DefaultFormats(), 1, 2, null, _dynamicQuery);

            Assert.NotNull(equal);
            Assert.NotNull(equal.Formats);
            Assert.NotNull(equal.Expression);
            Assert.NotNull(equal.ClassOptions);
            Assert.Equal(1, equal.Initial);
            Assert.Equal(2, equal.Final);
            Assert.Null(equal.LogicalOperator);
        }

        [Theory]
        [InlineData("AND", 4, 6)]
        [InlineData("OR", 5, 8)]
        public void Should_create_an_instance_1(string logicalOperator, int initValue, int finalValue)
        {
            Between<Test1, int> equal = new Between<Test1, int>(_classOptionsTupla.ClassOptions, new DefaultFormats(), initValue, finalValue, logicalOperator, _dynamicQuery);

            Assert.NotNull(equal);
            Assert.NotNull(equal.Formats);
            Assert.NotNull(equal.Expression);
            Assert.NotNull(equal.ClassOptions);
            Assert.Equal(initValue, equal.Initial);
            Assert.Equal(finalValue, equal.Final);
            Assert.NotNull(equal.LogicalOperator);
            Assert.Equal(logicalOperator, equal.LogicalOperator);
        }

        [Theory]
        [InlineData(null, 4, 9, "Test1.Id BETWEEN @Param AND @Param")]
        [InlineData("AND", 4, 8, "AND Test1.Id BETWEEN @Param AND @Param")]
        [InlineData("OR", 5, 7, "OR Test1.Id BETWEEN @Param AND @Param")]
        public void Should_get_criteria_detail(string logicalOperator, int inicialValue, int finalValue, string querypart)
        {
            Between<Test1, int> equal = new Between<Test1, int>(_classOptionsTupla.ClassOptions, new DefaultFormats(), inicialValue, finalValue, logicalOperator, _dynamicQuery);
            var result = equal.GetCriteria(ref _parameterId);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Count > 0);
            Assert.NotEmpty(result.Keys);
            Assert.NotEmpty(result.Values);
            Assert.NotNull(result.PropertyOptions);
            Assert.NotNull(result.SearchCriteria);
            Assert.NotNull(result.SearchCriteria.ClassOptions);
            Assert.NotNull(result.SearchCriteria.Formats);
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
            var andOr = where.Between(x => x.Id, 1, 2);
            Assert.NotNull(andOr);
            var result = andOr.Create();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_add_the_equality_query_with_and()
        {
            AndOrBase<Test1, SelectQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, SelectQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            var andOr = where.Between(x => x.Id, 1, 3).AndBetween(x => x.IsTest, true, false);
            Assert.NotNull(andOr);
            var result = andOr.Create();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_add_the_equality_query_with_or()
        {
            AndOrBase<Test1, SelectQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, SelectQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            var andOr = where.Between(x => x.Id, 1, 5).OrBetween(x => x.IsTest, true, false);
            Assert.NotNull(andOr);
            var result = andOr.Create();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}