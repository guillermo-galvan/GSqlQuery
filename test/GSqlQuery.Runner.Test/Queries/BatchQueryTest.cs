using GSqlQuery.Runner.Test.Models;
using GSqlQuery.SearchCriteria;
using System;
using System.Data;
using System.Linq;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class BatchQueryTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly Equal<int> _equal;
        private readonly IStatements _statements;
        private readonly ClassOptions _classOptions;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;

        public BatchQueryTest()
        {
            _statements = new Statements();
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.First(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _equal = new Equal<int>(_tableAttribute, _columnAttribute, 1);
            _connectionOptions = new ConnectionOptions<IDbConnection>(_statements, LoadGSqlQueryOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Throw_an_exception_if_null_parameter()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            InsertQuery<Test6, IDbConnection> query = new InsertQuery<Test6, IDbConnection>("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
               classOption.PropertyOptions,
               new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
               _connectionOptions, new Test6(1, null, DateTime.Now, true), classOption.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.IsAutoIncrementing));

            Assert.Throws<ArgumentNullException>(() => new BatchQuery(null, _classOptions.PropertyOptions, null));
            Assert.Throws<ArgumentNullException>(() => new BatchQuery(query.Text, null, null));
        }
    }
}