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
    public class InTest
    {
        private readonly PropertyOptions _columnAttribute;
        private readonly QueryOptions _queryOptions;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly ClassOptions _classOptions;
        private readonly ClassOptionsTupla<PropertyOptions> _classOptionsTupla;

        public InTest()
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
            In<int> test = new In<int>(_classOptionsTupla, _queryBuilder.QueryOptions.Formats, new int[] { 1, 2, 3, 1, 4 });

            Assert.NotNull(test);
            Assert.NotNull(test.Table);
            Assert.NotNull(test.Column);
            Assert.Equal(new int[] { 1, 2, 3, 1, 4 }, test.Values);
            Assert.Null(test.LogicalOperator);
        }

        [Theory]
        [InlineData("AND", new int[] { 1, 2, 3, 1, 4 })]
        [InlineData("OR", new int[] { 4, 5, 6, 7, 8, 9, 10 })]
        public void Should_create_an_instance_1(string logicalOperator, int[] value)
        {
            In<int> test = new In<int>(_classOptionsTupla, _queryBuilder.QueryOptions.Formats, value, logicalOperator);

            Assert.NotNull(test);
            Assert.NotNull(test.Table);
            Assert.NotNull(test.Column);
            Assert.Equal(value, test.Values);
            Assert.NotNull(test.LogicalOperator);
            Assert.Equal(logicalOperator, test.LogicalOperator);
        }

        [Theory]
        [InlineData(null, new int[] { 1, 2, 3, 1, 4 }, "Test1.Id IN (@Param,@Param,@Param,@Param,@Param)")]
        [InlineData("AND", new int[] { 4, 5, 6, 7, 8 }, "AND Test1.Id IN (@Param,@Param,@Param,@Param,@Param)")]
        [InlineData("OR", new int[] { 14, 15, 16, 17, 18 }, "OR Test1.Id IN (@Param,@Param,@Param,@Param,@Param)")]
        public void Should_get_criteria_detail(string logicalOperator, int[] value, string querypart)
        {
            In<int> test = new In<int>(_classOptionsTupla, _queryBuilder.QueryOptions.Formats, value, logicalOperator);
            var result = test.GetCriteria();

            Assert.NotNull(result);
            Assert.NotNull(result.SearchCriteria);
            Assert.NotNull(result.SearchCriteria.Column);
            Assert.NotNull(result.SearchCriteria.Table);
            Assert.NotNull(result.ParameterDetails);
            Assert.NotEmpty(result.ParameterDetails);
            var parameter = result.ParameterDetails.ElementAt(0);
            Assert.Equal(value[0], parameter.Value);
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
            var andOr = where.In(x => x.Id, new int[] { 4, 5, 6, 7, 8, 9, 10 });
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
            var andOr = where.In(x => x.Id, new int[] { 4, 5, 6, 7, 8, 9, 10 }).AndIn(x => x.Id, new int[] { 4, 5, 6, 7, 8, 9, 10 });
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
            var andOr = where.In(x => x.Id, new int[] { 4, 5, 6, 7, 8, 9, 10 }).OrIn(x => x.Id, new int[] { 4, 5, 6, 7, 8, 9, 10 });
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}