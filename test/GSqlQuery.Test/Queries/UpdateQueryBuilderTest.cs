using GSqlQuery.Queries;
using GSqlQuery.Test.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class UpdateQueryBuilderTest
    {
        private readonly List<string> _columnsValue;
        private readonly IStatements _statements;

        public UpdateQueryBuilderTest()
        {
            _columnsValue = new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) };
            _statements = new Statements();
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            UpdateQueryBuilder<Test1> queryBuilder = new UpdateQueryBuilder<Test1>(_statements, _columnsValue, string.Empty);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.Options);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
            Assert.NotNull(queryBuilder.ColumnValues);
            Assert.NotEmpty(queryBuilder.ColumnValues);
            Assert.Equal(3, queryBuilder.ColumnValues.Count);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            object entity = null;
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1>(null, _columnsValue, string.Empty));
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1>(_statements, null, string.Empty));
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1>(_statements, entity, _columnsValue));
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1>(null, entity, _columnsValue));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface()
        {
            UpdateQueryBuilder<Test1> queryBuilder = new UpdateQueryBuilder<Test1>(_statements, _columnsValue, string.Empty);
            IWhere<Test1, UpdateQuery<Test1>> where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_update_query()
        {
            UpdateQueryBuilder<Test3> queryBuilder = new UpdateQueryBuilder<Test3>(_statements, new List<string> { nameof(Test3.Ids), nameof(Test3.Names), nameof(Test3.Creates) },
                string.Empty);
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

        [Fact]
        public void Should_add_a_new_column_value_with_set_value()
        {
            UpdateQueryBuilder<Test1> queryBuilder = new UpdateQueryBuilder<Test1>(_statements, new List<string> { nameof(Test1.Name) }, string.Empty);

            queryBuilder.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now);

            Assert.NotNull(queryBuilder.ColumnValues);
            Assert.NotEmpty(queryBuilder.ColumnValues);
            Assert.Equal(3, queryBuilder.ColumnValues.Count);
        }

        [Fact]
        public void Should_add_a_new_column_value_with_property()
        {
            Test1 model = new Test1(1, null, DateTime.Now, true);
            UpdateQueryBuilder<Test1> test = new UpdateQueryBuilder<Test1>(_statements, model, new List<string> { nameof(Test1.Name) });

            test.Set(x => x.Id).Set(x => x.Create);

            Assert.NotNull(test.ColumnValues);
            Assert.NotEmpty(test.ColumnValues);
            Assert.Equal(3, test.ColumnValues.Count);
        }

        [Fact]
        public void Should_generate_the_query()
        {
            Test1 model = new Test1(1, null, DateTime.Now, true);
            UpdateQueryBuilder<Test1> test = new UpdateQueryBuilder<Test1>(_statements, model, new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) });
            var query = test.Set(x => x.Id).Set(x => x.Create).Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }


        [Fact]
        public void Should_generate_the_query2()
        {
            UpdateQueryBuilder<Test1> test = new UpdateQueryBuilder<Test1>(_statements, new List<string> { nameof(Test1.Name) }, string.Empty);
            var query = test.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now).Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }

        [Fact]
        public void Should_get_the_where_query()
        {
            Test1 model = new Test1(1, null, DateTime.Now, true);
            UpdateQueryBuilder<Test1> test = new UpdateQueryBuilder<Test1>(_statements, model, new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) });
            var where = test.Set(x => x.Id).Set(x => x.Create).Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_get_the_where_query2()
        {
            UpdateQueryBuilder<Test1> test = new UpdateQueryBuilder<Test1>(_statements, new List<string> { nameof(Test1.Name) }, string.Empty);
            var where = test.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now).Where();
            Assert.NotNull(where);
        }
    }
}