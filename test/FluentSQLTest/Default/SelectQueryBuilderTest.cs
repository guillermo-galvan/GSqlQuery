using FluentSQL.Default;
using FluentSQLTest.Models;

namespace FluentSQLTest.Default
{
    public class SelectQueryBuilderTest
    {
        private readonly IStatements _stantements;

        public SelectQueryBuilderTest()
        {
            _stantements = new FluentSQL.Default.Statements();
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            SelectQueryBuilder<Test1> queryBuilder = new(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _stantements);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.Statements);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new SelectQueryBuilder<Test1>(null, _stantements));
            Assert.Throws<ArgumentNullException>(() => new SelectQueryBuilder<Test1>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, null));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface()
        {
            SelectQueryBuilder<Test1> queryBuilder = new(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _stantements);
            IWhere<Test1, SelectQuery<Test1>> where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_delete_query()
        {
            SelectQueryBuilder<Test1> queryBuilder = new(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _stantements);
            IQuery<Test1> query = queryBuilder.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Statements);
            Assert.Null(query.Criteria);
        }
    }
}
