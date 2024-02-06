﻿using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class SelectWhereTest
    {
        private readonly Equal<int> _equal;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;

        public SelectWhereTest()
        {
            var classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            var columnAttribute = classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            var classOptionsTupla = new ClassOptionsTupla<ColumnAttribute>(classOptions, columnAttribute);
            _equal = new Equal<int>(classOptionsTupla, new DefaultFormats(), 1);
            _queryBuilder = new SelectQueryBuilder<Test1>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
               new DefaultFormats());
        }

        [Fact]
        public void Should_add_criteria_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1>, IFormats> query = new AndOrBase<Test1, SelectQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            Assert.NotNull(query);
            query.Add(_equal);
            Assert.True(true);
        }

        [Fact]
        public void Throw_exception_if_null_ISearchCriteria_is_added_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1>, IFormats> query = new AndOrBase<Test1, SelectQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            Assert.NotNull(query);
            Assert.Throws<ArgumentNullException>(() => query.Add(null));
        }

        [Fact]
        public void Should_build_the_criteria_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1>, IFormats> query = new AndOrBase<Test1, SelectQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            Assert.NotNull(query);
            query.Add(_equal);

            var criteria = ((ISearchCriteriaBuilder<SelectQuery<Test1>>)query).BuildCriteria();
            Assert.NotNull(criteria);
            Assert.NotEmpty(criteria);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_with_expression_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1>, IFormats> where = new AndOrBase<Test1, SelectQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            IAndOr<Test1, SelectQuery<Test1>> andOr = GSqlQueryExtension.GetAndOr(where, x => x.Id);
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_with_expression_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1>, IFormats> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.GetAndOr(where, x => x.Id));
        }

        [Fact]
        public void Should_validate_of_IAndOr_SelectQuery()
        {
            IAndOr<Test1, SelectQuery<Test1>> andOr = new AndOrBase<Test1, SelectQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
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
        public void Throw_exception_if_expression_is_null_in_IAndOr_SelectQuery()
        {
            IAndOr<Test1, SelectQuery<Test1>> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.Validate(where, x => x.Id));
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1>, IFormats> where = new AndOrBase<Test1, SelectQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            IAndOr<Test1, SelectQuery<Test1>> andOr = GSqlQueryExtension.GetAndOr(where);
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_SelectQuery()
        {
            AndOrBase<Test1, SelectQuery<Test1>, IFormats> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.GetAndOr(where));
        }
    }
}