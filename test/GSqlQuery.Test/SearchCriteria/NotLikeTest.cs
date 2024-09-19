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
    public class NotLikeTest
    {
        private readonly PropertyOptions _columnAttribute;
        private readonly QueryOptions _queryOptions;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly ClassOptions _classOptions;
        private readonly ClassOptionsTupla<PropertyOptions> _classOptionsTupla;
        private uint _parameterId = 0;
        private readonly Expression<Func<Test1, int>> _dynamicQuery;

        public NotLikeTest()
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
            var dynamicQuery = _dynamicQuery;
            NotLike<Test1, int> test = new NotLike<Test1, int>(_classOptionsTupla.ClassOptions, new DefaultFormats(), "1", null, ref dynamicQuery);

            Assert.NotNull(test);
            Assert.NotNull(test.Formats);
            Assert.NotNull(test.Expression);
            Assert.NotNull(test.ClassOptions);
            Assert.Equal("1", test.Data);
            Assert.Null(test.LogicalOperator);
        }

        [Theory]
        [InlineData("AND", "re")]
        [InlineData("OR", "der")]
        public void Should_create_an_instance_1(string logicalOperator, string value)
        {
            var dynamicQuery = _dynamicQuery;
            NotLike<Test1, int> test = new NotLike<Test1, int>(_classOptionsTupla.ClassOptions, new DefaultFormats(), value, logicalOperator, ref dynamicQuery);

            Assert.NotNull(test);
            Assert.NotNull(test.Formats);
            Assert.NotNull(test.Expression);
            Assert.NotNull(test.ClassOptions);
            Assert.Equal(value, test.Data);
            Assert.NotNull(test.LogicalOperator);
            Assert.Equal(logicalOperator, test.LogicalOperator);
        }

        [Theory]
        [InlineData(null, "venga", "Test1.Id NOT LIKE CONCAT('%', @Param, '%')")]
        [InlineData("AND", "res", "AND Test1.Id NOT LIKE CONCAT('%', @Param, '%')")]
        [InlineData("OR", "pollo", "OR Test1.Id NOT LIKE CONCAT('%', @Param, '%')")]
        public void Should_get_criteria_detail(string logicalOperator, string value, string querypart)
        {
            var dynamicQuery = _dynamicQuery;
            NotLike<Test1, int> test = new NotLike<Test1, int>(_classOptionsTupla.ClassOptions, new DefaultFormats(), value, logicalOperator, ref dynamicQuery);
            var result = test.GetCriteria(ref _parameterId);

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
            var andOr = where.NotLike(x => x.Id, "ds");
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
            var andOr = where.NotLike(x => x.Id, "1256").AndNotLike(x => x.IsTest, "1");
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
            var andOr = where.NotLike(x => x.Id, "1256").OrNotLike(x => x.IsTest, "45981");
            Assert.NotNull(andOr);
            var result = andOr.Create();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}