using GSqlQuery.SearchCriteria;
using GSqlQuery.Runner.Test.Models;
using System.Linq;
using System;
using Xunit;
using System.Data.Common;

namespace GSqlQuery.Runner.Test.Queries
{
    public class BatchQueryTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly Equal<int> _equal;
        private readonly IStatements _statements;
        private readonly ClassOptions _classOptions;
        private readonly Test1 _test1;
        private readonly ConnectionOptions<DbConnection> _connectionOptions;

        public BatchQueryTest()
        {
            _statements = new Statements();
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.First(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _equal = new Equal<int>(_tableAttribute, _columnAttribute, 1);
            _test1 = new Test1();
            _connectionOptions = new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Throw_an_exception_if_null_parameter()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            InsertQuery<Test6, DbConnection> query = new InsertQuery<Test6, DbConnection>("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
               classOption.PropertyOptions.Select(x => x.ColumnAttribute),
               new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
               _connectionOptions, new Test6(1, null, DateTime.Now, true), classOption.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.IsAutoIncrementing));

            Assert.Throws<ArgumentNullException>(() => new BatchQuery(null, new ColumnAttribute[] { _columnAttribute }, null));
            Assert.Throws<ArgumentNullException>(() => new BatchQuery(query.Text, null, null));
        }
    }
}
