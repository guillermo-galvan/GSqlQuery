using FluentSQL.Default;
using FluentSQL.Models;
using FluentSQLTest.Models;

namespace FluentSQLTest.Default
{
    public class QueryBuilderTest
    {
        [Fact]
        public void Properties_cannot_be_null()
        {
            QueryBuilder queryBuilder = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new FluentSQL.Default.Statements(), QueryType.Select);

            Assert.NotNull(queryBuilder);            
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
            Assert.NotNull(queryBuilder.Build());
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new QueryBuilder(null, new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, new FluentSQL.Default.Statements(), QueryType.Select));
            Assert.Throws<ArgumentNullException>(() => new QueryBuilder(new ClassOptions(typeof(Test1)), null, new FluentSQL.Default.Statements(), QueryType.Select));
            Assert.Throws<ArgumentNullException>(() => new QueryBuilder(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, null, QueryType.Select));
        }
    }
}
