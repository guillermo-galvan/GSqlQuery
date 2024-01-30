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
    public class LikeTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly IFormats _formats;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly ClassOptions _classOptions;
        private readonly ClassOptionsTupla<ColumnAttribute> _classOptionsTupla;

        public LikeTest()
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
            Like test = new Like(_classOptionsTupla, new DefaultFormats(), "1");

            Assert.NotNull(test);
            Assert.NotNull(test.Table);
            Assert.NotNull(test.Column);
            Assert.Equal("1", test.Value);
            Assert.Null(test.LogicalOperator);
        }

        [Theory]
        [InlineData("AND", "re")]
        [InlineData("OR", "der")]
        public void Should_create_an_instance_1(string logicalOperator, string value)
        {
            Like test = new Like(_classOptionsTupla, new DefaultFormats(), value, logicalOperator);

            Assert.NotNull(test);
            Assert.NotNull(test.Table);
            Assert.NotNull(test.Column);
            Assert.Equal(value, test.Value);
            Assert.NotNull(test.LogicalOperator);
            Assert.Equal(logicalOperator, test.LogicalOperator);
        }

        [Theory]
        [InlineData(null, "venga", "Test1.Id LIKE CONCAT('%', @Param, '%')")]
        [InlineData("AND", "res", "AND Test1.Id LIKE CONCAT('%', @Param, '%')")]
        [InlineData("OR", "pollo", "OR Test1.Id LIKE CONCAT('%', @Param, '%')")]
        public void Should_get_criteria_detail(string logicalOperator, string value, string querypart)
        {
            Like test = new Like(_classOptionsTupla, new DefaultFormats(), value, logicalOperator);
            var result = test.GetCriteria(_formats, _classOptions.PropertyOptions);

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
            var a = result.ParameterReplace();
            Assert.Equal(querypart, result.ParameterReplace());
        }

        [Fact]
        public void Should_add_the_equality_query()
        {
            AndOrBase<Test1, SelectQuery<Test1>, IFormats> where = new AndOrBase<Test1, SelectQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            var andOr = where.Like(x => x.Id, "ds");
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
            var andOr = where.Like(x => x.Id, "1256").AndLike(x => x.IsTest, "1");
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
            var andOr = where.Like(x => x.Id, "1256").OrLike(x => x.IsTest, "45981");
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Options);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}