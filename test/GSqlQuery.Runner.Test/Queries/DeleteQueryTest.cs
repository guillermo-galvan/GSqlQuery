﻿using GSqlQuery.SearchCriteria;
using System.Data.Common;
using GSqlQuery.Runner.Test.Models;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Threading;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class DeleteQueryTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly Equal<int> _equal;
        private readonly IStatements _statements;
        private readonly ClassOptions _classOptions;
        private readonly ConnectionOptions<DbConnection> _connectionOptions;
        private readonly ConnectionOptions<DbConnection> _connectionOptionsAsync;

        public DeleteQueryTest()
        {
            _statements = new Statements();
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _equal = new Equal<int>(_tableAttribute, _columnAttribute, 1);
            _connectionOptions = new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock());
            _connectionOptionsAsync = new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMockAsync());
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            DeleteQuery<Test1, DbConnection> query = new DeleteQuery<Test1, DbConnection>("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions);

            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.Statements);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters2()
        {
            Assert.Throws<ArgumentNullException>(() => new DeleteQuery<Test1, DbConnection>("query", null, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions));
            Assert.Throws<ArgumentNullException>(() => new DeleteQuery<Test1, DbConnection>("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, null));
            Assert.Throws<ArgumentNullException>(() => new DeleteQuery<Test1, DbConnection>(null, new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions));
        }

        [Fact]
        public void Should_execute_the_query()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            DeleteQuery<Test3, DbConnection> query = new DeleteQuery<Test3, DbConnection>("DELETE FROM [TableName];",
                new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptions);
            var result = query.Execute();
            Assert.Equal(1, result);
        }

        [Fact]
        public void Throw_exception_if_DatabaseManagment_not_found()
        {
            DeleteQuery<Test1, DbConnection> query = new DeleteQuery<Test1, DbConnection>("DELETE FROM [TableName];",
                new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) },
                _connectionOptions);
            Assert.Throws<ArgumentNullException>(() => query.Execute(null));
        }

        [Fact]
        public void Should_execute_the_query2()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            DeleteQuery<Test3, DbConnection> query = new DeleteQuery<Test3, DbConnection>("DELETE FROM [TableName];",
                new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptions);
            var result = query.Execute(LoadFluentOptions.GetDbConnection());
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Should_executeAsync_the_query()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            DeleteQuery<Test3, DbConnection> query = new DeleteQuery<Test3, DbConnection>("DELETE FROM [TableName];",
                new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptionsAsync);
            var result = await query.ExecuteAsync(CancellationToken.None);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Throw_exception_if_DatabaseManagment_not_found_Async()
        {
            DeleteQuery<Test1, DbConnection> query = new DeleteQuery<Test1, DbConnection>("DELETE FROM [TableName];",
                new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) },
                _connectionOptionsAsync);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await query.ExecuteAsync(null, CancellationToken.None));
        }

        [Fact]
        public async Task Should_executeAsync_the_query2()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            DeleteQuery<Test3, DbConnection> query = new DeleteQuery<Test3, DbConnection>("DELETE FROM [TableName];",
                new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _connectionOptionsAsync);
            var result = await query.ExecuteAsync(LoadFluentOptions.GetDbConnection(), CancellationToken.None);
            Assert.Equal(1, result);
        }
    }
}
