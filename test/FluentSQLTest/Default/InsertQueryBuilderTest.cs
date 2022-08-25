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
    public class InsertQueryBuilderTest
    {
        private readonly ConnectionOptions _connectionOptions;

        public InsertQueryBuilderTest()
        {
            _connectionOptions = new ConnectionOptions(new FluentSQL.Default.Statements());
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            InsertQueryBuilder<Test3> queryBuilder = new(new ClassOptions(typeof(Test3)), new List<string> { nameof(Test3.Ids), nameof(Test3.Names), nameof(Test3.Creates), nameof(Test3.Creates) }, _connectionOptions, new Test3(1, null, DateTime.Now, true));

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.ConnectionOptions);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new InsertQueryBuilder<Test1>(null, new List<string> { nameof(Test3.Ids), nameof(Test3.Names), nameof(Test3.Creates), nameof(Test3.Creates) }, _connectionOptions, new Test3(1, null, DateTime.Now, true)));
            Assert.Throws<ArgumentNullException>(() => new InsertQueryBuilder<Test1>(new ClassOptions(typeof(Test3)), null, _connectionOptions, new Test3(1, null, DateTime.Now, true)));
            Assert.Throws<ArgumentNullException>(() => new InsertQueryBuilder<Test1>(new ClassOptions(typeof(Test3)), new List<string> { nameof(Test3.Ids), nameof(Test3.Names), nameof(Test3.Creates), nameof(Test3.Creates) }, null, new Test3(1, null, DateTime.Now, true)));
            Assert.Throws<ArgumentNullException>(() => new InsertQueryBuilder<Test1>(new ClassOptions(typeof(Test3)), new List<string> { nameof(Test3.Ids), nameof(Test3.Names), nameof(Test3.Creates), nameof(Test3.Creates) }, _connectionOptions, null));
        }

        [Fact]
        public void Should_return_an_insert_query()
        {
            InsertQueryBuilder<Test1> queryBuilder = new(new ClassOptions(typeof(Test3)), new List<string> { nameof(Test3.Ids), nameof(Test3.Names), nameof(Test3.Creates), nameof(Test3.Creates) }, _connectionOptions, new Test3(1, null, DateTime.Now, true));
            IQuery<Test1> query = queryBuilder.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.ConnectionOptions);
            Assert.NotNull(query.ConnectionOptions.Statements);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }
    }
}
