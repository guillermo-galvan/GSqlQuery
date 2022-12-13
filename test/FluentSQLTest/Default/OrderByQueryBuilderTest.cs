using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.SearchCriteria;
using FluentSQLTest.Models;

namespace FluentSQLTest.Default
{
    public class OrderByQueryBuilderTest
    {
        private readonly IStatements _stantements;

        public OrderByQueryBuilderTest()
        {
            _stantements = new FluentSQL.Default.Statements();
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            SelectQueryBuilder<Test1> queryBuilder = new(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _stantements);

            var result = queryBuilder.OrderBy(x => x.Id, OrderBy.ASC);

            Assert.NotNull(result);
            Assert.NotNull(result.Statements);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
            Assert.Equal(queryBuilder.Columns.Count(), result.Columns.Count());
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            SelectQueryBuilder<Test1> queryBuilder = null;
            Assert.Throws<ArgumentNullException>(() => queryBuilder.OrderBy(x => x.Id, OrderBy.ASC));
        }

        [Fact]
        public void Should_return_an_orderBy_query()
        {
            SelectQueryBuilder<Test1> queryBuilder = new(new List<string> { nameof(Test1.Id) },
                _stantements);
            var result = queryBuilder.OrderBy(x => x.Id, OrderBy.ASC).OrderBy(x => new { x.Name, x.Create}, OrderBy.DESC);
            IQuery<Test1> query = result.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Statements);
            Assert.Null(query.Criteria);
        }

        [Fact]
        public void Should_return_an_orderBy_query_with_where()
        {
            var queryBuilder = new SelectQueryBuilder<Test1>(new List<string> { nameof(Test1.Id) }, _stantements)
                                   .Where().Equal(x => x.IsTest,true).OrEqual(x => x.IsTest,  false);
            var result = queryBuilder.OrderBy(x => x.Id, OrderBy.ASC).OrderBy(x => new { x.Name, x.Create }, OrderBy.DESC);
            IQuery<Test1> query = result.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.Criteria);
        }
    }
}
