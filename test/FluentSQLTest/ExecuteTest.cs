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
        public void Should_create_instance_of_ContinuousExecution()
        {
            var result = Execute.ContinuousExecutionFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()));
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_add_Execution()
        {
            var result = Execute.ContinuousExecutionFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()))
                                .New((c) => _test3.Insert(c));
            Assert.NotNull(result);
            var result2 = result.ContinueWith((c, t) => Test6.Update(c, x => x.IsTests, true));
            Assert.NotNull(result2);
            result2.Start();
        }

        [Fact]
        public void Should_start_execution()
        {
            var result = Execute.ContinuousExecutionFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()))
                                .New((c) => _test3.Insert(c))
                                .ContinueWith((c, t) => Test6.Update(c, x => x.IsTests, true))
                                .ContinueWith((c, t) => Test3.Select(c)).Start();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Should_start_execution_with_Connection()
        {
            var result = Execute.ContinuousExecutionFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()))
                                .New((c) => _test3.Insert(c))
                                .ContinueWith((c, t) => Test6.Update(c, x => x.IsTests, true))
                                .ContinueWith((c, t) => Test3.Select(c))
                                .Start(LoadFluentOptions.GetDbConnection());

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Should_add_Execution2()
        {
            var result = Execute.ContinuousExecutionFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()))
                                .New((c) => Test3.Select(c));
            Assert.NotNull(result);
            var result2 = result.ContinueWith((c, t) => Test6.Select(c).Where().Equal(t => t.IsTests, true));
            Assert.NotNull(result2);
            var result3 = result2.Start();
            Assert.Empty(result3!);
        }

        [Fact]
        public void Should_start_execution2()
        {
            var result = Execute.ContinuousExecutionFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()))
                                .New((c) => Test3.Select(c).Where().LessThan(x => x.Creates, DateTime.Now))
                                .ContinueWith((c, t) => Test6.Update(c, x => x.IsTests, true))
                                .ContinueWith((c, t) => Test3.Select(c)).Start();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Should_start_execution_with_Connection2()
        {
            var result = Execute.ContinuousExecutionFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()))
                                .New((c) => Test3.Select(c))
                                .ContinueWith((c, t) => Test6.Update(c, x => x.IsTests, true))
                                .ContinueWith((c, t) => Test3.Select(c))
                                .Start(LoadFluentOptions.GetDbConnection());

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Should_add_Execution3()
        {
            var result = Execute.ContinuousExecutionFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()))
                                .New((c) => Test3.Update(c, x => x.IsTests, true));
            Assert.NotNull(result);
            var result2 = result.ContinueWith((c, t) => Test6.Select(c).Where().Equal(t => t.IsTests, true));
            Assert.NotNull(result2);
            result2.Start();
        }

        [Fact]
        public void Should_start_execution3()
        {
            var result = Execute.ContinuousExecutionFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()))
                                .New((c) => Test3.Update(c, x => x.IsTests, true).Where().IsNotNull(x => x.Creates))
                                .ContinueWith((c, t) => Test6.Update(c, x => x.IsTests, true))
                                .ContinueWith((c, t) => Test3.Select(c)).Start();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Should_start_execution_with_Connection3()
        {
            var result = Execute.ContinuousExecutionFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()))
                                .New((c) => Test3.Update(c, x => x.IsTests, true).Where().IsNotNull(x => x.Creates))
                                .ContinueWith((c, t) => Test6.Update(c, x => x.IsTests, true))
                                .ContinueWith((c, t) => Test3.Delete(c))
                                .ContinueWith((c, t) => Test3.Select(c))
                                .Start(LoadFluentOptions.GetDbConnection());

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Should_start_execution_with_Connection4()
        {
            var result = Execute.ContinuousExecutionFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()))
                                .New((c) => Test3.Select(c, x => x.Ids).Count())
                                .ContinueWith((c, t) => Test6.Update(c, x => x.IsTests, true))
                                .ContinueWith((c, t) => Test3.Delete(c))
                                .ContinueWith((c, t) => Test3.Select(c))
                                .Start(LoadFluentOptions.GetDbConnection());

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Should_add_Execution4()
        {
            var result = Execute.ContinuousExecutionFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()))
                                .New((c) => Test3.Select(c, x => x.Ids).Count());
            Assert.NotNull(result);
            var result2 = result.ContinueWith((c, t) => Test6.Select(c).Where().Equal(t => t.IsTests, true));
            Assert.NotNull(result2);
            result2.Start();
        }

        [Fact]
        public void Should_start_execution5()
        {
            var result = Execute.ContinuousExecutionFactory(new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock()))
                                .New((c) => Test3.Select(c, x => x.Ids).Count().Where().IsNotNull(x => x.Creates))
                                .ContinueWith((c, t) => Test6.Update(c, x => x.IsTests, true))
                                .ContinueWith((c, t) => Test3.Select(c)).Start();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Throw_exception_if_parameter_is_null()
        {
            IDatabaseManagement<DbConnection> databaseManagement = null;
            Assert.Throws<ArgumentNullException>(() => Execute.ContinuousExecutionFactory(new ConnectionOptions<DbConnection>(null, LoadFluentOptions.GetDatabaseManagmentMock())));
            Assert.Throws<ArgumentNullException>(() => Execute.ContinuousExecutionFactory(new ConnectionOptions<DbConnection>(_statements, null)));
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
