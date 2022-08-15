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
            QueryBuilder<Test1> queryBuilder = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new FluentSQL.Default.Statements(), QueryType.Select);

            Assert.NotNull(queryBuilder);            
            Assert.NotNull(queryBuilder.Statements);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new QueryBuilder<Test1>(null, new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, new FluentSQL.Default.Statements(), QueryType.Select));
            Assert.Throws<ArgumentNullException>(() => new QueryBuilder<Test1>(new ClassOptions(typeof(Test1)), null, new FluentSQL.Default.Statements(), QueryType.Select));
            Assert.Throws<ArgumentNullException>(() => new QueryBuilder<Test1>(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, null, QueryType.Select));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface()
        {
            QueryBuilder<Test1> queryBuilder = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new FluentSQL.Default.Statements(), QueryType.Select);
            IWhere<Test1> where = queryBuilder.Where();
            Assert.NotNull(where);
        }
    }
}
