using GSqlQuery.Extensions;
using GSqlQuery.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace GSqlQuery.Test.Extensions
{
    public class GeneralExtensionTest
    {
        public GeneralExtensionTest()
        { }

        [Fact]
        public void Should_return_the_column_attribute()
        {
            var classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test3));
            var result = ExpressionExtension.GetColumnsQuery(classOptions, [nameof(Test3.Ids), nameof(Test3.IsTests), nameof(Test3.Creates)]);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void Should_return_the_classoption_and_memeberinfos()
        {
            Expression<Func<Test1, object>> expression = x => new { x.Name, x.Create, x.IsTest };
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = ExpressionExtension.GetOptionsAndMembers(expression);
            Assert.NotNull(options.ClassOptions);
            Assert.NotNull(options.MemberInfo);
        }

        [Fact]
        public void Should_return_the_classoption_and_memeberinfo()
        {
            Expression<Func<Test1, object>> expression = x => x.Name;
            ClassOptionsTupla<MemberInfo> options = ExpressionExtension.GetOptionsAndMember(expression);
            Assert.NotNull(options.ClassOptions);
            Assert.NotNull(options.MemberInfo);
        }

        [Fact]
        public void Should_vallidate_memeberinfos()
        {
            Expression<Func<Test1, object>> expression = x => new { x.Name, x.Create, x.IsTest };
            ClassOptionsTupla<IEnumerable<MemberInfo>> options = ExpressionExtension.GetOptionsAndMembers(expression);

            try
            {
                ExpressionExtension.ValidateMemberInfos(QueryType.Delete, options);
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
            ClassOptionsTupla<MemberInfo> options = ExpressionExtension.GetOptionsAndMember(expression);
            var result = ExpressionExtension.ValidateMemberInfo(options.MemberInfo, options.ClassOptions);
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_get_value()
        {
            Test1 model = new Test1(1, "Name", DateTime.Now, true);
            Expression<Func<Test1, object>> expression = x => x.Name;
            ClassOptionsTupla<MemberInfo> options = ExpressionExtension.GetOptionsAndMember(expression);
            var propertyOptions = ExpressionExtension.ValidateMemberInfo(options.MemberInfo, options.ClassOptions);
            var result = ExpressionExtension.GetValue(propertyOptions,model);
            Assert.NotNull(result);
            Assert.NotEmpty(result.ToString());
        }

        [Fact]
        public void Should_return_the_property_options()
        {
            var classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test3));
            var result = ExpressionExtension.GetPropertyQuery(classOptions,new string[] { nameof(Test3.Ids), nameof(Test3.IsTests), nameof(Test3.Creates) });

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void Should_return_JoinCriteriaPart_two_tables()
        {
            Expression<Func<Join<Test3, Test6>, int>> expression = x => x.Table1.Ids;
            var result = ExpressionExtension.GetJoinColumn(expression);
            Assert.NotNull(result);
            Assert.NotNull(result.Table);
            Assert.NotNull(result.Column);
            Assert.NotNull(result.MemberInfo);
        }

        [Fact]
        public void Should_return_JoinCriteriaPart_three_tables()
        {
            Expression<Func<Join<Test3, Test6, Test1>, int>> expression = x => x.Table1.Ids;
            var result = ExpressionExtension.GetJoinColumn(expression);
            Assert.NotNull(result);
            Assert.NotNull(result.Table);
            Assert.NotNull(result.Column);
            Assert.NotNull(result.MemberInfo);
        }
    }
}