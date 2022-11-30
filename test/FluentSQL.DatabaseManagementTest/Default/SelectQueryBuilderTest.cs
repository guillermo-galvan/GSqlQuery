using FluentSQL.DatabaseManagement;
using FluentSQL.DatabaseManagement.Default;
using FluentSQL.DatabaseManagement.Models;
using FluentSQL.DatabaseManagementTest.Models;
using System.Data.Common;

namespace FluentSQL.DatabaseManagementTest.Default
{
    public class SelectQueryBuilderTest
    {
        private readonly IStatements _stantements;
        private readonly ConnectionOptions<DbConnection> _connectionOptions;

        public SelectQueryBuilderTest()
        {
            _stantements = new FluentSQL.Default.Statements();
            _connectionOptions = new ConnectionOptions<DbConnection>(_stantements, LoadFluentOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            SelectQueryBuilder<Test1, DbConnection> queryBuilder = new(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.ConnectionOptions);
            Assert.NotNull(queryBuilder.ConnectionOptions.Statements);
            Assert.NotNull(queryBuilder.ConnectionOptions.DatabaseManagment);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters2()
        {
            Assert.Throws<ArgumentNullException>(() => new SelectQueryBuilder<Test1, DbConnection>(null, _connectionOptions));
            Assert.Throws<ArgumentNullException>(() => new SelectQueryBuilder<Test1, DbConnection>(
                new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, null));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface2()
        {
            SelectQueryBuilder<Test1, DbConnection> queryBuilder = new(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);
            var where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_delete_query2()
        {
            SelectQueryBuilder<Test1, DbConnection> queryBuilder = new(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);
            IQuery<Test1, DbConnection, IEnumerable<Test1>> query = queryBuilder.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.DatabaseManagment);
            Assert.NotNull(query.Statements);
            Assert.Null(query.Criteria);
        }
    }
}
