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

namespace FluentSQLTest.Extensions
{
    public class IAndOrExtensionTest
    {
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly SelectQueryBuilder<Test1, DbConnection> _selectQueryBuilder;

        public IAndOrExtensionTest()
        {
            _queryBuilder = new(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
               new FluentSQL.Default.Statements());

            _selectQueryBuilder = new SelectQueryBuilder<Test1, DbConnection>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new ConnectionOptions<DbConnection>(new FluentSQL.Default.Statements(), LoadFluentOptions.GetDatabaseManagmentMock()));
        }

        [Fact]
        public void Should_return_the_criteria()
        {
            SelectWhere<Test1> where = new(_queryBuilder);
            IEnumerable<CriteriaDetail>? criterias = null;
            var andOr = where.Equal(x => x.Id,1);
            string result = andOr.GetCliteria(_queryBuilder.Statements, ref criterias);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.NotNull(criterias);
            Assert.NotEmpty(criterias);
        }

        [Fact]
        public void Should_return_the_criteria2()
        {
            SelectWhere<Test1,DbConnection> where = new(_selectQueryBuilder);
            IEnumerable<CriteriaDetail>? criterias = null;
            var andOr = where.Equal(x => x.Id, 1);
            string result = andOr.GetCliteria(_queryBuilder.Statements, ref criterias);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.NotNull(criterias);
            Assert.NotEmpty(criterias);
        }
    }
}
