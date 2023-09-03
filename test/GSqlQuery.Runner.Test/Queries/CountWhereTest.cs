using GSqlQuery.Extensions;
using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using GSqlQuery.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class CountWhereTest
    {
        private readonly Equal<int> _equal;
        private readonly SelectQueryBuilder<Test1, IDbConnection> _selectQueryBuilder;
        private readonly CountQueryBuilder<Test1, IDbConnection> _connectionCountQueryBuilder;
        public CountWhereTest()
        {
            _equal = new Equal<int>(new TableAttribute("Test1"), new ColumnAttribute("Id"), 1);
            _selectQueryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new ConnectionOptions<IDbConnection>(new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()));
            _connectionCountQueryBuilder = new CountQueryBuilder<Test1, IDbConnection>(_selectQueryBuilder);
        }

        [Fact]
        public void Should_add_criteria_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query =
                new AndOrBase<Test1, CountQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_connectionCountQueryBuilder);
            Assert.NotNull(query);
            query.Add(_equal);
            Assert.True(true);
        }

        [Fact]
        public void Throw_exception_if_null_ISearchCriteria_is_added_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query =
                new AndOrBase<Test1, CountQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_connectionCountQueryBuilder);
            Assert.NotNull(query);
            Assert.Throws<ArgumentNullException>(() => query.Add(null));
        }

        [Fact]
        public void Should_build_the_criteria_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query =
                new AndOrBase<Test1, CountQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_connectionCountQueryBuilder);
            Assert.NotNull(query);
            query.Add(_equal);
            var criteria = query.BuildCriteria(new Statements());
            Assert.NotNull(criteria);
            Assert.NotEmpty(criteria);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_with_expression_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = new AndOrBase<Test1, CountQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_connectionCountQueryBuilder);
            var andOr = where.GetAndOr(x => x.Id);
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_with_expression_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = null;
            Assert.Throws<ArgumentNullException>(() => where.GetAndOr(x => x.Id));
        }

        [Fact]
        public void Should_validate_of_IAndOr_CountQuery()
        {
            var andOr = new AndOrBase<Test1, CountQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_connectionCountQueryBuilder);
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
        public void Throw_exception_if_expression_is_null_in_IAndOr_CountQuery()
        {
            IAndOr<Test1, CountQuery<Test1, IDbConnection>> where = null;
            Assert.Throws<ArgumentNullException>(() => where.Validate(x => x.Id));
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where =
                new AndOrBase<Test1, CountQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_connectionCountQueryBuilder);
            var andOr = where.GetAndOr();
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = null;
            Assert.Throws<ArgumentNullException>(() => where.GetAndOr());
        }
    }
}