﻿using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Extensions;
using GSqlQuery.Test.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GSqlQuery.Test.SearchCriteria
{
    public class NotInTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly IFormats _formats;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly ClassOptions _classOptions;
        private readonly ClassOptionsTupla<ColumnAttribute> _classOptionsTupla;

        public NotInTest()
        {
            _formats = new DefaultFormats();
            _queryBuilder = new SelectQueryBuilder<Test1>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _formats);
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _classOptionsTupla = new ClassOptionsTupla<ColumnAttribute>(_classOptions, _columnAttribute);
        }

        [Fact]
        public void Should_create_an_instance()
        {
            NotIn<int> test = new NotIn<int>(_classOptionsTupla, _queryBuilder.Options, new int[] { 1, 2, 3, 1, 4 });

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
            NotIn<int> test = new NotIn<int>(_classOptionsTupla, _queryBuilder.Options, value, logicalOperator);

            Assert.NotNull(test);
            Assert.NotNull(test.Table);
            Assert.NotNull(test.Column);
            Assert.Equal(value, test.Values);
            Assert.NotNull(test.LogicalOperator);
            Assert.Equal(logicalOperator, test.LogicalOperator);
        }

        [Theory]
        [InlineData(null, new int[] { 1, 2, 3, 1, 4 }, "Test1.Id NOT IN (@Param,@Param,@Param,@Param,@Param)")]
        [InlineData("AND", new int[] { 4, 5, 6, 7, 8 }, "AND Test1.Id NOT IN (@Param,@Param,@Param,@Param,@Param)")]
        [InlineData("OR", new int[] { 14, 15, 16, 17, 18 }, "OR Test1.Id NOT IN (@Param,@Param,@Param,@Param,@Param)")]
        public void Should_get_criteria_detail(string logicalOperator, int[] value, string querypart)
        {
            NotIn<int> test = new NotIn<int>(_classOptionsTupla, _queryBuilder.Options, value, logicalOperator);
            var result = test.GetCriteria(_formats, _classOptions.PropertyOptions);

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
            Assert.NotNull(parameter.PropertyOptions);
            Assert.Contains("@", parameter.Name);
            Assert.Equal(_columnAttribute.Name, parameter.PropertyOptions.ColumnAttribute.Name);
            Assert.NotNull(result.QueryPart);
            Assert.NotEmpty(result.QueryPart);
            Assert.Equal(querypart, result.ParameterReplace());
        }

        [Fact]
        public void Should_add_the_equality_query()
        {
            AndOrBase<Test1, SelectQuery<Test1>, IFormats> where = new AndOrBase<Test1, SelectQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            var andOr = where.NotIn(x => x.Id, new int[] { 4, 5, 6, 7, 8, 9, 10 });
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_add_the_equality_query_with_and()
        {
            AndOrBase<Test1, SelectQuery<Test1>, IFormats> where = new AndOrBase<Test1, SelectQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            var andOr = where.NotIn(x => x.Id, new int[] { 4, 5, 6, 7, 8, 9, 10 }).AndNotIn(x => x.Id, new int[] { 4, 5, 6, 7, 8, 9, 10 });
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_add_the_equality_query_with_or()
        {
            AndOrBase<Test1, SelectQuery<Test1>, IFormats> where = new AndOrBase<Test1, SelectQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            var andOr = where.NotIn(x => x.Id, new int[] { 4, 5, 6, 7, 8, 9, 10 }).OrNotIn(x => x.Id, new int[] { 4, 5, 6, 7, 8, 9, 10 });
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}