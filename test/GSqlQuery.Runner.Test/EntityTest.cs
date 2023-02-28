using GSqlQuery.Runner.Test.Data;
using GSqlQuery.Runner.Test.Extensions;
using GSqlQuery.Runner.Test.Models;
using System;
using System.Data.Common;
using Xunit;

namespace GSqlQuery.Runner.Test
{
    public class EntityTest
    {
        public EntityTest()
        { }

        [Fact]
        public void Throw_an_exception_if_null_key_is_passed()
        {
            ConnectionOptions<DbConnection> connectionOptions = null;
            Assert.Throws<ArgumentNullException>(() => Test3.Select(connectionOptions));
        }

        [Fact]
        public void Throw_an_exception_if_null_key_is_passed_1()
        {
            ConnectionOptions<DbConnection> connectionOptions = null;
            Assert.Throws<ArgumentNullException>(() => Test3.Select(connectionOptions, (x) => x.IsTests));
        }

        [Fact]
        public void Throw_exception_if_property_is_not_selected()
        {
            ConnectionOptions<DbConnection> connectionOptions = new ConnectionOptions<DbConnection>(new Models.Statements(), LoadFluentOptions.GetDatabaseManagmentMock());
            Assert.Throws<InvalidOperationException>(() => Test3.Select(connectionOptions, x => x));
        }

        [Theory]
        [ClassData(typeof(Select_Test3_TestData_ConnectionOptions))]
        public void Should_generate_the_static_select_query(ConnectionOptions<DbConnection> connection, string query)
        {
            var queryBuilder = Test3.Select(connection);
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Build().Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Theory]
        [ClassData(typeof(Select_Test3_TestData2_ConnectionOptions))]
        public void Should_generate_the_static_select_query2(ConnectionOptions<DbConnection> connection, string query)
        {
            var queryBuilder = Test3.Select(connection, x => new { x.Ids, x.Names, x.Creates });
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Build().Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Theory]
        [ClassData(typeof(Select_Test3_TestData3_ConnectionOptions))]
        public void Should_generate_the_static_select_query3(ConnectionOptions<DbConnection> connection, string queryText)
        {
            var query = Test3.Select(connection, x => new { x.Ids, x.Names, x.Creates }).Where().Equal(x => x.IsTests, true).AndEqual(x => x.Ids, 12).Build();
            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }

            Assert.Equal(queryText, result);
        }

