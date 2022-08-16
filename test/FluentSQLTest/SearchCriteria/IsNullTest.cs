using FluentSQL.Default;
using FluentSQL.Models;
using FluentSQL.SearchCriteria;
using FluentSQLTest.Extensions;
using FluentSQLTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest.SearchCriteria
{
    public class IsNullTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly IStatements _statements;
        private readonly QueryBuilder<Test1> _queryBuilder;

        public IsNullTest()
        {
            _columnAttribute = new ColumnAttribute("Id");
            _tableAttribute = new TableAttribute("Test1");
            _statements = new FluentSQL.Default.Statements();
            _queryBuilder = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new FluentSQL.Default.Statements(), QueryType.Select);
        }

        [Fact]
        public void Should_create_an_instance()
        {
            IsNull test = new(_tableAttribute, _columnAttribute);

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
            IsNull test = new(_tableAttribute, _columnAttribute, logicalOperator);

            Assert.NotNull(test);
            Assert.NotNull(test.Table);
            Assert.NotNull(test.Column);            
            Assert.NotNull(test.LogicalOperator);
            Assert.Equal(logicalOperator, test.LogicalOperator);
        }

        [Theory]
        [InlineData(null, "Test1.Id IS NULL")]
        [InlineData("AND", "AND Test1.Id IS NULL")]
        [InlineData("OR", "OR Test1.Id IS NULL")]
        public void Should_get_criteria_detail(string logicalOperator, string querypart)
        {
            IsNull test = new(_tableAttribute, _columnAttribute, logicalOperator);
            var result = test.GetCriteria(_statements);

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
            IWhere<Test1> where = new Where<Test1>(_queryBuilder);
            var andOr = where.IsNull(x => x.Id);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_add_the_equality_query_with_and()
        {
            IWhere<Test1> where = new Where<Test1>(_queryBuilder);
            var andOr = where.IsNull(x => x.Id).AndIsNull(x => x.IsTest);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_add_the_equality_query_with_or()
        {
            IWhere<Test1> where = new Where<Test1>(_queryBuilder);
            var andOr = where.IsNull(x => x.Id).OrIsNull(x => x.IsTest);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}
