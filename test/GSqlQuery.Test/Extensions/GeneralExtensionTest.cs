using GSqlQuery.Extensions;
using System.Linq.Expressions;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Models;
using System.Reflection;

namespace GSqlQuery.Test.Extensions
{
    public class GeneralExtensionTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly Equal<int> _equal;
        private readonly IStatements _stantements;
        private readonly ClassOptions _classOptions;

        public GeneralExtensionTest()
        {
            _stantements = new Statements();
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _equal = new Equal<int>(_tableAttribute, _columnAttribute, 1);
        }

        [Fact]
        public void Should_return_the_column_attribute()
        {
            var classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test3));
            var result = classOptions.GetColumnsQuery(new string[] { nameof(Test3.Ids), nameof(Test3.IsTests), nameof(Test3.Creates) });

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void Should_return_the_classoption_and_memeberinfos()
        {
            Expression<Func<Test1, object>> expression = x => new { x.Name, x.Create, x.IsTest };
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();
            Assert.NotNull(options.ClassOptions);
            Assert.NotNull(options.MemberInfo);
        }

        [Fact]
        public void Should_return_the_classoption_and_memeberinfo()
        {
            Expression<Func<Test1, object>> expression = x => x.Name;
            ClassOptionsTupla<MemberInfo> options = expression.GetOptionsAndMember();
            Assert.NotNull(options.ClassOptions);
            Assert.NotNull(options.MemberInfo);
        }

        [Fact]
        public void Should_vallidate_memeberinfos()
        {
            Expression<Func<Test1, object>> expression = x => new { x.Name, x.Create, x.IsTest };
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = expression.GetOptionsAndMembers();

            try
            {
                options.MemberInfo.ValidateMemberInfos("test");
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Should_vallidate_memeberinfo()
        {
            Expression<Func<Test1, object>> expression = x => x.Name;
            ClassOptionsTupla<MemberInfo> options = expression.GetOptionsAndMember();
            var result = options.MemberInfo.ValidateMemberInfo(options.ClassOptions);
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_get_value()
        {
            Test1 model = new(1, "Name", DateTime.Now, true);
            Expression<Func<Test1, object>> expression = x => x.Name;
            ClassOptionsTupla<MemberInfo> options = expression.GetOptionsAndMember();
            var propertyOptions = options.MemberInfo.ValidateMemberInfo(options.ClassOptions);
            var result = propertyOptions.GetValue(model);
            Assert.NotNull(result);
            Assert.NotEmpty(result.ToString());
        }

        [Fact]
        public void Should_return_the_property_options()
        {
            var classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test3));
            var result = classOptions.GetPropertyQuery(new string[] { nameof(Test3.Ids), nameof(Test3.IsTests), nameof(Test3.Creates) });

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
        }
    }
}
