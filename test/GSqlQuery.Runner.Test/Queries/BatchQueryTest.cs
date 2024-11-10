using GSqlQuery.Runner.Test.Models;
using System;
using System.Data;
using System.Linq;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class BatchQueryTest
    {
        private readonly CriteriaDetail _equal;
        private readonly IFormats _formats;
        private readonly ClassOptions _classOptions;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;

        public BatchQueryTest()
        {
            _formats = new TestFormats();
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _equal = new CriteriaDetail("SELECT COUNT([Test1].[Id]) FROM [Test1];", Array.Empty<ParameterDetail>());
            _connectionOptions = new ConnectionOptions<IDbConnection>(_formats, LoadGSqlQueryOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Throw_an_exception_if_null_parameter()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            InsertQuery<Test6, IDbConnection> query = new InsertQuery<Test6, IDbConnection>("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
               classOption.PropertyOptions,
               new CriteriaDetail[] { _equal },
               _connectionOptions, new Test6(1, null, DateTime.Now, true), classOption.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.IsAutoIncrementing));

            Assert.Throws<ArgumentNullException>(() => new BatchQuery(null, _classOptions.PropertyOptions, null));
            Assert.Throws<ArgumentNullException>(() => new BatchQuery(query.Text, null, null));
        }
    }
}