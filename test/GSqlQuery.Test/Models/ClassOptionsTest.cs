using System;
using Xunit;

namespace GSqlQuery.Test.Models
{
    public class ClassOptionsTest
    {
        [Fact]
        public void Properties_cannot_be_null()
        {
            ClassOptions classOptions = new ClassOptions(typeof(Test1));
            Assert.NotNull(classOptions);
            Assert.NotNull(classOptions.Table);
            Assert.NotNull(classOptions.PropertyOptions);
            Assert.NotEmpty(classOptions.PropertyOptions);
            Assert.NotNull(classOptions.Type);
            Assert.NotNull(classOptions.ConstructorInfo);
            Assert.True(classOptions.IsConstructorByParam);
        }

        [Fact]
        public void Throw_an_exception_if_it_has_no_properties()
        {
            Assert.Throws<Exception>(() => new ClassOptions(typeof(Test2)));
        }

        [Fact]
        public void Throw_an_exception_if_it_has_no_constructor()
        {
            Assert.Throws<Exception>(() => new ClassOptions(typeof(Test5)));
        }
    }
}