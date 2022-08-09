using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentSQL.Extensions;
using FluentSQLTest.Models;

namespace FluentSQLTest.Extensions
{
    public class ExpressionExtensionTest
    {
        [Fact]
        public void It_should_extract_the_member_from_the_member_expression()
        {
            Expression<Func<Test1, string>> expression = x => x.Name;
            var result = expression.GetMembers();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(nameof(Test1.Name), result.First().Name);
        }

        [Fact]
        public void It_should_extract_the_member_from_the_member_expression_1()
        {
            Expression<Func<Test1, object>> expression = x => new { x.Name, x.Create, x.IsTest};
            var result = expression.GetMembers();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
            Assert.Collection(result, 
                item => Assert.Equal(nameof(Test1.Name), item.Name),
                item => Assert.Equal(nameof(Test1.Create), item.Name),
                item => Assert.Equal(nameof(Test1.IsTest), item.Name)
            );
        }
    }
}
