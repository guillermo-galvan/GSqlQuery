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
        private readonly IStatements _statements;

        public UpdateQueryBuilderTest()
        {
            _columnsValue = new()
            {
                { new ColumnAttribute("Create"), DateTime.Now.Ticks },
                { new ColumnAttribute("Id"), "test" },
                { new ColumnAttribute(nameof(Test3.IsTests)), true }
            };
            _statements = new FluentSQL.Default.Statements();
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            UpdateQueryBuilder<Test1> queryBuilder = new(_statements, _columnsValue);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.Statements);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1>(null, _columnsValue));
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1>(_statements, null));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface()
        {
            UpdateQueryBuilder<Test1> queryBuilder = new(_statements, _columnsValue);
            IWhere<Test1, UpdateQuery<Test1>> where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_update_query()
        {
            UpdateQueryBuilder<Test3> queryBuilder = new(_statements, _columnsValue);
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
