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
    public class SelectQueryBuilderTest
    {
        private readonly ConnectionOptions _connectionOptions;

        public SelectQueryBuilderTest()
        {
            _connectionOptions = new ConnectionOptions(new FluentSQL.Default.Statements());
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            SelectQueryBuilder<Test1> queryBuilder = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.ConnectionOptions);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new DeleteQueryBuilder<Test1>(null, new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, _connectionOptions));
            Assert.Throws<ArgumentNullException>(() => new DeleteQueryBuilder<Test1>(new ClassOptions(typeof(Test1)), null, _connectionOptions));
            Assert.Throws<ArgumentNullException>(() => new DeleteQueryBuilder<Test1>(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, null));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface()
        {
            SelectQueryBuilder<Test1> queryBuilder = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);
            IWhere<Test1, SelectQuery<Test1>> where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_delete_query()
        {
            SelectQueryBuilder<Test1> queryBuilder = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                _connectionOptions);
            IQuery<Test1> query = queryBuilder.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.ConnectionOptions);
            Assert.NotNull(query.ConnectionOptions.Statements);
            Assert.Null(query.Criteria);
        }
    }
}
