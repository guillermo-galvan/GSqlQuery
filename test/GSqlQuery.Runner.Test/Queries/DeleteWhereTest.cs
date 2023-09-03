using GSqlQuery.Extensions;
using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using GSqlQuery.SearchCriteria;
using System;
using System.Data;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class DeleteWhereTest
    {
        private readonly Equal<int> _equal;
        private readonly DeleteQueryBuilder<Test1, IDbConnection> _deleteQueryBuilder;

        public DeleteWhereTest()
        {
            _equal = new Equal<int>(new TableAttribute("Test1"), new ColumnAttribute("Id"), 1);
            _deleteQueryBuilder = new DeleteQueryBuilder<Test1, IDbConnection>(new ConnectionOptions<IDbConnection>(new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()));

        }

        [Fact]
        public void Should_add_criteria2()
        {
            AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query =
                new AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_deleteQueryBuilder);
            Assert.NotNull(query);
            query.Add(_equal);
            Assert.True(true);
        }

        [Fact]
        public void Throw_exception_if_null_ISearchCriteria_is_added2()
        {
            AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query =
                new AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_deleteQueryBuilder);
            Assert.NotNull(query);
            Assert.Throws<ArgumentNullException>(() => query.Add(null));
        }

        [Fact]
        public void Should_build_the_criteria2()
        {
            AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query =
                new AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_deleteQueryBuilder);
            Assert.NotNull(query);
            query.Add(_equal);

            var criteria = query.BuildCriteria(new Statements());
            Assert.NotNull(criteria);
            Assert.NotEmpty(criteria);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_with_expression2()
        {
            AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where =
                new AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_deleteQueryBuilder);
            var andOr = where.GetAndOr(x => x.Id);
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_with_expression2()
        {
            AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = null;
            Assert.Throws<ArgumentNullException>(() => where.GetAndOr(x => x.Id));
        }

        [Fact]
        public void Should_validate_of_IAndOr2()
        {
            IAndOr<Test1, DeleteQuery<Test1, IDbConnection>> andOr = new AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_deleteQueryBuilder);
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
            IAndOr<Test1, DeleteQuery<Test1, IDbConnection>> where = null;
            Assert.Throws<ArgumentNullException>(() => where.Validate(x => x.Id));
        }

        [Fact]
        public void Should_get_the_IAndOr_interface2()
        {
            AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = new AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_deleteQueryBuilder);
            var andOr = where.GetAndOr();
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null2()
        {
            AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = null;
            Assert.Throws<ArgumentNullException>(() => where.GetAndOr());
        }
    }
}