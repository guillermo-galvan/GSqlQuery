using GSqlQuery.Test.Models;
using Xunit;

namespace GSqlQuery.Test.Extensions
{
    public class ComparisonOperatorsExtensionTest
    {
        private readonly IStatements _stantements;

        public ComparisonOperatorsExtensionTest()
        {
            _stantements= new Statements();
        }

        [Fact]
        public void Should_return_IJoinQueryBuilderWithWhere_two_tables_in_equal()
        {
            var result = Test3.Select(_stantements)
                              .InnerJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids);
            Assert.NotNull(result);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
        }

        [Fact]
        public void Should_return_IJoinQueryBuilderWithWhere_two_tables_in_notEqual()
        {
            var result = Test3.Select(_stantements)
                              .InnerJoin<Test6>().NotEqual(x => x.Table1.Ids, x => x.Table2.Ids);
            Assert.NotNull(result);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
        }

        [Fact]
        public void Should_return_IJoinQueryBuilderWithWhere_two_tables_in_greaterThan()
        {
            var result = Test3.Select(_stantements)
                              .InnerJoin<Test6>().GreaterThan(x => x.Table1.Ids, x => x.Table2.Ids);
            Assert.NotNull(result);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
        }

        [Fact]
        public void Should_return_IJoinQueryBuilderWithWhere_two_tables_in_lessThan()
        {
            var result = Test3.Select(_stantements)
                              .InnerJoin<Test6>().LessThan(x => x.Table1.Ids, x => x.Table2.Ids);
            Assert.NotNull(result);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
        }

        [Fact]
        public void Should_return_IJoinQueryBuilderWithWhere_two_tables_in_greaterThanOrEqual()
        {
            var result = Test3.Select(_stantements)
                              .InnerJoin<Test6>().GreaterThanOrEqual(x => x.Table1.Ids, x => x.Table2.Ids);
            Assert.NotNull(result);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
        }

        [Fact]
        public void Should_return_IJoinQueryBuilderWithWhere_two_tables_in_LessThanOrEqual()
        {
            var result = Test3.Select(_stantements)
                              .InnerJoin<Test6>().LessThanOrEqual(x => x.Table1.Ids, x => x.Table2.Ids);
            Assert.NotNull(result);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
        }

        [Fact]
        public void Should_return_IJoinQueryBuilderWithWhere_three_tables_in_equal()
        {
            var result = Test3.Select(_stantements)
                              .InnerJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .RightJoin<Test1>().Equal(x => x.Table2.Ids, x => x.Table3.Id);
            Assert.NotNull(result);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
        }

        [Fact]
        public void Should_return_IJoinQueryBuilderWithWhere_three_tables_in_notEqual()
        {
            var result = Test3.Select(_stantements)
                              .InnerJoin<Test6>().NotEqual(x => x.Table1.Ids, x => x.Table2.Ids)
                              .RightJoin<Test1>().NotEqual(x => x.Table2.Ids, x => x.Table3.Id); ;
            Assert.NotNull(result);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
        }

        [Fact]
        public void Should_return_IJoinQueryBuilderWithWhere_three_tables_in_greaterThan()
        {
            var result = Test3.Select(_stantements)
                              .InnerJoin<Test6>().GreaterThan(x => x.Table1.Ids, x => x.Table2.Ids)
                              .RightJoin<Test1>().GreaterThan(x => x.Table2.Ids, x => x.Table3.Id);
            Assert.NotNull(result);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
        }

        [Fact]
        public void Should_return_IJoinQueryBuilderWithWhere_three_tables_in_lessThan()
        {
            var result = Test3.Select(_stantements)
                              .InnerJoin<Test6>().LessThan(x => x.Table1.Ids, x => x.Table2.Ids)
                              .RightJoin<Test1>().LessThan(x => x.Table2.Ids, x => x.Table3.Id);
            Assert.NotNull(result);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
        }

        [Fact]
        public void Should_return_IJoinQueryBuilderWithWhere_three_tables_in_greaterThanOrEqual()
        {
            var result = Test3.Select(_stantements)
                              .InnerJoin<Test6>().GreaterThanOrEqual(x => x.Table1.Ids, x => x.Table2.Ids)
                              .RightJoin<Test1>().GreaterThanOrEqual(x => x.Table2.Ids, x => x.Table3.Id);
            Assert.NotNull(result);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
        }

        [Fact]
        public void Should_return_IJoinQueryBuilderWithWhere_three_tables_in_LessThanOrEqual()
        {
            var result = Test3.Select(_stantements)
                              .InnerJoin<Test6>().LessThanOrEqual(x => x.Table1.Ids, x => x.Table2.Ids)
                              .RightJoin<Test1>().LessThanOrEqual(x => x.Table2.Ids, x => x.Table3.Id);
            Assert.NotNull(result);
            Assert.NotNull(result.Columns);
            Assert.NotEmpty(result.Columns);
        }

    }
}
