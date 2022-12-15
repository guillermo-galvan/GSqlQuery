using System.Data.Common;
using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Runner.Default;
using GSqlQuery.Runner.Test.Models;
using GSqlQuery.Runner.Queries;

namespace GSqlQuery.Runner.Test.Default
{
    public class DeleteWhereTest
    {
        private readonly Equal<int> _equal;
        private readonly DeleteQueryBuilder<Test1, DbConnection> _deleteQueryBuilder;

        public DeleteWhereTest()
        {
            _equal = new Equal<int>(new TableAttribute("Test1"), new ColumnAttribute("Id"), 1);
            _deleteQueryBuilder = new DeleteQueryBuilder<Test1, DbConnection>(new ConnectionOptions<DbConnection>(new Statements(), LoadFluentOptions.GetDatabaseManagmentMock()));

        }

        [Fact]
        public void Should_add_criteria2()
        {
            DeleteWhere<Test1, DbConnection> query = new(_deleteQueryBuilder);
            Assert.NotNull(query);
            query.Add(_equal);
            Assert.True(true);
        }

        [Fact]
        public void Throw_exception_if_null_ISearchCriteria_is_added2()
        {
            DeleteWhere<Test1, DbConnection> query = new(_deleteQueryBuilder);
            Assert.NotNull(query);
            Assert.Throws<ArgumentNullException>(() => query.Add(null));
        }

        [Fact]
        public void Should_build_the_criteria2()
        {
            DeleteWhere<Test1, DbConnection> query = new(_deleteQueryBuilder);
            Assert.NotNull(query);
            query.Add(_equal);

            var criteria = query.BuildCriteria(new Statements());
            Assert.NotNull(criteria);
            Assert.NotEmpty(criteria);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_with_expression2()
        {
            DeleteWhere<Test1, DbConnection> where = new DeleteWhere<Test1, DbConnection>(_deleteQueryBuilder);
            var andOr = where.GetAndOr(x => x.Id);
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_with_expression2()
        {
            DeleteWhere<Test1, DbConnection> where = null;
            Assert.Throws<ArgumentNullException>(() => where.GetAndOr(x => x.Id));
        }

        [Fact]
        public void Should_validate_of_IAndOr2()
        {
            IAndOr<Test1, DeleteQuery<Test1, DbConnection>> andOr = new DeleteWhere<Test1, DbConnection>(_deleteQueryBuilder);
            try
            {
                andOr.Validate(x => x.IsTest);
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_in_IAndOr2()
        {
            IAndOr<Test1, DeleteQuery<Test1, DbConnection>> where = null;
            Assert.Throws<ArgumentNullException>(() => where.Validate(x => x.Id));
        }

        [Fact]
        public void Should_get_the_IAndOr_interface2()
        {
            DeleteWhere<Test1, DbConnection> where = new DeleteWhere<Test1, DbConnection>(_deleteQueryBuilder);
            var andOr = where.GetAndOr();
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null2()
        {
            DeleteWhere<Test1, DbConnection> where = null;
            Assert.Throws<ArgumentNullException>(() => where.GetAndOr());
        }
    }
}
