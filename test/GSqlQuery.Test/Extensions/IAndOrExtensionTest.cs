using GSqlQuery.Queries;
using GSqlQuery.Test.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GSqlQuery.Test.Extensions
{
    public class IAndOrExtensionTest
    {
        private readonly SelectQueryBuilder<Test1> _queryBuilder;

        public IAndOrExtensionTest()
        {
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            _queryBuilder = new SelectQueryBuilder<Test1>(dynamicQuery, new QueryOptions(new DefaultFormats()));
        }

        [Fact]
        public void Should_return_the_criteria()
        {
            AndOrBase<Test1, SelectQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, SelectQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            var andOr = where.Equal(x => x.Id, 1);
            IEnumerable<CriteriaDetailCollection> criterias = where.Create();
            string result = string.Join(" ", criterias.SelectMany(x => x.QueryPart));

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.NotNull(criterias);
            Assert.NotEmpty(criterias);
        }
    }
}