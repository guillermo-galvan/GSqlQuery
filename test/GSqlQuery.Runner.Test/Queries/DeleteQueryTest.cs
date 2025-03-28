﻿using GSqlQuery.Runner.Test.Models;
using GSqlQuery.SearchCriteria;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class DeleteQueryTest
    {
        private readonly TableAttribute _tableAttribute;
        private readonly Equal<Test1, int> _equal;
        private readonly IFormats _formats;
        private readonly ClassOptions _classOptions;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;
        private readonly ConnectionOptions<IDbConnection> _connectionOptionsAsync;
        private uint _parameterId = 0;

        public DeleteQueryTest()
        {
            _formats = new TestFormats();
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _tableAttribute = _classOptions.FormatTableName.Table;
            Expression<Func<Test1, int>> expression = (x) => x.Id;
            _equal = new Equal<Test1, int>(_classOptions, new DefaultFormats(), 1, null, ref expression);
            _connectionOptions = new ConnectionOptions<IDbConnection>(_formats, LoadGSqlQueryOptions.GetDatabaseManagmentMock());
            _connectionOptionsAsync = new ConnectionOptions<IDbConnection>(_formats, LoadGSqlQueryOptions.GetDatabaseManagmentMockAsync());
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            DeleteQuery<Test1, IDbConnection> query = new DeleteQuery<Test1, IDbConnection>("query", _tableAttribute, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _connectionOptions);

            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.QueryOptions);
            Assert.NotNull(query.QueryOptions.Formats);
            Assert.NotNull(query.QueryOptions.DatabaseManagement);
        }

        [Fact]
        public void Should_execute_the_query()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            DeleteQuery<Test3, IDbConnection> query = new DeleteQuery<Test3, IDbConnection>("DELETE FROM [TableName];", _tableAttribute, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _connectionOptions);

            var result = query.Execute();
            Assert.Equal(1, result);
        }

        [Fact]
        public void Throw_exception_if_DatabaseManagment_not_found()
        {
            DeleteQuery<Test1, IDbConnection> query = new DeleteQuery<Test1, IDbConnection>("DELETE FROM [TableName];", _tableAttribute, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _connectionOptions);
            Assert.Throws<ArgumentNullException>(() => query.Execute(null));
        }

        [Fact]
        public void Should_execute_the_query2()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            DeleteQuery<Test3, IDbConnection> query = new DeleteQuery<Test3, IDbConnection>("DELETE FROM [TableName];", _tableAttribute, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _connectionOptions);
            var result = query.Execute(LoadGSqlQueryOptions.GetIDbConnection());
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Should_executeAsync_the_query()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            DeleteQuery<Test3, IDbConnection> query = new DeleteQuery<Test3, IDbConnection>("DELETE FROM [TableName];", _tableAttribute, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _connectionOptionsAsync);
            var result = await query.ExecuteAsync(CancellationToken.None);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Throw_exception_if_DatabaseManagment_not_found_Async()
        {
            DeleteQuery<Test1, IDbConnection> query = new DeleteQuery<Test1, IDbConnection>("DELETE FROM [TableName];", _tableAttribute, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _connectionOptionsAsync);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await query.ExecuteAsync(null, CancellationToken.None));
        }

        [Fact]
        public async Task Should_executeAsync_the_query2()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            DeleteQuery<Test3, IDbConnection> query = new DeleteQuery<Test3, IDbConnection>("DELETE FROM [TableName];", _tableAttribute, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _connectionOptionsAsync);
            var result = await query.ExecuteAsync(LoadGSqlQueryOptions.GetIDbConnection(), CancellationToken.None);
            Assert.Equal(1, result);
        }
    }
}