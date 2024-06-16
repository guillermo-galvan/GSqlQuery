using GSqlQuery.Test.Models;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace GSqlQuery.Test.Extensions
{
    public class ComparisonOperatorsExtensionTest
    {
        private readonly QueryOptions _queryOptions;

        public ComparisonOperatorsExtensionTest()
        {
            _queryOptions = new QueryOptions(new DefaultFormats());
        }

        [Fact]
        public void NotEqual_Join_Two_Table()
        {
            var query = Entity<Test1>.Select(_queryOptions).LeftJoin<Test3>().NotEqual(x => x.Table2.Ids, x => x.Table1.Id).Build();
            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id <> Test1.Id;", query.Text);
        }

        [Fact]
        public void GreaterThan_Join_Two_Table()
        {
            var query = Entity<Test1>.Select(_queryOptions).LeftJoin<Test3>().GreaterThan(x => x.Table2.Ids, x => x.Table1.Id).Build();

            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id > Test1.Id;", query.Text);
        }

        [Fact]
        public void LessThan_Join_Two_Table()
        {
            var query = Entity<Test1>.Select(_queryOptions).LeftJoin<Test3>().LessThan(x => x.Table2.Ids, x => x.Table1.Id).Build();
          
            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id < Test1.Id;", query.Text);
        }

        [Fact]
        public void GreaterThanOrEqual_Join_Two_Table()
        {
            var query = Entity<Test1>.Select(_queryOptions).LeftJoin<Test3>().GreaterThanOrEqual(x => x.Table2.Ids, x => x.Table1.Id).Build();
            
            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id >= Test1.Id;", query.Text);
        }

        [Fact]
        public void LessThanOrEqual_Join_Two_Table()
        {
            var query = Entity<Test1>.Select(_queryOptions).LeftJoin<Test3>().LessThanOrEqual(x => x.Table2.Ids, x => x.Table1.Id).Build();

            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id <= Test1.Id;", query.Text);
        }

        [Fact]
        public void NotEqual_Join_Three_Table()
        {
            var query = Entity<Test1>.Select(_queryOptions)
                                            .LeftJoin<Test3>().NotEqual(x => x.Table2.Ids, x => x.Table1.Id)
                                            .RightJoin<Test6>().NotEqual(x => x.Table3.Ids, x => x.Table1.Id).Build();
            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id <> Test1.Id RIGHT JOIN TableName ON TableName.Id <> Test1.Id;", query.Text);
        }

        [Fact]
        public void GreaterThan_Join_Three_Table()
        {
            var query = Entity<Test1>.Select(_queryOptions)
                                            .LeftJoin<Test3>().GreaterThan(x => x.Table2.Ids, x => x.Table1.Id)
                                            .RightJoin<Test6>().GreaterThan(x => x.Table3.Ids, x => x.Table1.Id).Build();
            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id > Test1.Id RIGHT JOIN TableName ON TableName.Id > Test1.Id;", query.Text);
        }

        [Fact]
        public void LessThan_Join_Three_Table()
        {
            var query = Entity<Test1>.Select(_queryOptions)
                                            .LeftJoin<Test3>().LessThan(x => x.Table2.Ids, x => x.Table1.Id)
                                            .RightJoin<Test6>().LessThan(x => x.Table3.Ids, x => x.Table1.Id).Build();

            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id < Test1.Id RIGHT JOIN TableName ON TableName.Id < Test1.Id;", query.Text);
        }

        [Fact]
        public void GreaterThanOrEqual_Join_Three_Table()
        {
            var query = Entity<Test1>.Select(_queryOptions)
                                            .LeftJoin<Test3>().GreaterThanOrEqual(x => x.Table2.Ids, x => x.Table1.Id)
                                            .RightJoin<Test6>().GreaterThanOrEqual(x => x.Table3.Ids, x => x.Table1.Id).Build();
            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id >= Test1.Id RIGHT JOIN TableName ON TableName.Id >= Test1.Id;", query.Text);
        }

        [Fact]
        public void LessThanOrEqual_Join_Three_Table()
        {
            var query = Entity<Test1>.Select(_queryOptions)
                                            .LeftJoin<Test3>().LessThanOrEqual(x => x.Table2.Ids, x => x.Table1.Id)
                                            .RightJoin<Test6>().LessThanOrEqual(x => x.Table3.Ids, x => x.Table1.Id).Build();
            Assert.NotNull(query.Text);
            Assert.Equal("SELECT Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest,Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test1 LEFT JOIN Test3 ON Test3.Id <= Test1.Id RIGHT JOIN TableName ON TableName.Id <= Test1.Id;", query.Text);
        }
    }
}
