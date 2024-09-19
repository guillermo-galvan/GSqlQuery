using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace GSqlQuery.Test.SearchCriteria
{
    public class IsNullTest
    {
        private readonly PropertyOptions _columnAttribute;
        private readonly QueryOptions _queryOptions;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly ClassOptions _classOptions;
        private readonly ClassOptionsTupla<PropertyOptions> _classOptionsTupla;
        private uint _parameterId = 0;
        private readonly Expression<Func<Test1, int>> _dynamicQuery;

        public IsNullTest()
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
            IsNull<Test1, int> test = new IsNull<Test1, int>(_classOptionsTupla.ClassOptions, new DefaultFormats(), null, ref dynamicQuery);

            Assert.NotNull(test);
            Assert.NotNull(test.Formats);
            Assert.NotNull(test.Expression);
            Assert.NotNull(test.ClassOptions);
            Assert.Null(test.LogicalOperator);
        }

        [Theory]
        [InlineData("AND")]
        [InlineData("OR")]
        public void Should_create_an_instance_1(string logicalOperator)
        {
            var dynamicQuery = _dynamicQuery;
            IsNull<Test1, int> test = new IsNull<Test1, int>(_classOptionsTupla.ClassOptions, new DefaultFormats(), logicalOperator, ref dynamicQuery);

            Assert.NotNull(test);
            Assert.NotNull(test.Formats);
            Assert.NotNull(test.Expression);
            Assert.NotNull(test.ClassOptions);
            Assert.NotNull(test.LogicalOperator);
            Assert.Equal(logicalOperator, test.LogicalOperator);
        }

        [Theory]
        [InlineData(null, "Test1.Id IS NULL")]
        [InlineData("AND", "AND Test1.Id IS NULL")]
        [InlineData("OR", "OR Test1.Id IS NULL")]
        public void Should_get_criteria_detail(string logicalOperator, string querypart)
        {
            var dynamicQuery = _dynamicQuery;
            IsNull<Test1, int> test = new IsNull<Test1, int>(_classOptionsTupla.ClassOptions, new DefaultFormats(), logicalOperator, ref dynamicQuery);
            var result = test.GetCriteria(ref _parameterId);

            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.True(result.Count == 0);
            Assert.Empty(result.Keys);
            Assert.Empty(result.Values);
            Assert.NotNull(result.PropertyOptions);
            Assert.NotNull(result.SearchCriteria);
            Assert.NotNull(result.SearchCriteria.ClassOptions);
            Assert.NotNull(result.SearchCriteria.Formats);
            Assert.NotNull(result.QueryPart);
            Assert.NotEmpty(result.QueryPart);
            Assert.Equal(querypart, result.QueryPart);
        }

        [Fact]
        public void Should_add_the_equality_query()
        {
            AndOrBase<Test1, SelectQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, SelectQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            var andOr = where.IsNull(x => x.Id);
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
            var andOr = where.IsNull(x => x.Id).AndIsNull(x => x.IsTest);
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
            var andOr = where.IsNull(x => x.Id).OrIsNull(x => x.IsTest);
            Assert.NotNull(andOr);
            var result = andOr.Create();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}