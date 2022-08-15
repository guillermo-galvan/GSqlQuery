using FluentSQL.Default;
using FluentSQL.Models;
using FluentSQL.SearchCriteria;
using FluentSQLTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest.SearchCriteria
{
    public class EqualTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly IStatements _statements;
        private readonly QueryBuilder<Test1> _queryBuilder;
        
        public EqualTest()
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
            Equal<int> equal = new (_tableAttribute, _columnAttribute, 1);

            Assert.NotNull(equal);
            Assert.NotNull(equal.Table);
            Assert.NotNull(equal.Column);
            Assert.Equal(1,equal.Value);
            Assert.Null(equal.LogicalOperator);
        }

        [Fact]
        public void Should_create_an_instance_1()
        {
            Equal<int> equal = new(_tableAttribute, _columnAttribute, 1,"AND");

            Assert.NotNull(equal);
            Assert.NotNull(equal.Table);
            Assert.NotNull(equal.Column);
            Assert.Equal(1, equal.Value);
            Assert.NotNull(equal.LogicalOperator);
            Assert.Equal("AND", equal.LogicalOperator);
        }

        [Fact]
        public void Should_get_criteria_detail()
        {
            Equal<int> equal = new(_tableAttribute, _columnAttribute, 1);
            var result = equal.GetCriteria(_statements);

            Assert.NotNull(result);
            Assert.NotNull(result.SearchCriteria);
            Assert.NotNull(result.SearchCriteria.Column);
            Assert.NotNull(result.SearchCriteria.Table);
            Assert.NotNull(result.ParameterValue);
            Assert.Equal(1,result.ParameterValue);
            Assert.NotNull(result.ParameterName);
            Assert.NotEmpty(result.ParameterName);
            Assert.NotNull(result.Criterion);
            Assert.NotEmpty(result.Criterion);
        }

        [Fact]
        public void Should_add_the_equality_query()
        {
            IWhere<Test1> where = new Where<Test1>(_queryBuilder);
            var andOr = where.Equal(x => x.Id, 1);
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
            var andOr = where.Equal(x => x.Id, 1).AndEqual(x => x.IsTest, true);
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
            var andOr = where.Equal(x => x.Id, 1).OrEqual(x => x.IsTest, true);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}
