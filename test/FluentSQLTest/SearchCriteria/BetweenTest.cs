using FluentSQL.Default;
using FluentSQL.Helpers;
using FluentSQL.Models;
using FluentSQL.SearchCriteria;
using FluentSQLTest.Extensions;
using FluentSQLTest.Models;
using System.Data.Common;

namespace FluentSQLTest.SearchCriteria
{
    public class BetweenTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly IStatements _statements;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly ClassOptions _classOptions;
        private readonly SelectQueryBuilder<Test1, DbConnection> _selectQueryBuilder;

        public BetweenTest()
        {
            _statements = new FluentSQL.Default.Statements();
            _queryBuilder = new(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
               new FluentSQL.Default.Statements());
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _selectQueryBuilder = new(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()));
        }

        [Fact]
        public void Should_create_an_instance()
        {
            Between<int> equal = new(_tableAttribute, _columnAttribute, 1,2);

            Assert.NotNull(equal);
            Assert.NotNull(equal.Table);
            Assert.NotNull(equal.Column);
            Assert.Equal(1, equal.Initial);
            Assert.Equal(2, equal.Final);
            Assert.Null(equal.LogicalOperator);
        }

        [Theory]
        [InlineData("AND", 4,6)]
        [InlineData("OR", 5,8)]
        public void Should_create_an_instance_1(string logicalOperator, int initValue, int finalValue)
        {
            Between<int> equal = new(_tableAttribute, _columnAttribute, initValue, finalValue, logicalOperator);

            Assert.NotNull(equal);
            Assert.NotNull(equal.Table);
            Assert.NotNull(equal.Column);
            Assert.Equal(initValue, equal.Initial);
            Assert.Equal(finalValue, equal.Final);
            Assert.NotNull(equal.LogicalOperator);
            Assert.Equal(logicalOperator, equal.LogicalOperator);
        }

        [Theory]
        [InlineData(null, 4,9, "Test1.Id BETWEEN @Param AND @Param")]
        [InlineData("AND", 4,8, "AND Test1.Id BETWEEN @Param AND @Param")]
        [InlineData("OR", 5,7, "OR Test1.Id BETWEEN @Param AND @Param")]
        public void Should_get_criteria_detail(string logicalOperator, int inicialValue, int finalValue,string querypart)
        {
            Between<int> equal = new(_tableAttribute, _columnAttribute, inicialValue, finalValue, logicalOperator);
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
            SelectWhere<Test1> where = new(_queryBuilder);
            var andOr = where.Between(x => x.Id, 1,2);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_add_the_equality_query_with_and()
        {
            SelectWhere<Test1> where = new (_queryBuilder);
            var andOr = where.Between(x => x.Id, 1,3).AndBetween(x => x.IsTest, true,false);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_add_the_equality_query_with_or()
        {
            SelectWhere<Test1> where = new (_queryBuilder);
            var andOr = where.Between(x => x.Id, 1,5).OrBetween(x => x.IsTest, true,false);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_add_the_Between_query2()
        {
            SelectWhere<Test1, DbConnection> where = new(_selectQueryBuilder);
            var andOr = where.Between(x => x.Id, 1, 2);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_add_the_equality_query_with_and2()
        {
            SelectWhere<Test1, DbConnection> where = new(_selectQueryBuilder);
            var andOr = where.Between(x => x.Id, 1, 3).AndBetween(x => x.IsTest, true, false);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_add_the_equality_query_with_or2()
        {
            SelectWhere<Test1, DbConnection> where = new(_selectQueryBuilder);
            var andOr = where.Between(x => x.Id, 1, 5).OrBetween(x => x.IsTest, true, false);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_queryBuilder.Statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}
