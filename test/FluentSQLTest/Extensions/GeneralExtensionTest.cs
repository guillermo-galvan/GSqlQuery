using FluentSQL.Helpers;
using FluentSQLTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentSQL.Extensions;
using System.Linq.Expressions;
using FluentSQL.Default;
using FluentSQL.Models;
using FluentSQL.SearchCriteria;

namespace FluentSQLTest.Extensions
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
            _stantements = new FluentSQL.Default.Statements();
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
            var (Options, MemberInfos) = expression.GetOptionsAndMembers();
            Assert.NotNull(Options);
            Assert.NotNull(MemberInfos);
        }

        [Fact]
        public void Should_return_the_classoption_and_memeberinfo()
        {
            Expression<Func<Test1, object>> expression = x => x.Name;
            var (Options, MemberInfos) = expression.GetOptionsAndMember();
            Assert.NotNull(Options);
            Assert.NotNull(MemberInfos);
        }

        [Fact]
        public void Should_vallidate_memeberinfos()
        {
            Expression<Func<Test1, object>> expression = x => new { x.Name, x.Create, x.IsTest };
            var (Options, MemberInfos) = expression.GetOptionsAndMembers();

            try
            {
                MemberInfos.ValidateMemberInfos("test");
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
            var (Options, MemberInfos) = expression.GetOptionsAndMember();
            var result = MemberInfos.ValidateMemberInfo(Options);
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_get_value()
        {
            Test1 model = new(1, "Name", DateTime.Now, true);
            Expression<Func<Test1, object>> expression = x => x.Name;
            var (Options, MemberInfos) = expression.GetOptionsAndMember();
            var propertyOptions = MemberInfos.ValidateMemberInfo(Options);
            var result = propertyOptions.GetValue(model);
            Assert.NotNull(result);
            Assert.NotEmpty(result.ToString());
        }

        [Fact]
        public void Should_get_parameters_in_delete_query()
        {
            DeleteQuery<Test1> query = new("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_stantements, _classOptions.PropertyOptions) }, _stantements);
            var result = query.GetParameters(LoadFluentOptions.GetDatabaseManagmentMock());

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_get_parameters_in_select_query()
        {
            SelectQuery<Test1> query = new("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_stantements, _classOptions.PropertyOptions) }, _stantements);
            var result = query.GetParameters(LoadFluentOptions.GetDatabaseManagmentMock());

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_get_parameters_in_update_query()
        {
            UpdateQuery<Test1> query = new("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_stantements, _classOptions.PropertyOptions) }, _stantements);
            var result = query.GetParameters(LoadFluentOptions.GetDatabaseManagmentMock());

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_get_parameters_in_insert_query()
        {
            InsertQuery<Test1> query = new("query", new ColumnAttribute[] { _columnAttribute }, new CriteriaDetail[] { _equal.GetCriteria(_stantements, _classOptions.PropertyOptions) }, _stantements, new Test1());
            var result = query.GetParameters(LoadFluentOptions.GetDatabaseManagmentMock());

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
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
