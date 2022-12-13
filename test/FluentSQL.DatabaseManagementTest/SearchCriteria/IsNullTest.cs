using FluentSQL.DatabaseManagement.Default;
using FluentSQL.DatabaseManagement.Models;
using FluentSQL.DatabaseManagementTest.Models;
using FluentSQL.SearchCriteria;
using System.Data.Common;

namespace FluentSQL.DatabaseManagementTest.SearchCriteria
{
    public class IsNullTest
    {
        private readonly IStatements _statements;
        private readonly SelectQueryBuilder<Test1, DbConnection> _selectQueryBuilder;

        public IsNullTest()
        {
            _statements = new FluentSQL.Default.Statements();
            _selectQueryBuilder = new(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()));
        }

        [Fact]
        public void Should_add_the_equality_query2()
        {
            SelectWhere<Test1, DbConnection> where = new(_selectQueryBuilder);
            var andOr = where.IsNull(x => x.Id);
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
            var andOr = where.IsNull(x => x.Id).AndIsNull(x => x.IsTest);
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
            var andOr = where.IsNull(x => x.Id).OrIsNull(x => x.IsTest);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}
