using System;
using System.Data;
using Xunit;

namespace GSqlQuery.Runner.Test.Models
{
    public class ConnectionOptionsTest
    {
        [Fact]
        public void Properties_cannot_be_null()
        {
            var connectionOptions = new ConnectionOptions<IDbConnection>(new TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock());

            Assert.NotNull(connectionOptions.DatabaseManagement);
            Assert.NotNull(connectionOptions.Formats);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            IDatabaseManagement<IDbConnection> databaseManagement = null;
            Assert.Throws<ArgumentNullException>(() => new ConnectionOptions<IDbConnection>(null, LoadGSqlQueryOptions.GetDatabaseManagmentMock()));
            Assert.Throws<ArgumentNullException>(() => new ConnectionOptions<IDbConnection>(new TestFormats(), databaseManagement));
        }
    }
}