using FluentSQL.DatabaseManagement.Default;
using FluentSQL.DatabaseManagement.Extensions;
using FluentSQL.DatabaseManagement.Models;
using FluentSQL.DatabaseManagementTest.Models;
using FluentSQL.Extensions;
using System.Data.Common;

namespace FluentSQL.DatabaseManagementTest.Default
{
    public class CountQueryBuilderTest
    {
        private readonly IStatements _stantements;
        private readonly ConnectionOptions<DbConnection> _connectionOptions;

        public CountQueryBuilderTest()
        {
            _stantements = new FluentSQL.Default.Statements();
            _connectionOptions = new ConnectionOptions<DbConnection>(_stantements, LoadFluentOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Should_return_an_count_query2()
        {
            SelectQueryBuilder<Test1, DbConnection> queryBuilder = new(new List<string> { nameof(Test1.Id) },
                _connectionOptions);
            var result = queryBuilder.Count();
            var query = result.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.DatabaseManagment);
            Assert.Null(query.Criteria);
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            SelectQueryBuilder<Test1, DbConnection> queryBuilder = new(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);

            var result = queryBuilder.Count();

            Assert.NotNull(result);
            Assert.NotNull(result.ConnectionOptions);
            Assert.NotNull(result.ConnectionOptions.Statements);
            Assert.NotNull(result.ConnectionOptions.DatabaseManagment);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
            Assert.Equal(queryBuilder.Columns.Count(), result.Columns.Count());
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters2()
        {
            SelectQueryBuilder<Test1, DbConnection> queryBuilder = null;
            Assert.Throws<ArgumentNullException>(() => queryBuilder.Count());
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface2()
        {
            SelectQueryBuilder<Test1, DbConnection> queryBuilder = new(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);
            var result = queryBuilder.Count();
            var where = result.Where();
            Assert.NotNull(where);
        }
    }
}
