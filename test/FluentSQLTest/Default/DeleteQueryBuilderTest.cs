using FluentSQL.Default;
using FluentSQL.Models;
using FluentSQLTest.Models;

namespace FluentSQLTest.Default
{
    public class DeleteQueryBuilderTest
    {
        private readonly IStatements _statements;

        public DeleteQueryBuilderTest()
        {
            _statements = new FluentSQL.Default.Statements();
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            DeleteQueryBuilder<Test1> queryBuilder = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _statements);

            Assert.NotNull(queryBuilder);            
            Assert.NotNull(queryBuilder.Statements);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new DeleteQueryBuilder<Test1>(null, new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, _statements));
            Assert.Throws<ArgumentNullException>(() => new DeleteQueryBuilder<Test1>(new ClassOptions(typeof(Test1)), null, _statements));
            Assert.Throws<ArgumentNullException>(() => new DeleteQueryBuilder<Test1>(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, null));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface()
        {
            DeleteQueryBuilder<Test1> queryBuilder = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _statements);
            IWhere<Test1, DeleteQuery<Test1>> where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_delete_query()
        {
            DeleteQueryBuilder<Test1> queryBuilder = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _statements);
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
