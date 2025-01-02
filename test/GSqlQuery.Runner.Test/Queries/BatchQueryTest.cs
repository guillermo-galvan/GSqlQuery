using GSqlQuery.Runner.Test.Models;
using System;
using System.Data;
using System.Linq;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class BatchQueryTest
    {
        private readonly IFormats _formats;
        private readonly ClassOptions _classOptions;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;

        public BatchQueryTest()
        {
            _formats = new TestFormats();
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _connectionOptions = new ConnectionOptions<IDbConnection>(_formats, LoadGSqlQueryOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Throw_an_exception_if_null_parameter()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test6));

            InsertQuery<Test6, IDbConnection> query = new InsertQuery<Test6, IDbConnection>("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])", _classOptions.FormatTableName.Table, classOption.PropertyOptions, [], _connectionOptions, new Test6(1, null, DateTime.Now, true), null);

            Assert.Throws<ArgumentNullException>(() => new BatchQuery(null, _classOptions.FormatTableName.Table, _classOptions.PropertyOptions, null));
            Assert.Throws<ArgumentNullException>(() => new BatchQuery(query.Text, _classOptions.FormatTableName.Table, null, null));
        }
    }
}