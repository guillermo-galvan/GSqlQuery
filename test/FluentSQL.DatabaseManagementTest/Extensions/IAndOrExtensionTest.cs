using FluentSQL.DatabaseManagement.Default;
using FluentSQL.DatabaseManagement.Models;
using FluentSQL.DatabaseManagementTest.Models;
using FluentSQL.Extensions;
using FluentSQL.SearchCriteria;
using System.Data.Common;

namespace FluentSQL.DatabaseManagementTest.Extensions
{
    public class IAndOrExtensionTest
    {
        private readonly SelectQueryBuilder<Test1, DbConnection> _selectQueryBuilder;

        public IAndOrExtensionTest()
        {
            _selectQueryBuilder = new SelectQueryBuilder<Test1, DbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new ConnectionOptions<DbConnection>(new FluentSQL.Default.Statements(), LoadFluentOptions.GetDatabaseManagmentMock()));
        }

        [Fact]
        public void Should_return_the_criteria2()
        {
            SelectWhere<Test1, DbConnection> where = new(_selectQueryBuilder);
            IEnumerable<CriteriaDetail>? criterias = null;
            var andOr = where.Equal(x => x.Id, 1);
            string result = andOr.GetCliteria(new FluentSQL.Default.Statements(), ref criterias);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.NotNull(criterias);
            Assert.NotEmpty(criterias);
        }
    }
}
