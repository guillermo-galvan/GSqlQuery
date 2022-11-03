using FluentSQL;
using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.Models;
using FluentSQL.SearchCriteria;
using FluentSQLTest.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest
{
    public class ExecuteTest
    {
        private readonly IStatements _statements;
        private readonly Test3 _test3;
        public ExecuteTest()
        {
            _statements = new FluentSQL.Default.Statements();
            _test3 = new Test3(1, null, DateTime.Now, true);
        }

        [Fact]
        public void Should_create_instance_of_BatchExecute()
        {
            var result = Execute.BatchExecuteFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()));
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_add_bacth()
        {
            var result = Execute.BatchExecuteFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()));
            Assert.NotNull(result);
            var result2 = result.Add((c) => Test6.Update(c, x => x.IsTests, true).Build());
            Assert.NotNull(result2);
        }

        [Fact]
        public void Should_execution()
        {
            var result = Execute.BatchExecuteFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()))
                                .Add((c) => Test6.Update(c, x => x.IsTests, true).Build())
                                .Add((c) => Test3.Select(c).Build())
                                .Exec();
            Assert.Equal(0, result);
        }

        [Fact]
        public void Should_execution_with_Connection()
        {
            var result = Execute.BatchExecuteFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()))
                                .Add((c) => Test6.Update(c, x => x.IsTests, true).Build())
                                .Add((c) => Test3.Select(c).Build())
                                .Exec(LoadFluentOptions.GetDbConnection());
            Assert.Equal(0, result);
        }

        [Fact]
        public void Throw_exception_if_parameter_is_null2()
        {
            IDatabaseManagement<DbConnection> databaseManagement = null;
            Assert.Throws<ArgumentNullException>(() => Execute.BatchExecuteFactory(new ConnectionOptions<DbConnection>(null, LoadFluentOptions.GetDatabaseManagmentMock())));
            Assert.Throws<ArgumentNullException>(() => Execute.BatchExecuteFactory(new ConnectionOptions<DbConnection>(_statements, databaseManagement)));
        }
    }
}
