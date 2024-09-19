using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using GSqlQuery.Test.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class UpdateQueryBuilderTest
    {
        private readonly QueryOptions _queryOptions;

        public UpdateQueryBuilderTest()
        {
            _queryOptions = new QueryOptions(new DefaultFormats());
        }

        [Fact]
        public void Properties_cannot_be_null()
        {
            ClassOptionsTupla<KeyValuePair<string, PropertyOptions>> columnsValue = ExpressionExtension.GetOptionsAndMember<Test1, string>((x) => x.Name);
            UpdateQueryBuilder<Test1> queryBuilder = new UpdateQueryBuilder<Test1>(_queryOptions, columnsValue, string.Empty);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.QueryOptions);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
            Assert.NotNull(queryBuilder.ColumnValues);
            Assert.NotEmpty(queryBuilder.ColumnValues);
            Assert.Single(queryBuilder.ColumnValues);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            object entity = null;
            var columsn = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            ClassOptionsTupla<KeyValuePair<string, PropertyOptions>> columnsValue = ExpressionExtension.GetOptionsAndMember<Test1, string>((x) => x.Name);
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1>(null, columnsValue, string.Empty));
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1>(_queryOptions, null, string.Empty));
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1>(_queryOptions, entity, columsn));
            Assert.Throws<ArgumentNullException>(() => new UpdateQueryBuilder<Test1>(null, entity, columsn));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface()
        {
            ClassOptionsTupla<KeyValuePair<string, PropertyOptions>> columnsValue = ExpressionExtension.GetOptionsAndMember<Test1, string>((x) => x.Name);
            UpdateQueryBuilder<Test1> queryBuilder = new UpdateQueryBuilder<Test1>(_queryOptions, columnsValue, string.Empty);
            IWhere<Test1, UpdateQuery<Test1>, QueryOptions> where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_update_query()
        {
            Test1 model = new Test1(1, null, DateTime.Now, true);
            var columns = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            UpdateQueryBuilder<Test3> queryBuilder = new UpdateQueryBuilder<Test3>(_queryOptions, model, columns);
            UpdateQuery<Test3> query = queryBuilder.Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.QueryOptions.Formats);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }

        [Fact]
        public void Should_add_a_new_column_value_with_set_value()
        {
            Test1 model = new Test1(1, null, DateTime.Now, true);
            var columns = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            UpdateQueryBuilder<Test1> queryBuilder = new UpdateQueryBuilder<Test1>(_queryOptions, model, columns);

            queryBuilder.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now);

            Assert.NotNull(queryBuilder.ColumnValues);
            Assert.NotEmpty(queryBuilder.ColumnValues);
            Assert.Equal(3, queryBuilder.ColumnValues.Count);
        }

        [Fact]
        public void Should_add_a_new_column_value_with_property()
        {
            Test1 model = new Test1(1, null, DateTime.Now, true);
            var columns = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            UpdateQueryBuilder<Test1> test = new UpdateQueryBuilder<Test1>(_queryOptions, model, columns);

            test.Set(x => x.Id).Set(x => x.Create);

            Assert.NotNull(test.ColumnValues);
            Assert.NotEmpty(test.ColumnValues);
            Assert.Equal(3, test.ColumnValues.Count);
        }

        [Fact]
        public void Should_generate_the_query()
        {
            Test1 model = new Test1(1, null, DateTime.Now, true);
            var columns = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            UpdateQueryBuilder<Test1> test = new UpdateQueryBuilder<Test1>(_queryOptions, model, columns);
            var query = test.Set(x => x.Id).Set(x => x.Create).Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.QueryOptions.Formats);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }


        [Fact]
        public void Should_generate_the_query2()
        {
            Test1 model = new Test1(1, null, DateTime.Now, true);
            var columns = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            UpdateQueryBuilder<Test1> test = new UpdateQueryBuilder<Test1>(_queryOptions, model, columns);
            var query = test.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now).Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.QueryOptions.Formats);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
        }

        [Fact]
        public void Should_get_the_where_query()
        {
            Test1 model = new Test1(1, null, DateTime.Now, true);
            var columns = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>((x) => new { x.Id, x.Name, x.Create });
            UpdateQueryBuilder<Test1> test = new UpdateQueryBuilder<Test1>(_queryOptions, model, columns);
            var where = test.Set(x => x.Id).Set(x => x.Create).Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_get_the_where_query2()
        {
            var column = ExpressionExtension.GetOptionsAndMember<Test1, object>((x) => x.Id);
            UpdateQueryBuilder<Test1> test = new UpdateQueryBuilder<Test1>(_queryOptions, column, string.Empty);
            var where = test.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now).Where();
            Assert.NotNull(where);
        }
    }
}