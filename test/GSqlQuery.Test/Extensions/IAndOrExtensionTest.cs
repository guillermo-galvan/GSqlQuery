using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using GSqlQuery.Test.Models;
using System.Collections.Generic;
using Xunit;

namespace GSqlQuery.Test.Extensions
{
    public class IAndOrExtensionTest
    {
        private readonly SelectQueryBuilder<Test1> _queryBuilder;

        public IAndOrExtensionTest()
        {
            _queryBuilder = new SelectQueryBuilder<Test1>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
               new Statements());
        }

        [Fact]
        public void Should_return_the_criteria()
        {
            AndOrBase<Test1, SelectQuery<Test1>> where = new AndOrBase<Test1, SelectQuery<Test1>>(_queryBuilder);
            IEnumerable<CriteriaDetail> criterias = null;
            var andOr = where.Equal(x => x.Id, 1);
            string result = andOr.GetCliteria(_queryBuilder.Statements, ref criterias);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.NotNull(criterias);
            Assert.NotEmpty(criterias);
        }
    }
}
