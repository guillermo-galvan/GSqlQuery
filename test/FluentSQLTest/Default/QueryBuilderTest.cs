using FluentSQL.Default;
using FluentSQL.Models;
using FluentSQLTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest.Default
{
    public class QueryBuilderTest
    {
        [Fact]
        public void Properties_cannot_be_null()
        {
            QueryBuilder queryBuilder = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) });

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.Text);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new QueryBuilder(null, new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }));
            Assert.Throws<ArgumentNullException>(() => new QueryBuilder(new ClassOptions(typeof(Test1)), null));
            Assert.Throws<ArgumentNullException>(() => new QueryBuilder(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, null));
        }
    }
}
