using GSqlQuery.Default;
using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Models;

namespace GSqlQuery.Test.Extensions
{
    public class IAndOrExtensionTest
    {
        private readonly SelectQueryBuilder<Test1> _queryBuilder;

        public IAndOrExtensionTest()
        {
            _queryBuilder = new(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
               new GSqlQuery.Default.Statements());
        }

        [Fact]
        public void Should_return_the_criteria()
        {
            SelectWhere<Test1> where = new(_queryBuilder);
            IEnumerable<CriteriaDetail>? criterias = null;
            var andOr = where.Equal(x => x.Id, 1);
            string result = andOr.GetCliteria(_queryBuilder.Statements, ref criterias);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.NotNull(criterias);
            Assert.NotEmpty(criterias);
        }
    }
}
