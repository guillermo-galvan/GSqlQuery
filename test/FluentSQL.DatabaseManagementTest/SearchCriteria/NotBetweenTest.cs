using FluentSQL.DatabaseManagement.Default;
using FluentSQL.DatabaseManagement.Models;
using FluentSQL.DatabaseManagementTest.Models;
using FluentSQL.SearchCriteria;
using System.Data.Common;

namespace FluentSQL.DatabaseManagementTest.SearchCriteria
{
    public class NotBetweenTest
    {
        private readonly IStatements _statements;
        private readonly SelectQueryBuilder<Test1, DbConnection> _selectQueryBuilder;

        public NotBetweenTest()
        {
            _statements = new FluentSQL.Default.Statements();
            _selectQueryBuilder = new(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()));
        }

        [Fact]
        public void Should_add_the_Between_query2()
        {
            SelectWhere<Test1, DbConnection> where = new(_selectQueryBuilder);
            var andOr = where.NotBetween(x => x.Id, 1, 2);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_add_the_equality_query_with_and2()
        {
            SelectWhere<Test1, DbConnection> where = new(_selectQueryBuilder);
            var andOr = where.NotBetween(x => x.Id, 1, 3).AndNotBetween(x => x.IsTest, true, false);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_add_the_equality_query_with_or2()
        {
            SelectWhere<Test1, DbConnection> where = new(_selectQueryBuilder);
            var andOr = where.NotBetween(x => x.Id, 1, 5).OrNotBetween(x => x.IsTest, true, false);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}
