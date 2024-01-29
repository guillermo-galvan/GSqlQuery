using GSqlQuery.Test.Data;
using GSqlQuery.Test.Extensions;
using GSqlQuery.Test.Models;
using System;
using Xunit;

namespace GSqlQuery.Test
{
    public class EntityTest
    {
        private readonly IFormats _stantements;

        public EntityTest()
        {
            _stantements = new DefaultFormats();
        }

        [Fact]
        public void Throw_an_exception_if_null_key_is_passed()
        {
            Assert.Throws<ArgumentNullException>(() => Test3.Select(null));
        }

        [Fact]
        public void Throw_an_exception_if_null_key_is_passed_1()
        {
            Assert.Throws<ArgumentNullException>(() => Test3.Select(null, (x) => x.IsTests));
        }

        [Fact]
        public void Throw_exception_if_property_is_not_selected()
        {
            Assert.Throws<InvalidOperationException>(() => Test3.Select(_stantements, x => x));
        }

        [Theory]
        [ClassData(typeof(Select_Test3_TestData))]
        public void Retrieve_all_properties_of_the_query(IFormats formats, string query)
        {
            IQueryBuilderWithWhere<SelectQuery<Test3>, IFormats> queryBuilder = Test3.Select(formats);
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Build().Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Theory]
        [ClassData(typeof(Select_Test3_TestData2))]
        public void Retrieve_some_properties_from_the_query(IFormats formats, string query)
        {
            IQueryBuilderWithWhere<SelectQuery<Test3>, IFormats> queryBuilder = Test3.Select(formats, x => new { x.Ids, x.Names, x.Creates });
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Build().Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Theory]
        [ClassData(typeof(Select_Test3_TestData3))]
        public void Should_return_the_query_with_where(IFormats formats, string queryText)
        {
            var query = Test3.Select(formats, x => new { x.Ids, x.Names, x.Creates }).Where().Equal(x => x.IsTests, true).AndEqual(x => x.Ids, 12).Build();
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
        [ClassData(typeof(Insert_Test3_TestData))]
        public void Should_generate_the_insert_query_with_auto_incrementing(IFormats formats, string queryText)
        {
            Test3 test = new Test3(1, null, DateTime.Now, true);
            var query = test.Insert(formats).Build();

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
        [ClassData(typeof(Insert_Test6_TestData))]
        public void Should_generate_the_insert_query(IFormats formats, string queryText)
        {
            Test6 test = new Test6()
            {
                Ids = 1, Names = null, Creates = DateTime.Now, IsTests = true
            };
            var query = test.Insert(formats).Build();

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
        [ClassData(typeof(Insert_Test3_TestData))]
        public void Should_generate_the_insert_static_query_with_auto_incrementing(IFormats formats, string queryText)
        {
            Test3 test = new Test3(1, null, DateTime.Now, true);
            var query = Test3.Insert(formats, test).Build();

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
        [ClassData(typeof(Insert_Test6_TestData))]
        public void Should_generate_the_insert_static_query(IFormats formats, string queryText)
        {
            Test6 test = new Test6()
            {
                Ids = 1,
                Names = null,
                Creates = DateTime.Now,
                IsTests = true
            };
            var query = Test6.Insert(formats, test).Build();

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
        [ClassData(typeof(Update_Test3_TestData))]
        public void Should_generate_the_update_query(IFormats formats, string queryText)
        {
            Test3 test = new Test3(1, null, DateTime.Now, true);
            var query = test.Update(formats, x => new { x.Ids, x.Names, x.Creates }).Set(x => x.IsTests).Build();

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
        [ClassData(typeof(Update_Test3_TestData2))]
        public void Should_generate_the_update_query_with_where(IFormats formats, string queryText)
        {
            Test3 test = new Test3(1, null, DateTime.Now, true);
            var query = test.Update(formats, x => new { x.Ids, x.Names, x.Creates }).Set(x => x.IsTests)
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
        [ClassData(typeof(Update_Test3_TestData))]
        public void Should_generate_the_static_update_query(IFormats formats, string queryText)
        {
            var query = Test3.Update(formats, x => x.Ids, 1).Set(x => x.Names, "Test").Set(x => x.Creates, DateTime.Now).Set(x => x.IsTests, false).Build();

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
        [ClassData(typeof(Update_Test3_TestData2))]
        public void Should_generate_the_static_update_query_with_where(IFormats formats, string queryText)
        {
            var query = Test3.Update(formats, x => x.Ids, 1).Set(x => x.Names, "Test").Set(x => x.Creates, DateTime.Now).Set(x => x.IsTests, false)
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
        [ClassData(typeof(Delete_Test3_TestData))]
        public void Should_generate_the_delete_query(IFormats formats, string queryText)
        {
            var query = Test3.Delete(formats).Build();

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);
            Assert.Equal(queryText, query.Text);
        }

        [Theory]
        [ClassData(typeof(Delete_Test3_TestData2))]
        public void Should_generate_the_delete_where_query(IFormats formats, string queryText)
        {
            var query = Test3.Delete(formats).Where().Equal(x => x.IsTests, true).AndIsNotNull(x => x.Creates).Build();

            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Formats);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);

            string result = query.Text;
            foreach (var item in query.Criteria)
            {
                result = item.ParameterDetails.ParameterReplace(result);
            }
            Assert.Equal(queryText, result);
        }

        [Theory]
        [ClassData(typeof(Count_Test3_TestData))]
        public void Should_generate_the_count_query(IFormats formats, string query)
        {
            IQueryBuilderWithWhere<Test3, SelectQuery<Test3>, IFormats> queryBuilder = Test3.Select(formats, x => x.Ids);
            IQueryBuilderWithWhere<Test3, CountQuery<Test3>, IFormats> countQuery = queryBuilder.Count();
            Assert.NotNull(countQuery);
            Assert.NotEmpty(countQuery.Build().Text);
            Assert.Equal(query, countQuery.Build().Text);
        }

        [Theory]
        [ClassData(typeof(Count_Test3_TestData2))]
        public void Should_generate_some_properties_from_the_count_query(IFormats formats, string query)
        {
            IQueryBuilderWithWhere<Test3, SelectQuery<Test3>, IFormats> queryBuilder = Test3.Select(formats, x => new { x.Ids, x.Names, x.Creates });
            IQueryBuilderWithWhere<Test3, CountQuery<Test3>, IFormats> countQuery = queryBuilder.Count();
            Assert.NotNull(countQuery);
            Assert.NotEmpty(countQuery.Build().Text);
            Assert.Equal(query, countQuery.Build().Text);
        }

        [Theory]
        [ClassData(typeof(Count_Test3_TestData3))]
        public void Should_return_the_count_query_with_where(IFormats formats, string queryText)
        {
            var query = Test3.Select(formats, x => new { x.Ids, x.Names, x.Creates }).Count().Where().Equal(x => x.IsTests, true).AndEqual(x => x.Ids, 12).Build();
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
        [ClassData(typeof(OrderBy_Test3_TestData))]
        public void Should_generate_the_orderby_query(IFormats formats, string query)
        {
            IQueryBuilderWithWhere<Test3, SelectQuery<Test3>, IFormats> queryBuilder = Test3.Select(formats, x => x.Ids);
            IQueryBuilder<OrderByQuery<Test3>, IFormats> countQuery = queryBuilder.OrderBy(x => x.Names, OrderBy.ASC).OrderBy(x => x.Creates, OrderBy.DESC);
            Assert.NotNull(countQuery);
            Assert.NotEmpty(countQuery.Build().Text);
            Assert.Equal(query, countQuery.Build().Text);
        }

        [Theory]
        [ClassData(typeof(OrderBy_Test3_TestData2))]
        public void Should_generate_some_properties_from_the_orderby_query(IFormats formats, string query)
        {
            IQueryBuilderWithWhere<Test3, SelectQuery<Test3>, IFormats> queryBuilder = Test3.Select(formats, x => new { x.Ids, x.Names, x.Creates });
            IQueryBuilder<OrderByQuery<Test3>, IFormats> countQuery = queryBuilder.OrderBy(x => x.Names, OrderBy.ASC).OrderBy(x => x.Creates, OrderBy.DESC);
            Assert.NotNull(countQuery);
            Assert.NotEmpty(countQuery.Build().Text);
            Assert.Equal(query, countQuery.Build().Text);
        }

        [Theory]
        [ClassData(typeof(OrderBy_Test3_TestData3))]
        public void Should_return_the_orderby_query_with_where(IFormats formats, string queryText)
        {
            var queryBuilder = Test3.Select(formats, x => new { x.Ids, x.Names, x.Creates }).Where().Equal(x => x.IsTests, true).AndEqual(x => x.Ids, 12);
            var query = queryBuilder.OrderBy(x => x.Names, OrderBy.ASC).OrderBy(x => x.Creates, OrderBy.DESC).Build();
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
        [ClassData(typeof(Inner_Join_two_tables_TestData))]
        public void Should_generate_the_inner_join_two_tables_query(IFormats formats, string query)
        {
            var result = Test3.Select(formats)
                              .InnerJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
        }

        [Theory]
        [ClassData(typeof(Inner_Join_two_tables_TestData2))]
        public void Should_generate_the_inner_join_two_tables_query2(IFormats formats, string query)
        {
            var result = Test3.Select(formats, x => new { x.Names, x.Ids })
                              .InnerJoin<Test6>(x => new { x.Creates }).Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
        }

        [Theory]
        [ClassData(typeof(Inner_Join_two_tables_with_where_TestData))]
        public void Should_generate_the_inner_join_two_tables_query_with_where(IFormats formats, string query)
        {
            var queryResult = Test3.Select(formats)
                              .InnerJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .Where()
                              .Equal(x => x.Table1.Ids, 1)
                              .AndEqual(x => x.Table2.IsTests, true)
                              .Build();

            string result = queryResult.Text;
            if (queryResult.Criteria != null)
            {
                foreach (var item in queryResult.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
        }

        [Theory]
        [ClassData(typeof(Left_Join_two_tables_TestData))]
        public void Should_generate_the_left_join_two_tables_query(IFormats formats, string query)
        {
            var result = Test3.Select(formats)
                              .LeftJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
        }

        [Theory]
        [ClassData(typeof(Left_Join_two_tables_TestData2))]
        public void Should_generate_the_left_join_two_tables_query2(IFormats formats, string query)
        {
            var result = Test3.Select(formats, x => new { x.Names, x.Ids })
                              .LeftJoin<Test6>(x => new { x.Creates }).Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
        }

        [Theory]
        [ClassData(typeof(Left_Join_two_tables_with_where_TestData))]
        public void Should_generate_the_left_join_two_tables_query_with_where(IFormats formats, string query)
        {
            var queryResult = Test3.Select(formats)
                              .LeftJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .Where()
                              .Equal(x => x.Table1.Ids, 1)
                              .AndEqual(x => x.Table2.IsTests, true)
                              .Build();

            string result = queryResult.Text;
            if (queryResult.Criteria != null)
            {
                foreach (var item in queryResult.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
        }

        [Theory]
        [ClassData(typeof(Right_Join_two_tables_TestData))]
        public void Should_generate_the_right_join_two_tables_query(IFormats formats, string query)
        {
            var result = Test3.Select(formats)
                              .RightJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
        }

        [Theory]
        [ClassData(typeof(Right_Join_two_tables_TestData2))]
        public void Should_generate_the_right_join_two_tables_query2(IFormats formats, string query)
        {
            var result = Test3.Select(formats, x => new { x.Names, x.Ids })
                              .RightJoin<Test6>(x => new { x.Creates }).Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
        }

        [Theory]
        [ClassData(typeof(Right_Join_two_tables_with_where_TestData))]
        public void Should_generate_the_right_join_two_tables_query_with_where(IFormats formats, string query)
        {
            var queryResult = Test3.Select(formats)
                              .RightJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .Where()
                              .Equal(x => x.Table1.Ids, 1)
                              .AndEqual(x => x.Table2.IsTests, true)
                              .Build();

            string result = queryResult.Text;
            if (queryResult.Criteria != null)
            {
                foreach (var item in queryResult.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
        }

        [Theory]
        [ClassData(typeof(Inner_Join_three_tables_TestData))]
        public void Should_generate_the_inner_join_three_tables_query(IFormats formats, string query)
        {
            var result = Test3.Select(formats)
                              .InnerJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .InnerJoin<Test1>().Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
        }

        [Theory]
        [ClassData(typeof(Inner_Join_three_tables_with_where_TestData))]
        public void Should_generate_the_inner_join_three_tables_query_with_where(IFormats formats, string query)
        {
            var queryResult = Test3.Select(formats)
                              .InnerJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .InnerJoin<Test1>().Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Where()
                              .Equal(x => x.Table1.Ids, 1)
                              .AndEqual(x => x.Table2.IsTests, true)
                              .Build();

            string result = queryResult.Text;
            if (queryResult.Criteria != null)
            {
                foreach (var item in queryResult.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
        }

        [Theory]
        [ClassData(typeof(Inner_Join_three_tables_TestData2))]
        public void Should_generate_the_inner_join_three_tables_query2(IFormats formats, string query)
        {
            var result = Test3.Select(formats, x => new { x.Names, x.Ids })
                              .InnerJoin<Test6>(x => new { x.Creates }).Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .InnerJoin<Test1>(x => new { x.IsTest }).Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
        }

        [Theory]
        [ClassData(typeof(Left_Join_three_tables_TestData))]
        public void Should_generate_the_left_join_three_tables_query(IFormats formats, string query)
        {
            var result = Test3.Select(formats)
                              .LeftJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .LeftJoin<Test1>().Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
        }

        [Theory]
        [ClassData(typeof(Left_Join_three_tables_with_where_TestData))]
        public void Should_generate_the_left_join_three_tables_query_with_where(IFormats formats, string query)
        {
            var queryResult = Test3.Select(formats)
                              .LeftJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .LeftJoin<Test1>().Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Where()
                              .Equal(x => x.Table1.Ids, 1)
                              .AndEqual(x => x.Table2.IsTests, true)
                              .Build();

            string result = queryResult.Text;
            if (queryResult.Criteria != null)
            {
                foreach (var item in queryResult.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
        }

        [Theory]
        [ClassData(typeof(Left_Join_three_tables_TestData2))]
        public void Should_generate_the_left_join_three_tables_query2(IFormats formats, string query)
        {
            var result = Test3.Select(formats, x => new { x.Names, x.Ids })
                              .LeftJoin<Test6>(x => new { x.Creates }).Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .LeftJoin<Test1>(x => new { x.IsTest }).Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
        }

        [Theory]
        [ClassData(typeof(Right_Join_three_tables_TestData))]
        public void Should_generate_the_right_join_three_tables_query(IFormats formats, string query)
        {
            var result = Test3.Select(formats)
                              .RightJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .RightJoin<Test1>().Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
        }

        [Theory]
        [ClassData(typeof(Right_Join_three_tables_with_where_TestData))]
        public void Should_generate_the_right_join_three_tables_query_with_where(IFormats formats, string query)
        {
            var queryResult = Test3.Select(formats)
                              .RightJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .RightJoin<Test1>().Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Where()
                              .Equal(x => x.Table1.Ids, 1)
                              .AndEqual(x => x.Table2.IsTests, true)
                              .Build();

            string result = queryResult.Text;
            if (queryResult.Criteria != null)
            {
                foreach (var item in queryResult.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
        }

        [Theory]
        [ClassData(typeof(Right_Join_three_tables_TestData2))]
        public void Should_generate_the_right_join_three_tables_query2(IFormats formats, string query)
        {
            var result = Test3.Select(formats, x => new { x.Names, x.Ids })
                              .RightJoin<Test6>(x => new { x.Creates }).Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .RightJoin<Test1>(x => new { x.IsTest }).Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
        }


        [Theory]
        [ClassData(typeof(Inner_Join_OrderBy_two_tables_TestData))]
        public void Should_generate_the_inner_join_two_tables_orderBy_query(IFormats formats, string query)
        {
            var result = Test3.Select(formats)
                              .InnerJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .OrderBy(x => x.Table1.Creates, OrderBy.DESC)
                              .OrderBy(x => x.Table2.Names, OrderBy.ASC)
                              .Build();

            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
        }

        [Theory]
        [ClassData(typeof(Inner_Join_OrderBy_two_tables_TestData2))]
        public void Should_generate_the_inner_join_two_tables_orderBy_query2(IFormats formats, string query)
        {
            var result = Test3.Select(formats, x => new { x.Names, x.Ids })
                              .InnerJoin<Test6>(x => new { x.Creates }).Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .OrderBy(x => new { x.Table1.Creates, x.Table2.Names }, OrderBy.DESC)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
        }

        [Theory]
        [ClassData(typeof(Inner_Join_OrderBy_two_tables_with_where_TestData))]
        public void Should_generate_the_inner_join_two_tables_orderBy_query_with_where(IFormats formats, string query)
        {
            var queryResult = Test3.Select(formats)
                              .InnerJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .Where()
                              .Equal(x => x.Table1.Ids, 1)
                              .AndEqual(x => x.Table2.IsTests, true)
                              .OrderBy(x => new { x.Table1.Creates, x.Table2.Names }, OrderBy.DESC)
                              .Build();

            string result = queryResult.Text;
            if (queryResult.Criteria != null)
            {
                foreach (var item in queryResult.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
        }

        [Theory]
        [ClassData(typeof(Inner_Join_OrderBy_three_tables_TestData))]
        public void Should_generate_the_inner_join_three_tables_orderBy_query_with_where(IFormats formats, string query)
        {
            var queryResult = Test3.Select(formats)
                              .InnerJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .RightJoin<Test1>().Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Where()
                              .Equal(x => x.Table1.Ids, 1)
                              .AndEqual(x => x.Table2.IsTests, true)
                              .OrderBy(x => new { x.Table2.IsTests, x.Table1.Names, x.Table3.Id }, OrderBy.ASC)
                              .Build();

            string result = queryResult.Text;
            if (queryResult.Criteria != null)
            {
                foreach (var item in queryResult.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
        }


        [Theory]
        [ClassData(typeof(Inner_Join_OrderBy_three_tables_Where_In_TestData))]
        public void Should_generate_the_inner_join_three_tables_orderBy_query_with_where_in(IFormats formats, string query)
        {
            var queryResult = Test3.Select(formats)
                              .InnerJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .RightJoin<Test1>().Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Where()
                              .In(x => x.Table1.Ids, new int[] { 1,2,3 })
                              .AndNotIn(x => x.Table2.Ids, new int[] { 1, 2, 3 })
                              .OrderBy(x => new { x.Table2.IsTests, x.Table1.Names, x.Table3.Id }, OrderBy.ASC)
                              .Build();

            string result = queryResult.Text;
            if (queryResult.Criteria != null)
            {
                foreach (var item in queryResult.Criteria)
                {
                    result = item.ParameterDetails.ParameterReplace(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
        }

    }
}