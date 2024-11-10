using GSqlQuery.Extensions;
using GSqlQuery.Runner.Queries;
using GSqlQuery.Runner.Test.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class SelectWhereTest
    {
        private readonly Data.SearchCriteria _equal;
        private readonly SelectQueryBuilder<Test1, IDbConnection> _selectQueryBuilder;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;

        public SelectWhereTest()
        {
            _connectionOptions = new ConnectionOptions<IDbConnection>(new TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock());
            _equal = new Data.SearchCriteria(new DefaultFormats(), new TableAttribute("name"), new ColumnAttribute("column"));
            var columsn = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            _selectQueryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(columsn, _connectionOptions);
        }

        [Fact]
        public void Should_add_criteria_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query = new AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_selectQueryBuilder, _connectionOptions);
            Assert.NotNull(query);
            query.Add(_equal);
            Assert.True(true);
        }

        [Fact]
        public void Throw_exception_if_null_ISearchCriteria_is_added_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query = new AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_selectQueryBuilder, _connectionOptions);
            Assert.NotNull(query);
            Assert.Throws<ArgumentNullException>(() => query.Add(null));
        }

        [Fact]
        public void Should_build_the_criteria_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query = new AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_selectQueryBuilder, _connectionOptions);
            Assert.NotNull(query);
            query.Add(_equal);

            var criteria = query.BuildCriteria();
            Assert.NotNull(criteria);
            Assert.NotEmpty(criteria);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_with_expression_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = new AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_selectQueryBuilder, _connectionOptions);
            var andOr = GSqlQueryExtension.GetAndOr(where, x => x.Id);
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_with_expression_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.GetAndOr(where, x => x.Id));
        }

        [Fact]
        public void Should_validate_of_IAndOr_SelectQuery()
        {
            var andOr = new AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_selectQueryBuilder, _connectionOptions);
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
        public void Throw_exception_if_expression_is_null_in_IAndOr_SelectQuery()
        {
            IAndOr<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> andOr = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.Validate(andOr, x => x.Id));
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = new AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_selectQueryBuilder, _connectionOptions);
            IAndOr<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> andOr = GSqlQueryExtension.GetAndOr(where);
            Assert.NotNull(andOr);
        }
    }
}