        [Theory]
        [ClassData(typeof(Insert_Test3_TestData_ConnectionOptions))]
        public void Should_generate_the_insert_query_with_auto_incrementing2(ConnectionOptions<DbConnection> connection, string queryText)
        {
            Test3 test = new Test3(1, null, DateTime.Now, true);
            var query = test.Insert(connection).Build();

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }
            Assert.Equal(queryText, result);
        }

        [Theory]
        [ClassData(typeof(Insert_Test6_TestData_ConnectionOptions))]
        public void Should_generate_the_insert_query2(ConnectionOptions<DbConnection> connection, string queryText)
        {
            Test6 test = new Test6(1, null, DateTime.Now, true);
            var query = test.Insert(connection).Build();

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }
            Assert.Equal(queryText, result);
        }

        [Theory]
        [ClassData(typeof(Insert_Test3_TestData_ConnectionOptions))]
        public void Should_generate_the_insert_static_query_with_auto_incrementing2(ConnectionOptions<DbConnection> connection, string queryText)
        {
            Test3 test = new Test3(1, null, DateTime.Now, true);
            var query = Test3.Insert(connection, test).Build();

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }
            Assert.Equal(queryText, result);
        }

        [Theory]
        [ClassData(typeof(Insert_Test6_TestData_ConnectionOptions))]
        public void Should_generate_the_insert_static_query2(ConnectionOptions<DbConnection> connection, string queryText)
        {
            Test6 test = new Test6(1, null, DateTime.Now, true);
            var query = Test6.Insert(connection, test).Build();

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }
            Assert.Equal(queryText, result);
        }

        [Theory]
        [ClassData(typeof(Update_Test3_TestData_Connection))]
        public void Should_generate_the_update_query2(ConnectionOptions<DbConnection> connectionOptions, string queryText)
        {
            Test3 test = new Test3(1, null, DateTime.Now, true);
            var query = test.Update(connectionOptions, x => new { x.Ids, x.Names, x.Creates }).Set(x => x.IsTests).Build();

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }
            Assert.Equal(queryText, result);
        }

        [Theory]
        [ClassData(typeof(Update_Test3_TestData2_Connection))]
        public void Should_generate_the_update_query_with_where2(ConnectionOptions<DbConnection> connectionOptions, string queryText)
        {
            Test3 test = new Test3(1, null, DateTime.Now, true);
            var query = test.Update(connectionOptions, x => new { x.Ids, x.Names, x.Creates }).Set(x => x.IsTests)
                            .Where().Equal(x => x.IsTests, true).AndEqual(x => x.Creates, DateTime.Now).Build();

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }
            Assert.Equal(queryText, result);
        }

        [Theory]
        [ClassData(typeof(Update_Test3_TestData_Connection))]
        public void Should_generate_the_static_update_query2(ConnectionOptions<DbConnection> connectionOptions, string queryText)
        {
            var query = Test3.Update(connectionOptions, x => x.Ids, 1).Set(x => x.Names, "Test").Set(x => x.Creates, DateTime.Now).Set(x => x.IsTests, false).Build();

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }
            Assert.Equal(queryText, result);
        }

        [Theory]
        [ClassData(typeof(Update_Test3_TestData2_Connection))]
        public void Should_generate_the_static_update_query_with_where2(ConnectionOptions<DbConnection> connectionOptions, string queryText)
        {
            var query = Test3.Update(connectionOptions, x => x.Ids, 1).Set(x => x.Names, "Test").Set(x => x.Creates, DateTime.Now).Set(x => x.IsTests, false)
                            .Where().Equal(x => x.IsTests, true).AndEqual(x => x.Creates, DateTime.Now).Build();

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }
            Assert.Equal(queryText, result);
        }


        [Theory]
        [ClassData(typeof(Delete_Test3_TestData_Connection))]
        public void Should_generate_the_delete_query2(ConnectionOptions<DbConnection> connectionOptions, string queryText)
        {
            var query = Test3.Delete(connectionOptions).Build();

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);
            Assert.Equal(queryText, query.Text);
        }

        [Theory]
        [ClassData(typeof(Delete_Test3_TestData2_Connection))]
        public void Should_generate_the_delete_where_query2(ConnectionOptions<DbConnection> connectionOptions, string queryText)
        {
            var query = Test3.Delete(connectionOptions).Where().Equal(x => x.IsTests, true).AndIsNotNull(x => x.Creates).Build();

            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.Statements);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);

            string result = query.Text;
            foreach (var item in query.Criteria)
            {
                result = item.ParameterDetails.ParameterReplace(result);
            }
            Assert.Equal(queryText, result);
        }

        [Fact]
        public void Should_generate_the_inner_join_two_tables_query()
        {
            var connection = new ConnectionOptions<DbConnection>(new Statements(), LoadFluentOptions.GetDatabaseManagmentMock());

            //var tmp = Test3.Select(connection).InnerJoin<Test6>().Equal(x => x.Table1.IsTests, x => x.Table2.IsTests).AndEqual(x => x.Table1.Ids, x => x.Table2.Ids).Build();


            //var tmp = Test3.Select(connection).InnerJoin<Test6>().Equal(x => x.Table1.IsTests, x => x.Table2.IsTests).AndEqual(x => x.Table1.Ids, x => x.Table2.Ids)
            //                                  .InnerJoin<Test1>().Equal(x => x.Table1.IsTests, x => x.Table3.IsTest).AndEqual(x => x.Table1.Ids, x => x.Table2.Ids).Build();
        }
    }
}
