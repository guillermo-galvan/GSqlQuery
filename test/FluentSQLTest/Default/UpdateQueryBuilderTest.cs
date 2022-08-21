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
    public class UpdateQueryBuilderTest
    {
        private Dictionary<ColumnAttribute, object?> _columnsValue;

        public UpdateQueryBuilderTest()
        {
            _columnsValue = new()
            {
                { new ColumnAttribute("Create"), DateTime.Now.Ticks },
                { new ColumnAttribute("Id"), "test" },
                { new ColumnAttribute(nameof(Test3.IsTests)), true }
            };
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            UpdateQueryBuilder<Test1> queryBuilder = new(new ClassOptions(typeof(Test3)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, new FluentSQL.Default.Statements(), _columnsValue);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.Statements);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1>(null, new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, new FluentSQL.Default.Statements(), _columnsValue));
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1>(new ClassOptions(typeof(Test3)), null, new FluentSQL.Default.Statements(), _columnsValue));
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1>(new ClassOptions(typeof(Test3)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, null, _columnsValue));
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1>(new ClassOptions(typeof(Test3)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, new FluentSQL.Default.Statements(), null));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface()
        {
            UpdateQueryBuilder<Test1> queryBuilder = new(new ClassOptions(typeof(Test3)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, new FluentSQL.Default.Statements(), _columnsValue);
            IWhere<Test1, UpdateQuery<Test1>> where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_update_query()
        {
            UpdateQueryBuilder<Test3> queryBuilder = new(new ClassOptions(typeof(Test3)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, new FluentSQL.Default.Statements(), _columnsValue);
            UpdateQuery<Test3> query = queryBuilder.Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);

        }
    }
}
