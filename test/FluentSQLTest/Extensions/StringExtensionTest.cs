using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentSQL.Extensions;

namespace FluentSQLTest.Extensions
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
