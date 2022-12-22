using GSqlQuery.Extensions;
using System;
using Xunit;

namespace GSqlQuery.Test.Extensions
{
    public class ObjectExtensionTest
    {
        [Fact]
        public void Throw_an_exception_if_the_object_is_null()
        {
            object result = null;
            Assert.Throws<ArgumentNullException>(() => result.NullValidate("Test", "resut"));
        }
    }
}
