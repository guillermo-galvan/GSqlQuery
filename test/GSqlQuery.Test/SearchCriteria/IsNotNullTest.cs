﻿using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GSqlQuery.Test.SearchCriteria
{
    public class IsNotNullTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly IStatements _statements;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly ClassOptions _classOptions;

        public IsNotNullTest()
        {
            _statements = new Statements();
            _queryBuilder = new SelectQueryBuilder<Test1>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new Statements());
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
        }

        [Fact]
        public void Should_create_an_instance()
        {
            IsNotNull test = new IsNotNull(_tableAttribute, _columnAttribute);

            Assert.NotNull(test);
            Assert.NotNull(test.Table);
            Assert.NotNull(test.Column);
            Assert.Null(test.LogicalOperator);
        }

        [Theory]
        [InlineData("AND")]
        [InlineData("OR")]
        public void Should_create_an_instance_1(string logicalOperator)
        {
            IsNotNull test = new IsNotNull(_tableAttribute, _columnAttribute, logicalOperator);

            Assert.NotNull(test);
            Assert.NotNull(test.Table);
            Assert.NotNull(test.Column);
            Assert.NotNull(test.LogicalOperator);
            Assert.Equal(logicalOperator, test.LogicalOperator);
        }

        [Theory]
        [InlineData(null, "Test1.Id IS NOT NULL")]
        [InlineData("AND", "AND Test1.Id IS NOT NULL")]
        [InlineData("OR", "OR Test1.Id IS NOT NULL")]
        public void Should_get_criteria_detail(string logicalOperator, string querypart)
        {
            IsNotNull test = new IsNotNull (_tableAttribute, _columnAttribute, logicalOperator);
            var result = test.GetCriteria(_statements, _classOptions.PropertyOptions);

            Assert.NotNull(result);
            Assert.NotNull(result.SearchCriteria);
            Assert.NotNull(result.SearchCriteria.Column);
            Assert.NotNull(result.SearchCriteria.Table);
            Assert.NotNull(result.ParameterDetails);
            Assert.Empty(result.ParameterDetails);
            Assert.NotNull(result.QueryPart);
            Assert.NotEmpty(result.QueryPart);
            Assert.Equal(querypart, result.QueryPart);
        }

        [Fact]
        public void Should_add_the_equality_query()
        {
            SelectWhere<Test1> where = new SelectWhere<Test1>(_queryBuilder);
            var andOr = where.IsNotNull(x => x.Id);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_add_the_equality_query_with_and()
        {
            SelectWhere<Test1> where = new SelectWhere<Test1>(_queryBuilder);
            var andOr = where.IsNotNull(x => x.Id).AndIsNotNull(x => x.IsTest);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_add_the_equality_query_with_or()
        {
            SelectWhere<Test1> where = new SelectWhere<Test1>(_queryBuilder);
            var andOr = where.IsNotNull(x => x.Id).OrIsNotNull(x => x.IsTest);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}