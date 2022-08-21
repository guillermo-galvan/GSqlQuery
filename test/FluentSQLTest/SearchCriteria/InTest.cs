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
    public class InTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly IStatements _statements;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;

        public InTest()
        {
            _columnAttribute = new ColumnAttribute("Id");
            _tableAttribute = new TableAttribute("Test1");
            _statements = new FluentSQL.Default.Statements();
            _queryBuilder = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new ConnectionOptions(new FluentSQL.Default.Statements()));
        }

        [Fact]
        public void Should_create_an_instance()
        {
            In<int> test = new(_tableAttribute, _columnAttribute, new int[] { 1,2,3,1,4});

            Assert.NotNull(test);
            Assert.NotNull(test.Table);
            Assert.NotNull(test.Column);
            Assert.Equal(new int[] { 1, 2, 3, 1, 4 }, test.Values);
            Assert.Null(test.LogicalOperator);
        }

        [Theory]
        [InlineData("AND", new int[] { 1, 2, 3, 1, 4 })]
        [InlineData("OR", new int[] { 4,5,6,7,8,9,10 })]
        public void Should_create_an_instance_1(string logicalOperator, int[] value)
        {
            In<int> test = new(_tableAttribute, _columnAttribute, value, logicalOperator);

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
            In<int> test = new(_tableAttribute, _columnAttribute, value, logicalOperator);
            var result = test.GetCriteria(_statements);

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
            Assert.NotNull(result.QueryPart);
            Assert.NotEmpty(result.QueryPart);            
            Assert.Equal(querypart, result.ParameterReplace());
        }

        [Fact]
        public void Should_add_the_equality_query()
        {
            SelectWhere<Test1> where = new(_queryBuilder);
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
            SelectWhere<Test1> where = new(_queryBuilder);
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
            SelectWhere<Test1> where = new(_queryBuilder);
            var andOr = where.In(x => x.Id, new int[] { 4, 5, 6, 7, 8, 9, 10 }).OrIn(x => x.Id, new int[] { 4, 5, 6, 7, 8, 9, 10 });
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}
