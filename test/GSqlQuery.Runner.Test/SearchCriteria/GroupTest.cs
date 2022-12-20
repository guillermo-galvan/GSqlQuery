using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Extensions;
using GSqlQuery.Runner.Test.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Xunit;

namespace GSqlQuery.Runner.Test.SearchCriteria
{
    public class GroupTest
    {
        private readonly IStatements _statements;
        private readonly SelectQueryBuilder<Test1, DbConnection> _selectQueryBuilder;
        private readonly TableAttribute _tableAttribute;
        private readonly ClassOptions _classOptions;

        public GroupTest()
        {
            _statements = new Statements();
            _selectQueryBuilder = new SelectQueryBuilder<Test1, DbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()));
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _tableAttribute = _classOptions.Table;
        }

        [Fact]
        public void Should_create_an_instance2()
        {
            SelectWhere<Test1, DbConnection> andOr = new SelectWhere<Test1, DbConnection>(_selectQueryBuilder);
            Group<Test1, SelectQuery<Test1, DbConnection>, DbConnection, IEnumerable<Test1>> test = new Group<Test1, SelectQuery<Test1, DbConnection>, DbConnection, IEnumerable<Test1>>(_tableAttribute, null, andOr);

            Assert.NotNull(test);
            Assert.NotNull(test.Table);
            Assert.NotNull(test.Column);
            Assert.NotNull(test.AndOr);
            Assert.Null(test.LogicalOperator);
        }

        [Theory]
        [InlineData("AND")]
        [InlineData("OR")]
        public void Should_create_an_instance_2(string logicalOperator)
        {
            SelectWhere<Test1, DbConnection> andOr = new SelectWhere<Test1, DbConnection>(_selectQueryBuilder);
            Group<Test1, SelectQuery<Test1, DbConnection>, DbConnection, IEnumerable<Test1>> test = new Group<Test1, SelectQuery<Test1, DbConnection>, DbConnection, IEnumerable<Test1>>(_tableAttribute, logicalOperator, andOr);

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
        public void Should_get_criteria_detail2(string logicalOperator, string value, string querypart)
        {
            SelectWhere<Test1, DbConnection> andOr = new SelectWhere<Test1, DbConnection>(_selectQueryBuilder);
            Group<Test1, SelectQuery<Test1, DbConnection>, DbConnection, IEnumerable<Test1>> test = new Group<Test1, SelectQuery<Test1, DbConnection>, DbConnection, IEnumerable<Test1>>(_tableAttribute, logicalOperator, andOr);
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
        public void Should_add_the_equality_query2()
        {
            SelectWhere<Test1, DbConnection> where = new SelectWhere<Test1, DbConnection>(_selectQueryBuilder);
            var andOr = where.BeginGroup().Equal(x => x.Id, 1).CloseGroup();
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_add_the_equality_query_with_and2()
        {
            SelectWhere<Test1, DbConnection> where = new SelectWhere<Test1, DbConnection>(_selectQueryBuilder);
            var andOr = where.BeginGroup().Equal(x => x.Id, 1).AndEqual(x => x.IsTest, true).CloseGroup();
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_add_the_equality_query_with_or2()
        {
            SelectWhere<Test1, DbConnection> where = new SelectWhere<Test1, DbConnection>(_selectQueryBuilder);
            var andOr = where.BeginGroup().Equal(x => x.Id, 1).OrEqual(x => x.IsTest, true).CloseGroup();
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }
    }
}
