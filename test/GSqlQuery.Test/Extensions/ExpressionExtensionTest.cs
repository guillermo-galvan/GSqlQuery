using GSqlQuery.Extensions;
using GSqlQuery.Test.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace GSqlQuery.Test.Extensions
{
    public class ExpressionExtensionTest
    {
        [Fact]
        public void Should_extract_the_members_from_the_expression()
        {
            Expression<Func<Test1, string>> expression = x => x.Name;
            var result = ExpressionExtension.GetMembers(expression);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(nameof(Test1.Name), result.First().Name);
        }

        [Fact]
        public void Should_extract_the_members_from_the_expression_1()
        {
            Expression<Func<Test1, object>> expression = x => new { x.Name, x.Create, x.IsTest };
            var result = ExpressionExtension.GetMembers(expression);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            Assert.Collection(result,
                item => Assert.Equal(nameof(Test1.Name), item.Name),
                item => Assert.Equal(nameof(Test1.Create), item.Name),
                item => Assert.Equal(nameof(Test1.IsTest), item.Name)
            );
        }

        [Fact]
        public void Should_extract_the_member_from_the_expression()
        {
            Expression<Func<Test1, string>> expression = x => x.Name;
            var result = expression.GetMember();
            Assert.NotNull(result);
            Assert.Equal(nameof(Test1.Name), result.Name);

        }

        [Fact]
        public void Throw_exception_if_no_member_found_in_expression()
        {
            Expression<Func<Test1, Test1>> expression = x => x;
            Assert.Throws<InvalidOperationException>(() => expression.GetMember());
        }

        [Fact]
        public void Should_extract_the_ColumnAttribute_from_the_expression()
        {
            Expression<Func<Test1, string>> expression = x => x.Name;
            var result = ExpressionExtension.GetColumnAttribute(expression);
            Assert.NotNull(result);
        }

        [Fact]
        public void Throw_exception_if_no_ColumnAttribute_found_in_expression()
        {
            Expression<Func<Test1, Test1>> expression = x => x;
            Assert.Throws<InvalidOperationException>(() => ExpressionExtension.GetColumnAttribute(expression));
        }
    }
}