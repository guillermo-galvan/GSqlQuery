using GSqlQuery.Queries;
using GSqlQuery.Test.Models;
using System;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class DeleteQueryBuilderTest
    {
        private readonly IFormats _statements;

        public DeleteQueryBuilderTest()
        {
            _statements = new DefaultFormats();
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            DeleteQueryBuilder<Test1> queryBuilder = new DeleteQueryBuilder<Test1>(_statements);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.Options);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new DeleteQueryBuilder<Test1>(null));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface()
        {
            DeleteQueryBuilder<Test1> queryBuilder = new DeleteQueryBuilder<Test1>(_statements);
            IWhere<Test1, DeleteQuery<Test1>> where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_delete_query()
        {
            DeleteQueryBuilder<Test1> queryBuilder = new DeleteQueryBuilder<Test1>(_statements);
            IQuery<Test1> query = queryBuilder.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.Criteria);
            Assert.Empty(query.Criteria);
        }
    }
}