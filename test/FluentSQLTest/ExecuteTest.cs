using FluentSQL;
using FluentSQL.Models;
using FluentSQLTest.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest
{
    public class ExecuteTest
    {
        private readonly ConnectionOptions _connectionOptions;
        private readonly Test3 _test3;
        public ExecuteTest()
        {
            _connectionOptions = new ConnectionOptions(new FluentSQL.Default.Statements(), LoadFluentOptions.GetDatabaseManagmentMock());
            _test3 = new Test3(1, null, DateTime.Now, true);
        }

        [Fact]
        public void Should_create_instance_of_ContinuousExecution()
        {
            var result = Execute.ContinuousExecutionFactory(_connectionOptions);
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_add_Execution()
        {
            var result = Execute.ContinuousExecutionFactory(_connectionOptions).New((c) => _test3.Insert(c));
            Assert.NotNull(result);
            var result2 = result.ContinueWith((c, t) => Test6.Update(c, x => x.IsTests, true).Build());
            Assert.NotNull(result2);
        }

        [Fact]
        public void Should_start_execution()
        {
            var result = Execute.ContinuousExecutionFactory(_connectionOptions).New((c) => _test3.Insert(c))
                                .ContinueWith((c, t) => Test6.Update(c, x => x.IsTests, true).Build())
                                .ContinueWith((c, t) => Test3.Select(c).Build()).Start();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Should_start_execution_with_Connection()
        {
            var result = Execute.ContinuousExecutionFactory(_connectionOptions).New((c) => _test3.Insert(c))
                                .ContinueWith((c, t) => Test6.Update(c, x => x.IsTests, true).Build())
                                .ContinueWith((c, t) => Test3.Select(c).Build()).Start(LoadFluentOptions.GetDbConnection());

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Should_start_execution_with_transaction()
        {
            var result = Execute.ContinuousExecutionFactory(_connectionOptions).New((c) => _test3.Insert(c))
                                .ContinueWith((c, t) => Test6.Update(c, x => x.IsTests, true).Build())
                                .ContinueWith((c, t) => Test3.Select(c).Build()).StartWithTransaction();

            Assert.Null(result);
        }

        [Fact]
        public void Throw_exception_if_parameter_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => Execute.ContinuousExecutionFactory(null));
        }

        [Fact]
        public void Should_create_instance_of_BatchExecute()
        {
            var result = Execute.BatchExecuteFactory(_connectionOptions);
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_add_bacth()
        {
            var result = Execute.BatchExecuteFactory(_connectionOptions).Add((c) => _test3.Insert(c));
            Assert.NotNull(result);
            var result2 = result.Add((c) => Test6.Update(c, x => x.IsTests, true).Build());
            Assert.NotNull(result2);
        }

        [Fact]
        public void Should_execution()
        {
            var result = Execute.BatchExecuteFactory(_connectionOptions).Add((c) => _test3.Insert(c))
                                .Add((c) => Test6.Update(c, x => x.IsTests, true).Build())
                                .Add((c) => Test3.Select(c).Build()).Exec();

            Assert.NotNull(result);
            Assert.Equal(0,result);
        }

        [Fact]
        public void Should_execution_with_Connection()
        {
            var result = Execute.BatchExecuteFactory(_connectionOptions).Add((c) => _test3.Insert(c))
                                .Add((c) => Test6.Update(c, x => x.IsTests, true).Build())
                                .Add((c) => Test3.Select(c).Build()).Exec(LoadFluentOptions.GetDbConnection());

            Assert.NotNull(result);
            Assert.Equal(0, result);
        }

        [Fact]
        public void Should_execution_with_transaction()
        {
            var result = Execute.BatchExecuteFactory(_connectionOptions).Add((c) => _test3.Insert(c))
                                .Add((c) => Test6.Update(c, x => x.IsTests, true).Build())
                                .Add((c) => Test3.Select(c).Build()).ExecWithTransaction();

            Assert.Null(result);
        }

        [Fact]
        public void Throw_exception_if_parameter_is_null2()
        {
            Assert.Throws<ArgumentNullException>(() => Execute.BatchExecuteFactory(null));
        }
    }
}
