using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Models;
using FluentSQL.SearchCriteria;
using FluentSQLTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest.Default
{
    public class SelectWhereTest
    {
        private readonly Equal<int> _equal;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly SelectQueryBuilder<Test1, DbConnection> _selectQueryBuilder;

        public SelectWhereTest()
        {
            _equal = new Equal<int>(new TableAttribute("Test1"), new ColumnAttribute("Id"), 1);
            _queryBuilder = new(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
               new FluentSQL.Default.Statements());

            _selectQueryBuilder = new SelectQueryBuilder<Test1, DbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new ConnectionOptions<DbConnection>(new FluentSQL.Default.Statements(), LoadFluentOptions.GetDatabaseManagmentMock()));
        }

        [Fact]
        public void Should_add_criteria()
        {
            SelectWhere<Test1> query = new(_queryBuilder);
            Assert.NotNull(query);
            query.Add(_equal);
            Assert.True(true);
        }

        [Fact]
        public void Throw_exception_if_null_ISearchCriteria_is_added()
        {
            SelectWhere<Test1> query = new(_queryBuilder);
            Assert.NotNull(query);
            Assert.Throws<ArgumentNullException>(() => query.Add(null));
        }

        [Fact]
        public void Should_build_the_criteria()
        {
            SelectWhere<Test1> query = new(_queryBuilder);
            Assert.NotNull(query);
            query.Add(_equal);

            var criteria = ((ISearchCriteriaBuilder)query).BuildCriteria(_queryBuilder.Statements);
            Assert.NotNull(criteria);
            Assert.NotEmpty(criteria);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_with_expression()
        {
            SelectWhere<Test1> where = new (_queryBuilder);
            IAndOr<SelectQuery<Test1>> andOr = where.GetAndOr(x => x.Id);
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_with_expression()
        {
            SelectWhere<Test1> where = null;
            Assert.Throws<ArgumentNullException>(() => where.GetAndOr(x => x.Id));
        }

        [Fact]
        public void Should_validate_of_IAndOr()
        {
            IAndOr<Test1, SelectQuery<Test1>> andOr = new SelectWhere<Test1>(_queryBuilder);
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
        public void Throw_exception_if_expression_is_null_in_IAndOr()
        {
            IAndOr<Test1, SelectQuery<Test1>> where = null;
            Assert.Throws<ArgumentNullException>(() => where.Validate(x => x.Id));
        }

        [Fact]
        public void Should_get_the_IAndOr_interface()
        {
            SelectWhere<Test1> where = new (_queryBuilder);
            IAndOr<SelectQuery<Test1>> andOr = where.GetAndOr();
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null()
        {
            SelectWhere<Test1> where = null;
            Assert.Throws<ArgumentNullException>(() => where.GetAndOr());
        }


        [Fact]
        public void Should_add_criteria2()
        {
            SelectWhere<Test1,DbConnection> query = new(_selectQueryBuilder);
            Assert.NotNull(query);
            query.Add(_equal);
            Assert.True(true);
        }

        [Fact]
        public void Throw_exception_if_null_ISearchCriteria_is_added2()
        {
            SelectWhere<Test1,DbConnection> query = new(_selectQueryBuilder);
            Assert.NotNull(query);
            Assert.Throws<ArgumentNullException>(() => query.Add(null));
        }

        [Fact]
        public void Should_build_the_criteria2()
        {
            SelectWhere<Test1,DbConnection> query = new(_selectQueryBuilder);
            Assert.NotNull(query);
            query.Add(_equal);

            var criteria = ((ISearchCriteriaBuilder)query).BuildCriteria(_selectQueryBuilder.ConnectionOptions.Statements);
            Assert.NotNull(criteria);
            Assert.NotEmpty(criteria);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_with_expression2()
        {
            SelectWhere<Test1,DbConnection> where = new(_selectQueryBuilder);            
            var andOr = where.GetAndOr(x => x.Id);
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_with_expression2()
        {
            SelectWhere<Test1,DbConnection> where = null;
            Assert.Throws<ArgumentNullException>(() => where.GetAndOr(x => x.Id));
        }

        [Fact]
        public void Should_validate_of_IAndOr2()
        {
            var andOr = new SelectWhere<Test1,DbConnection>(_selectQueryBuilder);
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
            IAndOr<Test1,SelectQuery<Test1, DbConnection>> andOr = null;
            Assert.Throws<ArgumentNullException>(() => andOr.Validate(x => x.Id));
        }

        [Fact]
        public void Should_get_the_IAndOr_interface2()
        {
            SelectWhere<Test1,DbConnection> where = new(_selectQueryBuilder);
            IAndOr<Test1,SelectQuery<Test1, DbConnection>> andOr = where.GetAndOr(); 
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null2()
        {
            SelectWhere<Test1> where = null;
            Assert.Throws<ArgumentNullException>(() => where.GetAndOr());
        }
    }
}
