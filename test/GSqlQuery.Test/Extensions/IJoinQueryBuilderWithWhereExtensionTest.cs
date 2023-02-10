using GSqlQuery.Test.Models;
using Xunit;

namespace GSqlQuery.Test.Extensions
{
    public class IJoinQueryBuilderWithWhereExtensionTest
    {
        private readonly IStatements _stantements;

        public IJoinQueryBuilderWithWhereExtensionTest()
        {
            _stantements= new Statements();
        }

        [Fact]
        public void IJoinQueryBuilderWithWhere_equal_two_tables()
        {
            var result1 = Test3.Select(_stantements).InnerJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids).AndEqual(x => x.Table1.IsTests, x => x.Table2.IsTests);
            var result2 = Test3.Select(_stantements).InnerJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids).OrEqual(x => x.Table1.IsTests, x => x.Table2.IsTests);
            
            Assert.NotNull(result1);
            Assert.NotNull(result1.Columns);
            Assert.NotEmpty(result1.Columns);

            Assert.NotNull(result2);
            Assert.NotNull(result2.Columns);
            Assert.NotEmpty(result2.Columns);
        }

        [Fact]
        public void IJoinQueryBuilderWithWhere_notequal_two_tables()
        {
            var result1 = Test3.Select(_stantements).LeftJoin<Test6>().NotEqual(x => x.Table1.Ids, x => x.Table2.Ids).AndNotEqual(x => x.Table1.IsTests, x => x.Table2.IsTests);
            var result2 = Test3.Select(_stantements).LeftJoin<Test6>().NotEqual(x => x.Table1.Ids, x => x.Table2.Ids).OrNotEqual(x => x.Table1.IsTests, x => x.Table2.IsTests);

            Assert.NotNull(result1);
            Assert.NotNull(result1.Columns);
            Assert.NotEmpty(result1.Columns);

            Assert.NotNull(result2);
            Assert.NotNull(result2.Columns);
            Assert.NotEmpty(result2.Columns);
        }

        [Fact]
        public void IJoinQueryBuilderWithWhere_greaterThan_two_tables()
        {
            var result1 = Test3.Select(_stantements).RightJoin<Test6>().GreaterThan(x => x.Table1.Ids, x => x.Table2.Ids).AndGreaterThan(x => x.Table1.IsTests, x => x.Table2.IsTests);
            var result2 = Test3.Select(_stantements).RightJoin<Test6>().GreaterThan(x => x.Table1.Ids, x => x.Table2.Ids).OrGreaterThan(x => x.Table1.IsTests, x => x.Table2.IsTests);

            Assert.NotNull(result1);
            Assert.NotNull(result1.Columns);
            Assert.NotEmpty(result1.Columns);

            Assert.NotNull(result2);
            Assert.NotNull(result2.Columns);
            Assert.NotEmpty(result2.Columns);
        }

        [Fact]
        public void IJoinQueryBuilderWithWhere_lessThan_two_tables()
        {
            var result1 = Test3.Select(_stantements).RightJoin<Test6>().LessThan(x => x.Table1.Ids, x => x.Table2.Ids).AndLessThan(x => x.Table1.IsTests, x => x.Table2.IsTests);
            var result2 = Test3.Select(_stantements).RightJoin<Test6>().LessThan(x => x.Table1.Ids, x => x.Table2.Ids).OrLessThan(x => x.Table1.IsTests, x => x.Table2.IsTests);

            Assert.NotNull(result1);
            Assert.NotNull(result1.Columns);
            Assert.NotEmpty(result1.Columns);

            Assert.NotNull(result2);
            Assert.NotNull(result2.Columns);
            Assert.NotEmpty(result2.Columns);
        }

        [Fact]
        public void IJoinQueryBuilderWithWhere_GreaterThanOrEqual_two_tables()
        {
            var result1 = Test3.Select(_stantements).RightJoin<Test6>().GreaterThanOrEqual(x => x.Table1.Ids, x => x.Table2.Ids).AndGreaterThanOrEqual(x => x.Table1.IsTests, x => x.Table2.IsTests);
            var result2 = Test3.Select(_stantements).RightJoin<Test6>().GreaterThanOrEqual(x => x.Table1.Ids, x => x.Table2.Ids).OrGreaterThanOrEqual(x => x.Table1.IsTests, x => x.Table2.IsTests);

            Assert.NotNull(result1);
            Assert.NotNull(result1.Columns);
            Assert.NotEmpty(result1.Columns);

            Assert.NotNull(result2);
            Assert.NotNull(result2.Columns);
            Assert.NotEmpty(result2.Columns);
        }

        [Fact]
        public void IJoinQueryBuilderWithWhere_LessThanOrEqual_two_tables()
        {
            var result1 = Test3.Select(_stantements).RightJoin<Test6>().LessThanOrEqual(x => x.Table1.Ids, x => x.Table2.Ids).AndLessThanOrEqual(x => x.Table1.IsTests, x => x.Table2.IsTests);
            var result2 = Test3.Select(_stantements).RightJoin<Test6>().LessThanOrEqual(x => x.Table1.Ids, x => x.Table2.Ids).OrLessThanOrEqual(x => x.Table1.IsTests, x => x.Table2.IsTests);

            Assert.NotNull(result1);
            Assert.NotNull(result1.Columns);
            Assert.NotEmpty(result1.Columns);

            Assert.NotNull(result2);
            Assert.NotNull(result2.Columns);
            Assert.NotEmpty(result2.Columns);
        }

        [Fact]
        public void IJoinQueryBuilderWithWhere_equal_three_tables()
        {
            var result1 = Test3.Select(_stantements)
                               .InnerJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids).AndEqual(x => x.Table1.IsTests, x => x.Table2.IsTests)
                               .InnerJoin<Test1>().Equal(x => x.Table2.Ids, x => x.Table3.Id).AndEqual(x => x.Table3.IsTest, x => x.Table2.IsTests);
            var result2 = Test3.Select(_stantements)
                               .InnerJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids).OrEqual(x => x.Table1.IsTests, x => x.Table2.IsTests)
                               .InnerJoin<Test1>().Equal(x => x.Table2.Ids, x => x.Table3.Id).OrEqual(x => x.Table3.IsTest, x => x.Table2.IsTests);

            Assert.NotNull(result1);
            Assert.NotNull(result1.Columns);
            Assert.NotEmpty(result1.Columns);

            Assert.NotNull(result2);
            Assert.NotNull(result2.Columns);
            Assert.NotEmpty(result2.Columns);
        }

        [Fact]
        public void IJoinQueryBuilderWithWhere_notequal_three_tables()
        {
            var result1 = Test3.Select(_stantements)
                               .LeftJoin<Test6>().NotEqual(x => x.Table1.Ids, x => x.Table2.Ids).AndNotEqual(x => x.Table1.IsTests, x => x.Table2.IsTests)
                               .LeftJoin<Test1>().NotEqual(x => x.Table2.Ids, x => x.Table3.Id).AndNotEqual(x => x.Table3.IsTest, x => x.Table2.IsTests);
            var result2 = Test3.Select(_stantements)
                               .LeftJoin<Test6>().NotEqual(x => x.Table1.Ids, x => x.Table2.Ids).OrNotEqual(x => x.Table1.IsTests, x => x.Table2.IsTests)
                               .LeftJoin<Test1>().NotEqual(x => x.Table2.Ids, x => x.Table3.Id).OrNotEqual(x => x.Table3.IsTest, x => x.Table2.IsTests); 

            Assert.NotNull(result1);
            Assert.NotNull(result1.Columns);
            Assert.NotEmpty(result1.Columns);

            Assert.NotNull(result2);
            Assert.NotNull(result2.Columns);
            Assert.NotEmpty(result2.Columns);
        }

        [Fact]
        public void IJoinQueryBuilderWithWhere_greaterThan_three_tables()
        {
            var result1 = Test3.Select(_stantements)
                               .RightJoin<Test6>().GreaterThan(x => x.Table1.Ids, x => x.Table2.Ids).AndGreaterThan(x => x.Table1.IsTests, x => x.Table2.IsTests)
                               .RightJoin<Test1>().GreaterThan(x => x.Table2.Ids, x => x.Table3.Id).AndGreaterThan(x => x.Table3.IsTest, x => x.Table2.IsTests);
            var result2 = Test3.Select(_stantements)
                               .RightJoin<Test6>().GreaterThan(x => x.Table1.Ids, x => x.Table2.Ids).OrGreaterThan(x => x.Table1.IsTests, x => x.Table2.IsTests)
                               .RightJoin<Test1>().GreaterThan(x => x.Table2.Ids, x => x.Table3.Id).OrGreaterThan(x => x.Table3.IsTest, x => x.Table2.IsTests);

            Assert.NotNull(result1);
            Assert.NotNull(result1.Columns);
            Assert.NotEmpty(result1.Columns);

            Assert.NotNull(result2);
            Assert.NotNull(result2.Columns);
            Assert.NotEmpty(result2.Columns);
        }

        [Fact]
        public void IJoinQueryBuilderWithWhere_lessThan_three_tables()
        {
            var result1 = Test3.Select(_stantements)
                               .RightJoin<Test6>().LessThan(x => x.Table1.Ids, x => x.Table2.Ids).AndLessThan(x => x.Table1.IsTests, x => x.Table2.IsTests)
                               .RightJoin<Test1>().LessThan(x => x.Table2.Ids, x => x.Table3.Id).AndLessThan(x => x.Table3.IsTest, x => x.Table2.IsTests);
            var result2 = Test3.Select(_stantements)
                               .RightJoin<Test6>().LessThan(x => x.Table1.Ids, x => x.Table2.Ids).OrLessThan(x => x.Table1.IsTests, x => x.Table2.IsTests)
                               .RightJoin<Test1>().LessThan(x => x.Table2.Ids, x => x.Table3.Id).OrLessThan(x => x.Table3.IsTest, x => x.Table2.IsTests);

            Assert.NotNull(result1);
            Assert.NotNull(result1.Columns);
            Assert.NotEmpty(result1.Columns);

            Assert.NotNull(result2);
            Assert.NotNull(result2.Columns);
            Assert.NotEmpty(result2.Columns);
        }

        [Fact]
        public void IJoinQueryBuilderWithWhere_GreaterThanOrEqual_three_tables()
        {
            var result1 = Test3.Select(_stantements)
                               .RightJoin<Test6>().GreaterThanOrEqual(x => x.Table1.Ids, x => x.Table2.Ids).AndGreaterThanOrEqual(x => x.Table1.IsTests, x => x.Table2.IsTests)
                               .RightJoin<Test1>().GreaterThanOrEqual(x => x.Table2.Ids, x => x.Table3.Id).AndGreaterThanOrEqual(x => x.Table3.IsTest, x => x.Table2.IsTests);
            var result2 = Test3.Select(_stantements)
                               .RightJoin<Test6>().GreaterThanOrEqual(x => x.Table1.Ids, x => x.Table2.Ids).OrGreaterThanOrEqual(x => x.Table1.IsTests, x => x.Table2.IsTests)
                               .RightJoin<Test1>().GreaterThanOrEqual(x => x.Table2.Ids, x => x.Table3.Id).OrGreaterThanOrEqual(x => x.Table3.IsTest, x => x.Table2.IsTests);

            Assert.NotNull(result1);
            Assert.NotNull(result1.Columns);
            Assert.NotEmpty(result1.Columns);

            Assert.NotNull(result2);
            Assert.NotNull(result2.Columns);
            Assert.NotEmpty(result2.Columns);
        }

        [Fact]
        public void IJoinQueryBuilderWithWhere_LessThanOrEqual_three_tables()
        {
            var result1 = Test3.Select(_stantements)
                               .RightJoin<Test6>().LessThanOrEqual(x => x.Table1.Ids, x => x.Table2.Ids).AndLessThanOrEqual(x => x.Table1.IsTests, x => x.Table2.IsTests)
                               .RightJoin<Test1>().LessThanOrEqual(x => x.Table2.Ids, x => x.Table3.Id).AndLessThanOrEqual(x => x.Table3.IsTest, x => x.Table2.IsTests);
            var result2 = Test3.Select(_stantements)
                               .RightJoin<Test6>().LessThanOrEqual(x => x.Table1.Ids, x => x.Table2.Ids).OrLessThanOrEqual(x => x.Table1.IsTests, x => x.Table2.IsTests)
                               .RightJoin<Test1>().LessThanOrEqual(x => x.Table2.Ids, x => x.Table3.Id).OrLessThanOrEqual(x => x.Table3.IsTest, x => x.Table2.IsTests);

            Assert.NotNull(result1);
            Assert.NotNull(result1.Columns);
            Assert.NotEmpty(result1.Columns);

            Assert.NotNull(result2);
            Assert.NotNull(result2.Columns);
            Assert.NotEmpty(result2.Columns);
        }
    }
}
