using GSqlQuery.Extensions;
using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using GSqlQuery.SearchCriteria;
using Microsoft.SqlServer.Server;
using System;
using System.Data;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class DeleteWhereTest
    {
        private readonly Data.SearchCriteria _equal;
        private readonly DeleteQueryBuilder<Test1, IDbConnection> _deleteQueryBuilder;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;

        public DeleteWhereTest()
        {
            _connectionOptions = new ConnectionOptions<IDbConnection>(new TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock());
            _equal = new Data.SearchCriteria(new DefaultFormats(), new TableAttribute("name"), new ColumnAttribute("column"));
            _deleteQueryBuilder = new DeleteQueryBuilder<Test1, IDbConnection>(_connectionOptions);

        }

        [Fact]
        public void Should_add_criteria2()
        {
            AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query =
                new AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_deleteQueryBuilder, _connectionOptions);
            Assert.NotNull(query);
            query.Add(_equal);
            Assert.True(true);
        }

        [Fact]
        public void Throw_exception_if_null_ISearchCriteria_is_added2()
        {
            AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query =
                new AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_deleteQueryBuilder, _connectionOptions);
            Assert.NotNull(query);
            Assert.Throws<ArgumentNullException>(() => query.Add(null));
        }

        [Fact]
        public void Should_build_the_criteria2()
        {
            AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query =
                new AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_deleteQueryBuilder, _connectionOptions);
            Assert.NotNull(query);
            query.Add(_equal);

            var criteria = query.BuildCriteria();
            Assert.NotNull(criteria);
            Assert.NotEmpty(criteria);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_with_expression2()
        {
            AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where =
                new AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_deleteQueryBuilder, _connectionOptions);
            var andOr = GSqlQueryExtension.GetAndOr(where, x => x.Id);
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_with_expression2()
        {
            AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.GetAndOr(where, x => x.Id));
        }

        [Fact]
        public void Should_validate_of_IAndOr2()
        {
            IAndOr<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> andOr = new AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_deleteQueryBuilder, _connectionOptions);
            try
            {
                GSqlQueryExtension.Validate(andOr, x => x.IsTest);
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
            IAndOr<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.Validate(where, x => x.Id));
        }

        [Fact]
        public void Should_get_the_IAndOr_interface2()
        {
            AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = new AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_deleteQueryBuilder, _connectionOptions);
            var andOr = GSqlQueryExtension.GetAndOr(where);
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null2()
        {
            AndOrBase<Test1, DeleteQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.GetAndOr(where));
        }
    }
}