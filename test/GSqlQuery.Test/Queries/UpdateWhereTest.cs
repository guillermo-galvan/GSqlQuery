﻿using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Models;
using System;
using System.Linq.Expressions;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class UpdateWhereTest
    {
        private readonly Equal<Test1, int> _equal;
        private readonly UpdateQueryBuilder<Test1> _queryBuilder;

        public UpdateWhereTest()
        {
            var classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            var columnAttribute = classOptions.PropertyOptions[nameof(Test1.Id)];
            var classOptionsTupla = new ClassOptionsTupla<PropertyOptions>(classOptions, columnAttribute);
            Expression<Func<Test1, int>> expression = (x) => x.Id;
            _equal = new Equal<Test1, int>(classOptionsTupla.ClassOptions, new DefaultFormats(), 1, null, ref expression);
            _queryBuilder = new UpdateQueryBuilder<Test1>(new QueryOptions(new DefaultFormats()), expression, string.Empty);
        }

        [Fact]
        public void Should_add_criteria_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1>, QueryOptions> query = new AndOrBase<Test1, UpdateQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            Assert.NotNull(query);
            query.Add(_equal);
            Assert.True(true);
        }

        [Fact]
        public void Throw_exception_if_null_ISearchCriteria_is_added_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1>, QueryOptions> query = new AndOrBase<Test1, UpdateQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            Assert.NotNull(query);
            Assert.Throws<ArgumentNullException>(() => query.Add(null));
        }

        [Fact]
        public void Should_build_the_criteria_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1>, QueryOptions> query = new AndOrBase<Test1, UpdateQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            Assert.NotNull(query);
            query.Add(_equal);

            var criteria = ((ISearchCriteriaBuilder)query).Create();
            Assert.NotNull(criteria);
            Assert.NotEmpty(criteria);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_with_expression_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, UpdateQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            where.Equal(x => x.Id, 1);
            IAndOr<Test1, UpdateQuery<Test1>, QueryOptions> andOr = where.AndOr;
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_with_expression_UpdateQuery_NUll()
        {
            AndOrBase<Test1, UpdateQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, UpdateQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            IAndOr<Test1, UpdateQuery<Test1>, QueryOptions> andOr = where.AndOr;
            Assert.Null(andOr);
        }

        [Fact]
        public void Should_validate_of_IAndOr_UpdateQuery()
        {
            IAndOr<Test1, UpdateQuery<Test1>, QueryOptions> andOr = new AndOrBase<Test1, UpdateQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            try
            {
                GSqlQueryExtension.Validate(andOr, x => x.IsTest);
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_in_IAndOr_UpdateQuery()
        {
            IAndOr<Test1, UpdateQuery<Test1>, QueryOptions> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.Validate(where, x => x.Id));
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, UpdateQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            where.Equal(x => x.Id, 1);
            IAndOr<Test1, UpdateQuery<Test1>, QueryOptions> andOr = where.AndOr;
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_UpdateQuery_Null()
        {
            AndOrBase<Test1, UpdateQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, UpdateQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            IAndOr<Test1, UpdateQuery<Test1>, QueryOptions> andOr = where.AndOr;
            Assert.Null(andOr);
        }
    }
}