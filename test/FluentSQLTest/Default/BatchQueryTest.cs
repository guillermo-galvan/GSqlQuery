using FluentSQL.Default;
using FluentSQL.Helpers;
using FluentSQL.Models;
using FluentSQL.SearchCriteria;
using FluentSQLTest.Models;
using FluentSQL.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using FluentSQL;

namespace FluentSQLTest.Default
{
    public class BatchQueryTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly Equal<int> _equal;
        private readonly IStatements _statements;
        private readonly ClassOptions _classOptions;
        private readonly Test1 _test1;

        public BatchQueryTest()
        {
            _statements = new FluentSQL.Default.Statements();
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.First(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _equal = new Equal<int>(_tableAttribute, _columnAttribute, 1);
            _test1 = new Test1();
        }

        [Fact]
        public void Throw_an_exception_if_null_parameter()
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            InsertQuery<Test3> insert = new("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
                classOption.PropertyOptions.Select(x => x.ColumnAttribute),
                new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
                _statements, new Test3(0, null, DateTime.Now, true));
            
            Assert.Throws<ArgumentNullException>(() => new BatchQuery(null, new ColumnAttribute[] { _columnAttribute }, null,  insert.GetParameters(LoadFluentOptions.GetDatabaseManagmentMock())));
            Assert.Throws<ArgumentNullException>(() => new BatchQuery(insert.Text, null, null, insert.GetParameters(LoadFluentOptions.GetDatabaseManagmentMock())));
            Assert.Throws<ArgumentNullException>(() => new BatchQuery(insert.Text, new ColumnAttribute[] { _columnAttribute }, null,  null));
        }

        [Fact]
        public void Should_execute_the_batches()
        {
            //var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            //InsertQuery<Test3> insert = new("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
            //    classOption.PropertyOptions.Select(x => x.ColumnAttribute),
            //    new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
            //    _statements, new Test3(0, null, DateTime.Now, true));

            //var result = new BatchQuery(insert.Text, new ColumnAttribute[] { _columnAttribute }, null, _statements, insert.GetParameters(LoadFluentOptions.GetDatabaseManagmentMock()))
            //                .SetDatabaseManagement(LoadFluentOptions.GetDatabaseManagmentMock()).Exec();

            //Assert.Equal(0, result);
        }

        [Fact]
        public void Should_execute_the_batches2()
        {
            //var classOption = ClassOptionsFactory.GetClassOptions(typeof(Test3));

            //InsertQuery<Test3> insert = new("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])",
            //    classOption.PropertyOptions.Select(x => x.ColumnAttribute),
            //    new CriteriaDetail[] { _equal.GetCriteria(_statements, classOption.PropertyOptions) },
            //    _statements, new Test3(0, null, DateTime.Now, true));

            //var result = new BatchQuery(insert.Text, new ColumnAttribute[] { _columnAttribute }, null, _statements, insert.GetParameters(LoadFluentOptions.GetDatabaseManagmentMock()))
            //    .SetDatabaseManagement(LoadFluentOptions.GetDatabaseManagmentMock()).Exec(LoadFluentOptions.GetDbConnection());

            //Assert.Equal(0, result);
        }
    }
}
