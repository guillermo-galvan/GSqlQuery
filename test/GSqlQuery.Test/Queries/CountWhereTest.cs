using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class CountWhereTest
    {
        private readonly Equal<int> _equal;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly CountQueryBuilder<Test1> _countQueryBuilder;
        private readonly ClassOptionsTupla<ColumnAttribute> _classOptionsTupla;

        public CountWhereTest()
        {
            var classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _classOptionsTupla = new ClassOptionsTupla<ColumnAttribute>(classOptions, new ColumnAttribute("Id"));
            _equal = new Equal<int>(_classOptionsTupla, new DefaultFormats(), 1);
            _queryBuilder = new SelectQueryBuilder<Test1>(new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, new DefaultFormats());
            _countQueryBuilder = new CountQueryBuilder<Test1>(_queryBuilder);
        }

        [Fact]
        public void Should_add_criteria_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1>, IFormats> query = new AndOrBase<Test1, CountQuery<Test1>, IFormats>(_countQueryBuilder, _countQueryBuilder.Options);
            Assert.NotNull(query);
            query.Add(_equal);
            Assert.True(true);
        }

        [Fact]
        public void Throw_exception_if_null_ISearchCriteria_is_added_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1>, IFormats> query = new AndOrBase<Test1, CountQuery<Test1>, IFormats>(_countQueryBuilder, _countQueryBuilder.Options);
            Assert.NotNull(query);
            Assert.Throws<ArgumentNullException>(() => query.Add(null));
        }

        [Fact]
        public void Should_build_the_criteria_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1>, IFormats> query = new AndOrBase<Test1, CountQuery<Test1>, IFormats>(_countQueryBuilder, _countQueryBuilder.Options);
            Assert.NotNull(query);
            query.Add(_equal);

            var criteria = ((ISearchCriteriaBuilder<CountQuery<Test1>>)query).BuildCriteria();
            Assert.NotNull(criteria);
            Assert.NotEmpty(criteria);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_with_expression_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1>, IFormats> where = new AndOrBase<Test1, CountQuery<Test1>, IFormats>(_countQueryBuilder, _countQueryBuilder.Options);
            IAndOr<Test1, CountQuery<Test1>> andOr = GSqlQueryExtension.GetAndOr(where,x => x.Id);
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_with_expression_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1>, IFormats> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.GetAndOr(where, x => x.Id));
        }

        [Fact]
        public void Should_validate_of_IAndOr_CountQuery()
        {
            IAndOr<Test1, CountQuery<Test1>> andOr = new AndOrBase<Test1, CountQuery<Test1>, IFormats>(_countQueryBuilder, _countQueryBuilder.Options);
            try
            {
                GSqlQueryExtension.Validate(andOr,x => x.IsTest);
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_in_IAndOr_CountQuery()
        {
            IAndOr<Test1, CountQuery<Test1>> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.Validate(where, x => x.Id));
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1>, IFormats> where = new AndOrBase<Test1, CountQuery<Test1>, IFormats>(_countQueryBuilder, _countQueryBuilder.Options);
            IAndOr<Test1, CountQuery<Test1>> andOr = GSqlQueryExtension.GetAndOr(where);
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1>, IFormats> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.GetAndOr(where));
        }
    }
}