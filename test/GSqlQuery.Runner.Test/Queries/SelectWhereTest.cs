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
    public class SelectWhereTest
    {
        private readonly Equal<int> _equal;
        private readonly SelectQueryBuilder<Test1, IDbConnection> _selectQueryBuilder;

        public SelectWhereTest()
        {
            _equal = new Equal<int>(new TableAttribute("Test1"), new ColumnAttribute("Id"), 1);
            _selectQueryBuilder = new SelectQueryBuilder<Test1, IDbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new ConnectionOptions<IDbConnection>(new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()));
        }

        [Fact]
        public void Should_add_criteria_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query = new AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_selectQueryBuilder);
            Assert.NotNull(query);
            query.Add(_equal);
            Assert.True(true);
        }

        [Fact]
        public void Throw_exception_if_null_ISearchCriteria_is_added_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query = new AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_selectQueryBuilder);
            Assert.NotNull(query);
            Assert.Throws<ArgumentNullException>(() => query.Add(null));
        }

        [Fact]
        public void Should_build_the_criteria_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> query = new AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_selectQueryBuilder);
            Assert.NotNull(query);
            query.Add(_equal);

            var criteria = query.BuildCriteria(_selectQueryBuilder.Options.Statements);
            Assert.NotNull(criteria);
            Assert.NotEmpty(criteria);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_with_expression_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = new AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_selectQueryBuilder);
            var andOr = where.GetAndOr(x => x.Id);
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_with_expression_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = null;
            Assert.Throws<ArgumentNullException>(() => where.GetAndOr(x => x.Id));
        }

        [Fact]
        public void Should_validate_of_IAndOr_SelectQuery()
        {
            var andOr = new AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_selectQueryBuilder);
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
        public void Throw_exception_if_expression_is_null_in_IAndOr_SelectQuery()
        {
            IAndOr<Test1, SelectQuery<Test1, IDbConnection>> andOr = null;
            Assert.Throws<ArgumentNullException>(() => andOr.Validate(x => x.Id));
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>> where = new AndOrBase<Test1, SelectQuery<Test1, IDbConnection>, ConnectionOptions<IDbConnection>>(_selectQueryBuilder);
            IAndOr<Test1, SelectQuery<Test1, IDbConnection>> andOr = where.GetAndOr();
            Assert.NotNull(andOr);
        }
    }
}