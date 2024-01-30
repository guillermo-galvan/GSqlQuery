using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class UpdateWhereTest
    {
        private readonly Equal<int> _equal;
        private readonly UpdateQueryBuilder<Test1> _queryBuilder;

        public UpdateWhereTest()
        {
            var classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            var columnAttribute = classOptions.PropertyOptions.FirstOrDefault(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            var classOptionsTupla = new ClassOptionsTupla<ColumnAttribute>(classOptions, columnAttribute);
            _equal = new Equal<int>(classOptionsTupla, new DefaultFormats(), 1); 
            var columnsValue = new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) };

            _queryBuilder = new UpdateQueryBuilder<Test1>(new DefaultFormats(), columnsValue, string.Empty);
        }

        [Fact]
        public void Should_add_criteria_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1>, IFormats> query = new AndOrBase<Test1, UpdateQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            Assert.NotNull(query);
            query.Add(_equal);
            Assert.True(true);
        }

        [Fact]
        public void Throw_exception_if_null_ISearchCriteria_is_added_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1>, IFormats> query = new AndOrBase<Test1, UpdateQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            Assert.NotNull(query);
            Assert.Throws<ArgumentNullException>(() => query.Add(null));
        }

        [Fact]
        public void Should_build_the_criteria_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1>, IFormats> query = new AndOrBase<Test1, UpdateQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            Assert.NotNull(query);
            query.Add(_equal);

            var criteria = ((ISearchCriteriaBuilder<UpdateQuery<Test1>>)query).BuildCriteria(_queryBuilder.Options);
            Assert.NotNull(criteria);
            Assert.NotEmpty(criteria);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_with_expression_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1>, IFormats> where = new AndOrBase<Test1, UpdateQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            IAndOr<Test1, UpdateQuery<Test1>> andOr = where.GetAndOr(x => x.Id);
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_with_expression_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1>, IFormats> where = null;
            Assert.Throws<ArgumentNullException>(() => where.GetAndOr(x => x.Id));
        }

        [Fact]
        public void Should_validate_of_IAndOr_UpdateQuery()
        {
            IAndOr<Test1, UpdateQuery<Test1>> andOr = new AndOrBase<Test1, UpdateQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            try
            {
                andOr.Validate(x => x.IsTest);
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
            IAndOr<Test1, UpdateQuery<Test1>> where = null;
            Assert.Throws<ArgumentNullException>(() => where.Validate(x => x.Id));
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1>, IFormats> where = new AndOrBase<Test1, UpdateQuery<Test1>, IFormats>(_queryBuilder, _queryBuilder.Options);
            IAndOr<Test1, UpdateQuery<Test1>> andOr = where.GetAndOr();
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Throw_exception_if_expression_is_null_UpdateQuery()
        {
            AndOrBase<Test1, UpdateQuery<Test1>, IFormats> where = null;
            Assert.Throws<ArgumentNullException>(() => where.GetAndOr());
        }
    }
}