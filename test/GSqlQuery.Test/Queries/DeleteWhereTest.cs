﻿using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Models;
using System;
using System.Linq;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class DeleteWhereTest
    {
        private readonly Equal<int> _equal;
        private readonly DeleteQueryBuilder<Test1> _queryBuilder;

        public DeleteWhereTest()
        {
            var classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            var columnAttribute = classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            var classOptionsTupla = new ClassOptionsTupla<ColumnAttribute>(classOptions, columnAttribute);
            _equal = new Equal<int>(classOptionsTupla, new DefaultFormats(), 1);
            _queryBuilder = new DeleteQueryBuilder<Test1>(new QueryOptions(new DefaultFormats()));

        }

        [Fact]
        public void Should_add_criteria_DeleteQuery()
        {
            AndOrBase<Test1, DeleteQuery<Test1>, QueryOptions> query = new AndOrBase<Test1, DeleteQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            Assert.NotNull(query);
            query.Add(_equal);
            Assert.True(true);
        }

        [Fact]
        public void Throw_exception_if_null_ISearchCriteria_is_added_DeleteQuery()
        {
            AndOrBase<Test1, DeleteQuery<Test1>, QueryOptions> query = new AndOrBase<Test1, DeleteQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            Assert.NotNull(query);
            Assert.Throws<ArgumentNullException>(() => query.Add(null));
        }

        [Fact]
        public void Should_build_the_criteria_DeleteQuery()
        {
            AndOrBase<Test1, DeleteQuery<Test1>, QueryOptions> query = new AndOrBase<Test1, DeleteQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            Assert.NotNull(query);
            query.Add(_equal);

            var criteria = ((ISearchCriteriaBuilder<DeleteQuery<Test1>>)query).BuildCriteria();
            Assert.NotNull(criteria);
            Assert.NotEmpty(criteria);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_with_expression_DeleteQuery()
        {
            AndOrBase<Test1, DeleteQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, DeleteQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            IAndOr<Test1, DeleteQuery<Test1>, QueryOptions> andOr = GSqlQueryExtension.GetAndOr(where, x => x.Id);
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_with_expression_DeleteQuery()
        {
            AndOrBase<Test1, DeleteQuery<Test1>, QueryOptions> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.GetAndOr(where, x => x.Id));
        }

        [Fact]
        public void Should_validate_of_IAndOr_DeleteQuery()
        {
            IAndOr<Test1, DeleteQuery<Test1>, QueryOptions> andOr = new AndOrBase<Test1, DeleteQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
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
        public void Throw_exception_if_expression_is_null_in_IAndOr_DeleteQuery()
        {
            IAndOr<Test1, DeleteQuery<Test1>, QueryOptions> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.Validate(where, x => x.Id));
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_DeleteQuery()
        {
            AndOrBase<Test1, DeleteQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, DeleteQuery<Test1>, QueryOptions>(_queryBuilder, _queryBuilder.QueryOptions);
            IAndOr<Test1, DeleteQuery<Test1>, QueryOptions> andOr = GSqlQueryExtension.GetAndOr(where);
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_DeleteQuery()
        {
            AndOrBase<Test1, DeleteQuery<Test1>, QueryOptions> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.GetAndOr(where));
        }
    }
}