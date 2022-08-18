using FluentSQL.Helpers;
using FluentSQLTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentSQL.Extensions;
using System.Linq.Expressions;

namespace FluentSQLTest.Extensions
{
    public class GeneralExtensionTest
    {
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
    }
}
