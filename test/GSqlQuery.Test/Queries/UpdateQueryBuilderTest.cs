using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using GSqlQuery.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
            Expression<Func<Test1, string>> expression = (x) => x.Name;
            UpdateQueryBuilder<Test1> queryBuilder = new UpdateQueryBuilder<Test1>(_queryOptions, expression, string.Empty);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.QueryOptions);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
            Assert.NotNull(queryBuilder.ColumnValues);
            Assert.NotEmpty(queryBuilder.ColumnValues);
            Assert.Single(queryBuilder.ColumnValues);
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface()
        {
            Expression<Func<Test1, string>> expression = (x) => x.Name;
            UpdateQueryBuilder<Test1> queryBuilder = new UpdateQueryBuilder<Test1>(_queryOptions, expression, string.Empty);
            IWhere<Test1, UpdateQuery<Test1>, QueryOptions> where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_update_query()
        {
            Test1 model = new Test1(1, null, DateTime.Now, true);
            Expression<Func<Test1, object>> expression = (x) => new { x.Id, x.Name, x.Create };
            UpdateQueryBuilder<Test1> queryBuilder = new UpdateQueryBuilder<Test1>(_queryOptions, model, expression);
            UpdateQuery<Test1> query = queryBuilder.Build();
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
            Expression<Func<Test1, object>> expression = (x) => new { x.Id, x.Name, x.Create };
            UpdateQueryBuilder<Test1> queryBuilder = new UpdateQueryBuilder<Test1>(_queryOptions, model, expression);

            queryBuilder.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now);

            Assert.NotNull(queryBuilder.ColumnValues);
            Assert.NotEmpty(queryBuilder.ColumnValues);
            Assert.Equal(3, queryBuilder.ColumnValues.Count);
        }

        [Fact]
        public void Should_add_a_new_column_value_with_property()
        {
            Test1 model = new Test1(1, null, DateTime.Now, true);
            Expression<Func<Test1, object>> expression = (x) => new { x.Id, x.Name, x.Create };
            UpdateQueryBuilder<Test1> test = new UpdateQueryBuilder<Test1>(_queryOptions, model, expression);

            test.Set(x => x.Id).Set(x => x.Create);

            Assert.NotNull(test.ColumnValues);
            Assert.NotEmpty(test.ColumnValues);
            Assert.Equal(3, test.ColumnValues.Count);
        }

        [Fact]
        public void Should_generate_the_query()
        {
            Test1 model = new Test1(1, null, DateTime.Now, true);
            Expression<Func<Test1, object>> expression = (x) => new { x.Id, x.Name, x.Create };
            UpdateQueryBuilder<Test1> test = new UpdateQueryBuilder<Test1>(_queryOptions, model, expression);
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
            Expression<Func<Test1, object>> expression = (x) => new { x.Id, x.Name, x.Create };
            UpdateQueryBuilder<Test1> test = new UpdateQueryBuilder<Test1>(_queryOptions, model, expression);
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
            Expression<Func<Test1, object>> expression = (x) => new { x.Id, x.Name, x.Create };
            UpdateQueryBuilder<Test1> test = new UpdateQueryBuilder<Test1>(_queryOptions, model, expression);
            var where = test.Set(x => x.Id).Set(x => x.Create).Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_get_the_where_query2()
        {
            Expression<Func<Test1, int>> expression = (x) => x.Id;
            UpdateQueryBuilder<Test1> test = new UpdateQueryBuilder<Test1>(_queryOptions, expression, string.Empty);
            var where = test.Set(x => x.Id, 1).Set(x => x.Create, DateTime.Now).Where();
            Assert.NotNull(where);
        }
    }
}