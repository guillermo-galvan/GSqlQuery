using GSqlQuery.Extensions;
using System;
using Xunit;

namespace GSqlQuery.Test.Extensions
{
    public class StringExtensionTest
    {
        [Fact]
        public void Throw_an_exception_if_the_object_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => "".NullValidate("Test", "resut"));
        }
    }
}