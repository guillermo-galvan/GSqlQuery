﻿using GSqlQuery.Extensions;
using GSqlQuery.Runner.Test.Models;
using GSqlQuery.SearchCriteria;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class CountQueryTest
    {
        private readonly PropertyOptions _propertyOptions;
        private readonly Equal<Test1, int> _equal;
        private readonly ClassOptionsTupla<PropertyOptions> _classOptionsTupla;
        private readonly IFormats _formats;
        private readonly ClassOptions _classOptions;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;
        private readonly ConnectionOptions<IDbConnection> _connectionOptionsAsync;
        private uint _parameterId = 0;

        public CountQueryTest()
        {
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _propertyOptions = _classOptions.PropertyOptions[nameof(Test1.Id)];
            _classOptionsTupla = new ClassOptionsTupla<PropertyOptions>(_classOptions, _propertyOptions);
            Expression<Func<Test1, int>> expression = (x) => x.Id;
            _equal = new Equal<Test1, int>(_classOptionsTupla.ClassOptions, new DefaultFormats(), 1, null, ref expression);
            _formats = new TestFormats();
            _connectionOptions = new ConnectionOptions<IDbConnection>(_formats, LoadGSqlQueryOptions.GetDatabaseManagmentMock());
            _connectionOptionsAsync = new ConnectionOptions<IDbConnection>(_formats, LoadGSqlQueryOptions.GetDatabaseManagmentMockAsync());
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            CountQuery<Test1, IDbConnection> query = new CountQuery<Test1, IDbConnection>("query", _classOptions.FormatTableName.Table, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _connectionOptions);

            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.QueryOptions);
            Assert.NotNull(query.QueryOptions.DatabaseManagement);
        }

        [Fact]
        public void Should_execute_the_query()
        {
            CountQuery<Test1, IDbConnection> query = new CountQuery<Test1, IDbConnection>("SELECT COUNT([Test1].[Id]) FROM [Test1];", _classOptions.FormatTableName.Table, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _connectionOptions);
            var result = query.Execute();
            Assert.Equal(1, result);
        }


        [Fact]
        public void Throw_exception_if_DatabaseManagment_not_found()
        {
            CountQuery<Test1, IDbConnection> query = new CountQuery<Test1, IDbConnection>("SELECT COUNT([Test1].[Id]) FROM [Test1];", _classOptions.FormatTableName.Table, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _connectionOptions);
            Assert.Throws<ArgumentNullException>(() => query.Execute(null));
        }

        [Fact]
        public void Should_execute_the_query1()
        {
            CountQuery<Test1, IDbConnection> query = new CountQuery<Test1, IDbConnection>("SELECT COUNT([Test1].[Id]) FROM [Test1];", _classOptions.FormatTableName.Table, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _connectionOptions);
            var result = query.Execute(LoadGSqlQueryOptions.GetIDbConnection());
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Should_executeAsync_the_query()
        {
            CountQuery<Test1, IDbConnection> query = new CountQuery<Test1, IDbConnection>("SELECT COUNT([Test1].[Id]) FROM [Test1];", _classOptions.FormatTableName.Table, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _connectionOptionsAsync);
            var result = await query.ExecuteAsync(CancellationToken.None);
            Assert.Equal(1, result);
        }


        [Fact]
        public async Task Throw_exceptionAsync_if_DatabaseManagment_not_found()
        {
            CountQuery<Test1, IDbConnection> query = new CountQuery<Test1, IDbConnection>("SELECT COUNT([Test1].[Id]) FROM [Test1];", _classOptions.FormatTableName.Table, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _connectionOptionsAsync);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await query.ExecuteAsync(null, CancellationToken.None));
        }

        [Fact]
        public async Task Should_executeAsync_the_query1()
        {
            CountQuery<Test1, IDbConnection> query = new CountQuery<Test1, IDbConnection>("SELECT COUNT([Test1].[Id]) FROM [Test1];", _classOptions.FormatTableName.Table, _classOptions.PropertyOptions, [_equal.GetCriteria(ref _parameterId)], _connectionOptionsAsync);
            var result = await query.ExecuteAsync(LoadGSqlQueryOptions.GetIDbConnection(), CancellationToken.None);
            Assert.Equal(1, result);
        }
    }
}