﻿using FluentSQL;
using FluentSQL.Default;
using FluentSQL.Helpers;
using FluentSQL.Models;
using FluentSQL.SearchCriteria;
using FluentSQLTest.Extensions;
using FluentSQLTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FluentSQLTest.SearchCriteria
{
    public class GroupTest
    {
        private readonly TableAttribute _tableAttribute;
        private readonly IStatements _statements;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly ClassOptions _classOptions;

        public GroupTest()
        {
            _statements = new FluentSQL.Default.Statements();
            _queryBuilder = new( new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new FluentSQL.Default.Statements());
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _tableAttribute = _classOptions.Table;
        }

        [Fact]
        public void Should_create_an_instance()
        {
            SelectWhere<Test1> andOr = new (_queryBuilder);
            Group<Test1, SelectQuery<Test1>> test = new (_tableAttribute,null, andOr);

            Assert.NotNull(test);
            Assert.NotNull(test.Table);
            Assert.NotNull(test.Column);
            Assert.NotNull(test.AndOr);
            Assert.Null(test.LogicalOperator);
        }

        [Theory]
        [InlineData("AND")]
        [InlineData("OR")]
        public void Should_create_an_instance_1(string logicalOperator)
        {
            SelectWhere<Test1> andOr = new(_queryBuilder);
            Group<Test1, SelectQuery<Test1>> test = new(_tableAttribute, logicalOperator, andOr);

            Assert.NotNull(test);
            Assert.NotNull(test.Table);
            Assert.NotNull(test.Column);
            Assert.NotNull(test.AndOr);
            Assert.NotNull(test.LogicalOperator);
            Assert.Equal(logicalOperator, test.LogicalOperator);
        }

        [Theory]
        [InlineData(null, "dsdasdas", "(Test1.Name = @Param AND Test1.Create <> @Param)")]
        [InlineData("AND", "21313", "AND (Test1.Name = @Param AND Test1.Create <> @Param)")]
        [InlineData("OR", "rwtfsd", "OR (Test1.Name = @Param AND Test1.Create <> @Param)")]
        public void Should_get_criteria_detail(string logicalOperator, string value, string querypart)
        {
            SelectWhere<Test1> andOr = new(_queryBuilder);
            Group<Test1, SelectQuery<Test1>> test = new(_tableAttribute, logicalOperator, andOr);
            test.Equal(x => x.Name, value).AndNotEqual(x => x.Create, DateTime.Now);
            var result = test.GetCriteria(_statements, _classOptions.PropertyOptions);

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
            Assert.NotNull(result.QueryPart);
            Assert.NotEmpty(result.QueryPart);
            var a = result.ParameterReplace();
            Assert.Equal(querypart, result.ParameterReplace());
        }

        [Fact]
        public void Should_add_the_equality_query()
        {
            SelectWhere<Test1> where = new(_queryBuilder);
            var andOr = where.BeginGroup().Equal(x => x.Id, 1).CloseGroup();
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_add_the_equality_query_with_and()
        {
            SelectWhere<Test1> where = new(_queryBuilder);
            var andOr = where.BeginGroup().Equal(x => x.Id, 1).AndEqual(x => x.IsTest, true).CloseGroup();
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_add_the_equality_query_with_or()
        {
            SelectWhere<Test1> where = new(_queryBuilder);
            var andOr = where.BeginGroup().Equal(x => x.Id, 1).OrEqual(x => x.IsTest, true).CloseGroup();
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }
    }
}