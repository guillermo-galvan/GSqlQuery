using FluentSQL.DatabaseManagement;
using FluentSQL.DatabaseManagement.Models;
using System.Data.Common;

namespace FluentSQL.DatabaseManagementTest.Models
{
    public class ConnectionOptionsTest
    {
        [Fact]
        public void Properties_cannot_be_null()
        {
            var connectionOptions = new ConnectionOptions<DbConnection>(new Statements(), LoadFluentOptions.GetDatabaseManagmentMock());

            Assert.NotNull(connectionOptions.DatabaseManagment);
            Assert.NotNull(connectionOptions.Statements);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            IDatabaseManagement<DbConnection> databaseManagement = null;
            Assert.Throws<ArgumentNullException>(() => new ConnectionOptions<DbConnection>(null, LoadFluentOptions.GetDatabaseManagmentMock()));
            Assert.Throws<ArgumentNullException>(() => new ConnectionOptions<DbConnection>(new Statements(), databaseManagement));
        }
    }
}
