using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Extensions;
using GSqlQuery.Test.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GSqlQuery.Test.SearchCriteria
{
    public class NotBetweenTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly IStatements _statements;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly ClassOptions _classOptions;

        public NotBetweenTest()
        {
            _statements = new Statements();
            _queryBuilder = new SelectQueryBuilder<Test1>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _statements);
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
        }

        [Fact]
        public void Should_create_an_instance()
        {
            NotBetween<int> equal = new NotBetween<int>(_tableAttribute, _columnAttribute, 1, 2);

            Assert.NotNull(equal);
            Assert.NotNull(equal.Table);
            Assert.NotNull(equal.Column);
            Assert.Equal(1, equal.Initial);
            Assert.Equal(2, equal.Final);
            Assert.Null(equal.LogicalOperator);
        }

        [Theory]
        [InlineData("AND", 4, 6)]
        [InlineData("OR", 5, 8)]
        public void Should_create_an_instance_1(string logicalOperator, int initValue, int finalValue)
        {
            NotBetween<int> equal = new NotBetween<int>(_tableAttribute, _columnAttribute, initValue, finalValue, logicalOperator);

            Assert.NotNull(equal);
            Assert.NotNull(equal.Table);
            Assert.NotNull(equal.Column);
            Assert.Equal(initValue, equal.Initial);
            Assert.Equal(finalValue, equal.Final);
            Assert.NotNull(equal.LogicalOperator);
            Assert.Equal(logicalOperator, equal.LogicalOperator);
        }

        [Theory]
        [InlineData(null, 4, 9, "Test1.Id NOT BETWEEN @Param AND @Param")]
        [InlineData("AND", 4, 8, "AND Test1.Id NOT BETWEEN @Param AND @Param")]
        [InlineData("OR", 5, 7, "OR Test1.Id NOT BETWEEN @Param AND @Param")]
        public void Should_get_criteria_detail(string logicalOperator, int inicialValue, int finalValue, string querypart)
        {
            NotBetween<int> equal = new NotBetween<int>(_tableAttribute, _columnAttribute, inicialValue, finalValue, logicalOperator);
            var result = equal.GetCriteria(_statements, _classOptions.PropertyOptions);

            Assert.NotNull(result);
            Assert.NotNull(result.SearchCriteria);
            Assert.NotNull(result.SearchCriteria.Column);
            Assert.NotNull(result.SearchCriteria.Table);
            Assert.NotNull(result.ParameterDetails);
            Assert.NotEmpty(result.ParameterDetails);
            var parameter = result.ParameterDetails.ElementAt(0);
            Assert.Equal(inicialValue, parameter.Value);
            Assert.NotNull(parameter.Name);
            Assert.NotEmpty(parameter.Name);
            Assert.NotNull(parameter.PropertyOptions);
            Assert.Equal(_columnAttribute.Name, parameter.PropertyOptions.ColumnAttribute.Name);
            Assert.NotNull(result.QueryPart);
            Assert.NotEmpty(result.QueryPart);
            Assert.Equal(querypart, result.ParameterReplace());
        }

        [Fact]
        public void Should_add_the_Between_query()
        {
            AndOrBase<Test1, SelectQuery<Test1>> where = new AndOrBase<Test1, SelectQuery<Test1>>(_queryBuilder);
            var andOr = where.NotBetween(x => x.Id, 1, 2);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Options);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_add_the_equality_query_with_and()
        {
            AndOrBase<Test1, SelectQuery<Test1>> where = new AndOrBase<Test1, SelectQuery<Test1>>(_queryBuilder);
            var andOr = where.NotBetween(x => x.Id, 1, 3).AndNotBetween(x => x.IsTest, true, false);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Options);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_add_the_equality_query_with_or()
        {
            AndOrBase<Test1, SelectQuery<Test1>> where = new AndOrBase<Test1, SelectQuery<Test1>>(_queryBuilder);
            var andOr = where.NotBetween(x => x.Id, 1, 5).OrNotBetween(x => x.IsTest, true, false);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Options);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}
