using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Xunit;

namespace GSqlQuery.Runner.Test.SearchCriteria
{
    public class NotInTest
    {
        private readonly IStatements _statements;
        private readonly SelectQueryBuilder<Test1, DbConnection> _selectQueryBuilder;

        public NotInTest()
        {
            _statements = new Statements();
            _selectQueryBuilder = new SelectQueryBuilder<Test1, DbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()));
        }

        [Fact]
        public void Should_add_the_equality_query2()
        {
            SelectWhere<Test1, DbConnection> where = new SelectWhere<Test1, DbConnection>(_selectQueryBuilder);
            var andOr = where.NotIn(x => x.Id, new int[] { 4, 5, 6, 7, 8, 9, 10 });
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
            var andOr = where.NotIn(x => x.Id, new int[] { 4, 5, 6, 7, 8, 9, 10 }).AndNotIn(x => x.Id, new int[] { 4, 5, 6, 7, 8, 9, 10 });
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_add_the_equality_query_with_or2()
        {
            SelectWhere<Test1, DbConnection> where = new SelectWhere<Test1, DbConnection>(_selectQueryBuilder);
            var andOr = where.NotIn(x => x.Id, new int[] { 4, 5, 6, 7, 8, 9, 10 }).OrNotIn(x => x.Id, new int[] { 4, 5, 6, 7, 8, 9, 10 });
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}
