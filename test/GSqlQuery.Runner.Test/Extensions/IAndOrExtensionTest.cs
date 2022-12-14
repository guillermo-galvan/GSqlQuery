using GSqlQuery.Extensions;
using GSqlQuery.Runner.Default;
using GSqlQuery.Runner.Models;
using GSqlQuery.Runner.Test.Models;
using GSqlQuery.SearchCriteria;
using System.Data.Common;

namespace GSqlQuery.Runner.Test.Extensions
{
    public class IAndOrExtensionTest
    {
        private readonly SelectQueryBuilder<Test1, DbConnection> _selectQueryBuilder;

        public IAndOrExtensionTest()
        {
            _selectQueryBuilder = new SelectQueryBuilder<Test1, DbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new ConnectionOptions<DbConnection>(new Statements(), LoadFluentOptions.GetDatabaseManagmentMock()));
        }

        [Fact]
        public void Should_return_the_criteria2()
        {
            SelectWhere<Test1, DbConnection> where = new(_selectQueryBuilder);
            IEnumerable<CriteriaDetail>? criterias = null;
            var andOr = where.Equal(x => x.Id, 1);
            string result = andOr.GetCliteria(new Statements(), ref criterias);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.NotNull(criterias);
            Assert.NotEmpty(criterias);
        }
    }
}
