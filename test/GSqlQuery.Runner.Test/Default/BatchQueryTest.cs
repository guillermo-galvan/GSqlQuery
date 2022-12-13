using GSqlQuery.Default;
using GSqlQuery.Helpers;
using GSqlQuery.Models;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Runner.Test.Models;
using GSqlQuery.Runner.Default;

namespace GSqlQuery.Runner.Test.Default
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
            _statements = new GSqlQuery.Default.Statements();
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

            Assert.Throws<ArgumentNullException>(() => new BatchQuery(null, new ColumnAttribute[] { _columnAttribute }, null));
            Assert.Throws<ArgumentNullException>(() => new BatchQuery(insert.Text, null, null));
        }
    }
}
