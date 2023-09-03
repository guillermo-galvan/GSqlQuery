using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Extensions;
using GSqlQuery.Test.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GSqlQuery.Test.SearchCriteria
{
    public class NotEqualTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly IStatements _statements;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly ClassOptions _classOptions;

        public NotEqualTest()
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
            NotEqual<int> test = new NotEqual<int>(_tableAttribute, _columnAttribute, 1);

            Assert.NotNull(test);
            Assert.NotNull(test.Table);
            Assert.NotNull(test.Column);
            Assert.Equal(1, test.Value);
            Assert.Null(test.LogicalOperator);
        }

        [Theory]
        [InlineData("AND", 4)]
        [InlineData("OR", 5)]
        public void Should_create_an_instance_1(string logicalOperator, int value)
        {
            NotEqual<int> test = new NotEqual<int>(_tableAttribute, _columnAttribute, value, logicalOperator);

            Assert.NotNull(test);
            Assert.NotNull(test.Table);
            Assert.NotNull(test.Column);
            Assert.Equal(value, test.Value);
            Assert.NotNull(test.LogicalOperator);
            Assert.Equal(logicalOperator, test.LogicalOperator);
        }

        [Theory]
        [InlineData(null, 4, "Test1.Id <> @Param")]
        [InlineData("AND", 4, "AND Test1.Id <> @Param")]
        [InlineData("OR", 5, "OR Test1.Id <> @Param")]
        public void Should_get_criteria_detail(string logicalOperator, int value, string querypart)
        {
            NotEqual<int> test = new NotEqual<int>(_tableAttribute, _columnAttribute, value, logicalOperator);
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
            Assert.Equal(_columnAttribute.Name, parameter.PropertyOptions.ColumnAttribute.Name);
            Assert.NotNull(result.QueryPart);
            Assert.NotEmpty(result.QueryPart);
            Assert.Equal(querypart, result.ParameterReplace());
        }

        [Fact]
        public void Should_add_the_equality_query()
        {
            AndOrBase<Test1, SelectQuery<Test1>, IStatements> where = new AndOrBase<Test1, SelectQuery<Test1>, IStatements>(_queryBuilder);
            var andOr = where.NotEqual(x => x.Id, 1);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Options);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_add_the_equality_query_with_and()
        {
            AndOrBase<Test1, SelectQuery<Test1>, IStatements> where = new AndOrBase<Test1, SelectQuery<Test1>, IStatements>(_queryBuilder);
            var andOr = where.NotEqual(x => x.Id, 1).AndNotEqual(x => x.IsTest, true);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Options);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_add_the_equality_query_with_or()
        {
            AndOrBase<Test1, SelectQuery<Test1>, IStatements> where = new AndOrBase<Test1, SelectQuery<Test1>, IStatements>(_queryBuilder);
            var andOr = where.NotEqual(x => x.Id, 1).OrNotEqual(x => x.IsTest, true);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Options);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}