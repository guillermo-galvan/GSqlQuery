using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using Xunit;

namespace GSqlQuery.Runner.Test.SearchCriteria
{
    public class EqualTest
    {
        private readonly IStatements _statements;
        private readonly SelectQueryBuilder<Test1, DbConnection> _selectQueryBuilder;

        public EqualTest()
        {
            _statements = new Statements();
            _selectQueryBuilder = new SelectQueryBuilder<Test1, DbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()));
        }

        [Fact]
        public void Should_add_the_equality_query2()
        {
            AndOrBase<Test1, SelectQuery<Test1, DbConnection>> where = new AndOrBase<Test1, SelectQuery<Test1, DbConnection>>(_selectQueryBuilder);
            var andOr = where.Equal(x => x.Id, 1);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_add_the_equality_query_with_and2()
        {
            AndOrBase<Test1, SelectQuery<Test1, DbConnection>> where = new AndOrBase<Test1, SelectQuery<Test1, DbConnection>>(_selectQueryBuilder);
            var andOr = where.Equal(x => x.Id, 1).AndEqual(x => x.IsTest, true);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_add_the_equality_query_with_or2()
        {
            AndOrBase<Test1, SelectQuery<Test1, DbConnection>> where = new AndOrBase<Test1, SelectQuery<Test1, DbConnection>>(_selectQueryBuilder);
            var andOr = where.Equal(x => x.Id, 1).OrEqual(x => x.IsTest, true);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria(_statements);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}
