using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Models;
using FluentSQL.SearchCriteria;
using FluentSQLTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest.Extensions
{
    public class IDatabaseManagmentExtensionTest
    {
        [Fact]
        public void Should_validate_database_managment()
        {
            IDatabaseManagment databaseManagment = LoadFluentOptions.GetDatabaseManagmentMock();
            databaseManagment.ValidateDatabaseManagment();
            Assert.True(true);
        }

        [Fact]
        public void Throw_exception_if_database_management_is_null()
        {
            IDatabaseManagment databaseManagment = null;
            Assert.Throws<ArgumentNullException>(() => databaseManagment.ValidateDatabaseManagment());
            
        }
    }
}
