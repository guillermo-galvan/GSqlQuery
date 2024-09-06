using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using GSqlQuery.SearchCriteria;
using GSqlQuery.Test.Models;
using System;
using System.Linq.Expressions;
using Xunit;

namespace GSqlQuery.Test.Queries
{
    public class CountWhereTest
    {
        private readonly Equal<Test1, int> _equal;
        private readonly SelectQueryBuilder<Test1> _queryBuilder;
        private readonly CountQueryBuilder<Test1> _countQueryBuilder;
        private readonly ClassOptionsTupla<PropertyOptions> _classOptionsTupla;

        public CountWhereTest()
        {
            var classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            var propertyOptions = classOptions.PropertyOptions[nameof(Test1.Id)];
            _classOptionsTupla = new ClassOptionsTupla<PropertyOptions>(classOptions, propertyOptions);
            Expression<Func<Test1, int>> expression = (x) => x.Id;
            _equal = new Equal<Test1, int>(_classOptionsTupla.ClassOptions, new DefaultFormats(), 1, null, ref expression );
            DynamicQuery dynamicQuery = DynamicQueryCreate.Create((x) => new { x.Id, x.Name, x.Create });
            _queryBuilder = new SelectQueryBuilder<Test1>(dynamicQuery, new QueryOptions(new DefaultFormats()));
            _countQueryBuilder = new CountQueryBuilder<Test1>(_queryBuilder);
        }

        [Fact]
        public void Should_add_criteria_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1>, QueryOptions> query = new AndOrBase<Test1, CountQuery<Test1>, QueryOptions>(_countQueryBuilder, _countQueryBuilder.QueryOptions);
            Assert.NotNull(query);
            query.Add(_equal);
            Assert.True(true);
        }

        [Fact]
        public void Throw_exception_if_null_ISearchCriteria_is_added_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1>, QueryOptions> query = new AndOrBase<Test1, CountQuery<Test1>, QueryOptions>(_countQueryBuilder, _countQueryBuilder.QueryOptions);
            Assert.NotNull(query);
            Assert.Throws<ArgumentNullException>(() => query.Add(null));
        }

        [Fact]
        public void Should_build_the_criteria_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1>, QueryOptions> query = new AndOrBase<Test1, CountQuery<Test1>, QueryOptions>(_countQueryBuilder, _countQueryBuilder.QueryOptions);
            Assert.NotNull(query);
            query.Add(_equal);

            var criteria = ((ISearchCriteriaBuilder)query).Create();
            Assert.NotNull(criteria);
            Assert.NotEmpty(criteria);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_with_expression_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, CountQuery<Test1>, QueryOptions>(_countQueryBuilder, _countQueryBuilder.QueryOptions);
            where.Equal(x => x.Id, 1);
            IAndOr<Test1, CountQuery<Test1>, QueryOptions> andOr = where.AndOr;
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_with_expression_CountQuery_Null()
        {
            AndOrBase<Test1, CountQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, CountQuery<Test1>, QueryOptions>(_countQueryBuilder, _countQueryBuilder.QueryOptions);
            IAndOr<Test1, CountQuery<Test1>, QueryOptions> andOr = where.AndOr;
            Assert.Null(andOr);
        }

        [Fact]
        public void Should_validate_of_IAndOr_CountQuery()
        {
            IAndOr<Test1, CountQuery<Test1>, QueryOptions> andOr = new AndOrBase<Test1, CountQuery<Test1>, QueryOptions>(_countQueryBuilder, _countQueryBuilder.QueryOptions);
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
            IAndOr<Test1, CountQuery<Test1>, QueryOptions> where = null;
            Assert.Throws<ArgumentNullException>(() => GSqlQueryExtension.Validate(where, x => x.Id));
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_CountQuery()
        {
            AndOrBase<Test1, CountQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, CountQuery<Test1>, QueryOptions>(_countQueryBuilder, _countQueryBuilder.QueryOptions);
            where.Equal(x => x.Id, 1);
            IAndOr<Test1, CountQuery<Test1>, QueryOptions> andOr = where.AndOr;
            Assert.NotNull(andOr);
        }

        [Fact]
        public void Should_get_the_IAndOr_interface_CountQuery_Null()
        {
            AndOrBase<Test1, CountQuery<Test1>, QueryOptions> where = new AndOrBase<Test1, CountQuery<Test1>, QueryOptions>(_countQueryBuilder, _countQueryBuilder.QueryOptions);
            IAndOr<Test1, CountQuery<Test1>, QueryOptions> andOr = where.AndOr;
            Assert.Null(andOr);
        }
    }
}