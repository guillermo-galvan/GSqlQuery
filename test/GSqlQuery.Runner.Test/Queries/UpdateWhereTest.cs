using GSqlQuery.Extensions;
using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using GSqlQuery.SearchCriteria;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class UpdateWhereTest
    {
        private readonly Equal<Test1, int> _equal;
        private readonly UpdateQueryBuilder<Test1, IDbConnection> _updateQueryBuilder;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;

        public UpdateWhereTest()
        {
            _connectionOptions = new ConnectionOptions<IDbConnection>(new TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock());
            var classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            Expression<Func<Test1, int>> expression = (x) => x.Id;
            _equal = new Equal<Test1, int>(classOptions, new DefaultFormats(), 1, null, ref expression);
            _updateQueryBuilder = new UpdateQueryBuilder<Test1, IDbConnection>(_connectionOptions, expression, string.Empty);
        }

        [Fact]
        public void Should_add_criteria_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query = new AndOrBase<Test1, UpdateQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_updateQueryBuilder, _connectionOptions);
            Assert.NotNull(query);
            query.Add(_equal);
            Assert.True(true);
        }

        [Fact]
        public void Throw_exception_if_null_ISearchCriteria_is_added_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query = new AndOrBase<Test1, UpdateQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_updateQueryBuilder, _connectionOptions);
            Assert.NotNull(query);
            Assert.Throws<ArgumentNullException>(() => query.Add(null));
        }

        [Fact]
        public void Should_build_the_criteria_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query = new AndOrBase<Test1, UpdateQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_updateQueryBuilder, _connectionOptions);
            Assert.NotNull(query);
            query.Add(_equal);

            var criteria = query.Create();
            Assert.NotNull(criteria);
            Assert.NotEmpty(criteria);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_with_expression_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = new AndOrBase<Test1, UpdateQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_updateQueryBuilder, _connectionOptions);
            var andOr = where.AndOr;
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_with_expression_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.Validate(where, x => x.Id));
        }

        [Fact]
        public void Should_validate_of_IAndOr_UpdateQuery()
        {
            var andOr = new AndOrBase<Test1, UpdateQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_updateQueryBuilder, _connectionOptions);
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
        public void Throw_exception_if_expression_is_null_in_IAndOr_UpdateQuery()
        {
            IAndOr<Test1, UpdateQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.Validate(where, x => x.Id));
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = new AndOrBase<Test1, UpdateQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_updateQueryBuilder, _connectionOptions);
            var andOr = where.AndOr;
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.Validate(where, x => x.Id));
        }
    }
}