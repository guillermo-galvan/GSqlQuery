using System;
using System.Data.Common;
using Xunit;

namespace GSqlQuery.Runner.Test.Models
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
