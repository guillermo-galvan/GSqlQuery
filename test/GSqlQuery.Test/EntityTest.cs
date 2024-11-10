using GSqlQuery.Extensions;
using GSqlQuery.Test.Data;
using GSqlQuery.Test.Extensions;
using GSqlQuery.Test.Helpers;
using GSqlQuery.Test.Models;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using Xunit;
using Xunit.Abstractions;

namespace GSqlQuery.Test
{
    public class EntityTest
    {
        private readonly QueryOptions _queryOptions;

        public EntityTest()
        {
            _queryOptions = new QueryOptions(new DefaultFormats());
        }

        [Fact]
        public void borrar_despues()
        {
            Test3 test3 = new Test3 ( 1, "Test1", DateTime.Now, true);
            var select = test3.Update(_queryOptions, x => x.Ids).Set(x => x.Names).Set(x => x.Creates).Set(x => x.IsTests)
                            .Where().Equal(x => x.IsTests, true).AndEqual(x => x.Creates, DateTime.Now);

            Stopwatch timeMeasure = new Stopwatch();
            timeMeasure.Start();
            var query = select.Build();
            timeMeasure.Stop();

            Console.WriteLine($"Tiempo: {timeMeasure.Elapsed.TotalMilliseconds} ms");
            test3.IsTests = false;
            test3.Ids = 2;
            var select2 = test3.Update(_queryOptions, x => x.Ids).Set(x => x.Names).Set(x => x.Creates).Set(x => x.IsTests)
                            .Where().Equal(x => x.IsTests, true).AndEqual(x => x.Creates, DateTime.Now);

            Stopwatch timeMeasure2 = new Stopwatch();
            timeMeasure2.Start();
            query = select2.Build();
            timeMeasure2.Stop();

            Console.WriteLine($"Tiempo: {timeMeasure2.Elapsed.TotalMilliseconds} ms");
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
            Assert.Throws<InvalidOperationException>(() => Test3.Select(_queryOptions, x => x).Build());
        }

        [Theory]
        [ClassData(typeof(Select_Test3_TestData))]
        public void Retrieve_all_properties_of_the_query(QueryOptions queryOptions, string query)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates, x.IsTests };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));

            IQueryBuilderWithWhere<SelectQuery<Test3>, QueryOptions> queryBuilder = Test3.Select(queryOptions);
            SelectQuery<Test3> result = queryBuilder.Build();

            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
            Assert.Equal(validateColumns.Count, result.Columns.Count);
            Assert.All(result.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Select_Test3_TestData2))]
        public void Retrieve_some_properties_from_the_query(QueryOptions queryOptions, string query)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));
            IQueryBuilderWithWhere<SelectQuery<Test3>, QueryOptions> queryBuilder = Test3.Select(queryOptions, x => new { x.Ids, x.Names, x.Creates });

            SelectQuery<Test3> result = queryBuilder.Build();

            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
            Assert.Equal(validateColumns.Count, result.Columns.Count);
            Assert.All(result.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Select_Test3_TestData3))]
        public void Should_return_the_query_with_where(QueryOptions queryOptions, string queryText)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));
            var query = Test3.Select(queryOptions, x => new { x.Ids, x.Names, x.Creates }).Where().Equal(x => x.IsTests, true).AndEqual(x => x.Ids, 12).Build();

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);
            Assert.Equal(queryText, result);
            Assert.Equal(validateColumns.Count, query.Columns.Count);
            Assert.All(query.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Insert_Test3_TestData))]
        public void Should_generate_the_insert_query_with_auto_incrementing(QueryOptions queryOptions, string queryText)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates, x.IsTests };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));

            Test3 test = new Test3(1, null, DateTime.Now, true);
            var query = test.Insert(queryOptions).Build();
            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterReplaceInQuery(result);
                }
            }
            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);
            Assert.Equal(queryText, result);
            Assert.Equal(validateColumns.Count, query.Columns.Count);
            Assert.All(query.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Insert_Test6_TestData))]
        public void Should_generate_the_insert_query(QueryOptions queryOptions, string queryText)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates, x.IsTests };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));
            Test6 test = new Test6() { Ids = 1, Names = null, Creates = DateTime.Now, IsTests = true, };
            var query = test.Insert(queryOptions).Build();

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);
            Assert.Equal(queryText, result);
            Assert.Equal(validateColumns.Count, query.Columns.Count);
            Assert.All(query.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Insert_Test3_TestData))]
        public void Should_generate_the_insert_static_query_with_auto_incrementing(QueryOptions queryOptions, string queryText)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates, x.IsTests };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));
            Test3 test = new Test3(1, null, DateTime.Now, true);
            var query = Test3.Insert(queryOptions, test).Build();

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);
            Assert.Equal(queryText, result);
            Assert.Equal(validateColumns.Count, query.Columns.Count);
            Assert.All(query.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Insert_Test6_TestData))]
        public void Should_generate_the_insert_static_query(QueryOptions queryOptions, string queryText)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates, x.IsTests };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));
            Test6 test = new Test6()
            {
                Ids = 1,
                Names = null,
                Creates = DateTime.Now,
                IsTests = true
            };
            var query = Test6.Insert(queryOptions, test).Build();

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);
            Assert.Equal(queryText, result);
            Assert.Equal(validateColumns.Count, query.Columns.Count);
            Assert.All(query.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Update_Test3_TestData))]
        public void Should_generate_the_update_query(QueryOptions queryOptions, string queryText)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates, x.IsTests };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));
            Test3 test = new Test3(1, null, DateTime.Now, true);
            var query = test.Update(queryOptions, x => new { x.Ids, x.Names, x.Creates }).Set(x => x.IsTests).Build();

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);
            Assert.Equal(queryText, result);
            Assert.Equal(validateColumns.Count, query.Columns.Count);
            Assert.All(query.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Update_Test3_TestData2))]
        public void Should_generate_the_update_query_with_where(QueryOptions queryOptions, string queryText)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates, x.IsTests };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));
            Test3 test = new Test3(1, null, DateTime.Now, true);
            var query = test.Update(queryOptions, x => new { x.Ids, x.Names, x.Creates }).Set(x => x.IsTests)
                            .Where().Equal(x => x.IsTests, true).AndEqual(x => x.Creates, DateTime.Now).Build();

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);
            Assert.Equal(queryText, result);
            Assert.Equal(validateColumns.Count, query.Columns.Count);
            Assert.All(query.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Update_Test3_TestData))]
        public void Should_generate_the_static_update_query(QueryOptions queryOptions, string queryText)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates, x.IsTests };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));
            var query = Test3.Update(queryOptions, x => x.Ids, 1).Set(x => x.Names, "Test").Set(x => x.Creates, DateTime.Now).Set(x => x.IsTests, false).Build();

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);
            Assert.Equal(queryText, result);
            Assert.Equal(validateColumns.Count, query.Columns.Count);
            Assert.All(query.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Update_Test3_TestData2))]
        public void Should_generate_the_static_update_query_with_where(QueryOptions queryOptions, string queryText)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates, x.IsTests };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));
            var query = Test3.Update(queryOptions, x => x.Ids, 1).Set(x => x.Names, "Test").Set(x => x.Creates, DateTime.Now).Set(x => x.IsTests, false)
                            .Where().Equal(x => x.IsTests, true).AndEqual(x => x.Creates, DateTime.Now).Build();

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterReplaceInQuery(result);
                }
            }
            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);
            Assert.Equal(queryText, result);
            Assert.Equal(validateColumns.Count, query.Columns.Count);
            Assert.All(query.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Delete_Test3_TestData))]
        public void Should_generate_the_delete_query(QueryOptions queryOptions, string queryText)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates, x.IsTests };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));
            var query = Test3.Delete(queryOptions).Build();

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);
            Assert.Equal(queryText, query.Text);
            Assert.Equal(validateColumns.Count, query.Columns.Count);
            Assert.All(query.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Delete_Test3_TestData2))]
        public void Should_generate_the_delete_where_query(QueryOptions queryOptions, string queryText)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates, x.IsTests };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));
            var query = Test3.Delete(queryOptions).Where().Equal(x => x.IsTests, true).AndIsNotNull(x => x.Creates).Build();

            string result = query.Text;
            foreach (var item in query.Criteria)
            {
                result = item.ParameterReplaceInQuery(result);
            }
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns.Values);
            Assert.NotEmpty(query.Columns.Keys);
            Assert.NotNull(query.QueryOptions);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.Equal(queryText, result);
            Assert.Equal(validateColumns.Count, query.Columns.Count);
            Assert.All(query.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Count_Test3_TestData))]
        public void Should_generate_the_count_query(QueryOptions queryOptions, string query)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));
            IQueryBuilderWithWhere<Test3, SelectQuery<Test3>, QueryOptions> queryBuilder = Test3.Select(queryOptions, x => new { x.Ids });
            IQueryBuilderWithWhere<Test3, CountQuery<Test3>, QueryOptions> countQuery = queryBuilder.Count();
            var queryTmp = countQuery?.Build();

            Assert.NotNull(countQuery);
            Assert.NotEmpty(queryTmp.Text);
            Assert.Equal(query, queryTmp.Text);
            Assert.Equal(validateColumns.Count, queryTmp.Columns.Count);
            Assert.All(queryTmp.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Count_Test3_TestData2))]
        public void Should_generate_some_properties_from_the_count_query(QueryOptions queryOptions, string query)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));
            IQueryBuilderWithWhere<Test3, SelectQuery<Test3>, QueryOptions> queryBuilder = Test3.Select(queryOptions, x => new { x.Ids, x.Names, x.Creates });
            IQueryBuilderWithWhere<Test3, CountQuery<Test3>, QueryOptions> countQuery = queryBuilder.Count();
            var queryTmp = countQuery?.Build();
            Assert.NotNull(countQuery);
            Assert.NotEmpty(queryTmp.Text);
            Assert.Equal(query, queryTmp.Text);
            Assert.Equal(validateColumns.Count, queryTmp.Columns.Count);
            Assert.All(queryTmp.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Count_Test3_TestData3))]
        public void Should_return_the_count_query_with_where(QueryOptions queryOptions, string queryText)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));
            var query = Test3.Select(queryOptions, x => new { x.Ids, x.Names, x.Creates }).Count().Where().Equal(x => x.IsTests, true).AndEqual(x => x.Ids, 12).Build();

            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);
            Assert.Equal(queryText, result);
            Assert.Equal(validateColumns.Count, query.Columns.Count);
            Assert.All(query.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(OrderBy_Test3_TestData))]
        public void Should_generate_the_orderby_query(QueryOptions queryOptions, string query)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));

            IQueryBuilderWithWhere<Test3, SelectQuery<Test3>, QueryOptions> queryBuilder = Test3.Select(queryOptions, x => new { x.Ids });
            IOrderByQueryBuilder<Test3, OrderByQuery<Test3>, QueryOptions> countQuery = queryBuilder.OrderBy(x => new { x.Names }, OrderBy.ASC).OrderBy(x => new { x.Creates }, OrderBy.DESC);
            var queryTmp = countQuery?.Build();

            Assert.NotNull(countQuery);
            Assert.NotEmpty(queryTmp.Text);
            Assert.Equal(query, queryTmp.Text);
            Assert.Equal(validateColumns.Count, queryTmp.Columns.Count);
            Assert.All(queryTmp.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(OrderBy_Test3_TestData2))]
        public void Should_generate_some_properties_from_the_orderby_query(QueryOptions queryOptions, string query)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));
            IQueryBuilderWithWhere<Test3, SelectQuery<Test3>, QueryOptions> queryBuilder = Test3.Select(queryOptions, x => new { x.Ids, x.Names, x.Creates });
            IOrderByQueryBuilder<Test3, OrderByQuery<Test3>, QueryOptions> countQuery = queryBuilder.OrderBy(x => new { x.Names }, OrderBy.ASC).OrderBy(x => new { x.Creates }, OrderBy.DESC);
            var queryTmp = countQuery?.Build();

            Assert.NotNull(countQuery);
            Assert.NotEmpty(queryTmp.Text);
            Assert.Equal(query, queryTmp.Text);
            Assert.Equal(validateColumns.Count, queryTmp.Columns.Count);
            Assert.All(queryTmp.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(OrderBy_Test3_TestData3))]
        public void Should_return_the_orderby_query_with_where(QueryOptions queryOptions, string queryText)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));
            var queryBuilder = Test3.Select(queryOptions, x => new { x.Ids, x.Names, x.Creates }).Where().Equal(x => x.IsTests, true).AndEqual(x => x.Ids, 12);
            var query = queryBuilder.OrderBy(x => new { x.Names }, OrderBy.ASC).OrderBy(x => new { x.Creates }, OrderBy.DESC).Build();


            string result = query.Text;
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria)
                {
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);
            Assert.Equal(queryText, result);
            Assert.Equal(validateColumns.Count, query.Columns.Count);
            Assert.All(query.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Inner_Join_two_tables_TestData))]
        public void Should_generate_the_inner_join_two_tables_query(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable);

            var result = Test3.Select(queryOptions)
                              .InnerJoin<Test6>()
                              .Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .Build();

            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
            Assert.Equal(validateColumns.Count, result.Columns.Count);
            Assert.All(result.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Inner_Join_two_tables_TestData2))]
        public void Should_generate_the_inner_join_two_tables_query2(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Names, x.Ids });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Creates });

            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable);

            var result = Test3.Select(queryOptions, x => new { x.Names, x.Ids })
                              .InnerJoin<Test6>(x => new { x.Creates }).Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .Build();

            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
            Assert.Equal(validateColumns.Count, result.Columns.Count);
            Assert.All(result.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Inner_Join_two_tables_with_where_TestData))]
        public void Should_generate_the_inner_join_two_tables_query_with_where(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable);

            var queryResult = Test3.Select(queryOptions)
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
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
            Assert.Equal(validateColumns.Count, queryResult.Columns.Count);
            Assert.All(queryResult.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Left_Join_two_tables_TestData))]
        public void Should_generate_the_left_join_two_tables_query(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable);

            var result = Test3.Select(queryOptions)
                              .LeftJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
            Assert.Equal(validateColumns.Count, result.Columns.Count);
            Assert.All(result.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Left_Join_two_tables_TestData2))]
        public void Should_generate_the_left_join_two_tables_query2(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Names, x.Ids });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Creates });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable);

            var result = Test3.Select(queryOptions, x => new { x.Names, x.Ids })
                              .LeftJoin<Test6>(x => new { x.Creates }).Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
            Assert.Equal(validateColumns.Count, result.Columns.Count);
            Assert.All(result.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Left_Join_two_tables_with_where_TestData))]
        public void Should_generate_the_left_join_two_tables_query_with_where(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable);

            var queryResult = Test3.Select(queryOptions)
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
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
            Assert.Equal(validateColumns.Count, queryResult.Columns.Count);
            Assert.All(queryResult.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Right_Join_two_tables_TestData))]
        public void Should_generate_the_right_join_two_tables_query(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable);

            var result = Test3.Select(queryOptions)
                              .RightJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
            Assert.Equal(validateColumns.Count, result.Columns.Count);
            Assert.All(result.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Right_Join_two_tables_TestData2))]
        public void Should_generate_the_right_join_two_tables_query2(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Names, x.Ids });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Creates });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable);

            var result = Test3.Select(queryOptions, x => new { x.Names, x.Ids })
                              .RightJoin<Test6>(x => new { x.Creates }).Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
            Assert.Equal(validateColumns.Count, result.Columns.Count);
            Assert.All(result.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Right_Join_two_tables_with_where_TestData))]
        public void Should_generate_the_right_join_two_tables_query_with_where(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable);

            var queryResult = Test3.Select(queryOptions)
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
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
            Assert.Equal(validateColumns.Count, queryResult.Columns.Count);
            Assert.All(queryResult.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Inner_Join_three_tables_TestData))]
        public void Should_generate_the_inner_join_three_tables_query(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var thirdTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>(x => new { x.Id, x.Name, x.Create, x.IsTest });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable, thirdTable);


            var result = Test3.Select(queryOptions)
                              .InnerJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .InnerJoin<Test1>().Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
            Assert.Equal(validateColumns.Count, result.Columns.Count);
            Assert.All(result.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Inner_Join_three_tables_with_where_TestData))]
        public void Should_generate_the_inner_join_three_tables_query_with_where(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var thirdTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>(x => new { x.Id, x.Name, x.Create, x.IsTest });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable, thirdTable);

            var queryResult = Test3.Select(queryOptions)
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
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
            Assert.Equal(validateColumns.Count, queryResult.Columns.Count);
            Assert.All(queryResult.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Inner_Join_three_tables_TestData2))]
        public void Should_generate_the_inner_join_three_tables_query2(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Names, x.Ids });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Creates });
            var thirdTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>(x => new { x.IsTest });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable, thirdTable);

            var result = Test3.Select(queryOptions, x => new { x.Names, x.Ids })
                              .InnerJoin<Test6>(x => new { x.Creates }).Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .InnerJoin<Test1>(x => new { x.IsTest }).Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
            Assert.Equal(validateColumns.Count, result.Columns.Count);
            Assert.All(result.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Left_Join_three_tables_TestData))]
        public void Should_generate_the_left_join_three_tables_query(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var thirdTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>(x => new { x.Id, x.Name, x.Create, x.IsTest });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable, thirdTable);

            var result = Test3.Select(queryOptions)
                              .LeftJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .LeftJoin<Test1>().Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
            Assert.Equal(validateColumns.Count, result.Columns.Count);
            Assert.All(result.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Left_Join_three_tables_with_where_TestData))]
        public void Should_generate_the_left_join_three_tables_query_with_where(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var thirdTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>(x => new { x.Id, x.Name, x.Create, x.IsTest });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable, thirdTable);

            var queryResult = Test3.Select(queryOptions)
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
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
            Assert.Equal(validateColumns.Count, queryResult.Columns.Count);
            Assert.All(queryResult.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Left_Join_three_tables_TestData2))]
        public void Should_generate_the_left_join_three_tables_query2(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Names, x.Ids });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Creates });
            var thirdTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>(x => new { x.IsTest });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable, thirdTable);

            var result = Test3.Select(queryOptions, x => new { x.Names, x.Ids })
                              .LeftJoin<Test6>(x => new { x.Creates }).Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .LeftJoin<Test1>(x => new { x.IsTest }).Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Build();

            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
            Assert.Equal(validateColumns.Count, result.Columns.Count);
            Assert.All(result.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Right_Join_three_tables_TestData))]
        public void Should_generate_the_right_join_three_tables_query(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var thirdTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>(x => new { x.Id, x.Name, x.Create, x.IsTest });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable, thirdTable);

            var result = Test3.Select(queryOptions)
                              .RightJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .RightJoin<Test1>().Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
            Assert.Equal(validateColumns.Count, result.Columns.Count);
            Assert.All(result.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Right_Join_three_tables_with_where_TestData))]
        public void Should_generate_the_right_join_three_tables_query_with_where(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var thirdTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>(x => new { x.Id, x.Name, x.Create, x.IsTest });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable, thirdTable);

            var queryResult = Test3.Select(queryOptions)
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
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
            Assert.Equal(validateColumns.Count, queryResult.Columns.Count);
            Assert.All(queryResult.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Right_Join_three_tables_TestData2))]
        public void Should_generate_the_right_join_three_tables_query2(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Names, x.Ids });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Creates });
            var thirdTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>(x => new { x.IsTest });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable, thirdTable);

            var result = Test3.Select(queryOptions, x => new { x.Names, x.Ids })
                              .RightJoin<Test6>(x => new { x.Creates }).Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .RightJoin<Test1>(x => new { x.IsTest }).Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
            Assert.Equal(validateColumns.Count, result.Columns.Count);
            Assert.All(result.Columns, validateColumns.VerifyColumn);
        }


        [Theory]
        [ClassData(typeof(Inner_Join_OrderBy_two_tables_TestData))]
        public void Should_generate_the_inner_join_two_tables_orderBy_query(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable);

            var result = Test3.Select(queryOptions)
                              .InnerJoin<Test6>()
                              .Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .OrderBy(x => x.Table1.Creates, OrderBy.DESC)
                              .OrderBy(x => x.Table2.Names, OrderBy.ASC)
                              .Build();

            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
            Assert.Equal(validateColumns.Count, result.Columns.Count);
            Assert.All(result.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Inner_Join_OrderBy_two_tables_TestData2))]
        public void Should_generate_the_inner_join_two_tables_orderBy_query2(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Names, x.Ids });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Creates });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable);

            var result = Test3.Select(queryOptions, x => new { x.Names, x.Ids })
                              .InnerJoin<Test6>(x => new { x.Creates }).Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .OrderBy(x => new { x.Table1.Creates, x.Table2.Names }, OrderBy.DESC)
                              .Build();
            Assert.NotEmpty(result.Text);
            Assert.Equal(query, result.Text);
            Assert.Equal(validateColumns.Count, result.Columns.Count);
            Assert.All(result.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Inner_Join_OrderBy_two_tables_with_where_TestData))]
        public void Should_generate_the_inner_join_two_tables_orderBy_query_with_where(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable);

            var queryResult = Test3.Select(queryOptions)
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
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
            Assert.Equal(validateColumns.Count, queryResult.Columns.Count);
            Assert.All(queryResult.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Inner_Join_OrderBy_three_tables_TestData))]
        public void Should_generate_the_inner_join_three_tables_orderBy_query_with_where(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var thirdTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>(x => new { x.Id, x.Name, x.Create, x.IsTest });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable, thirdTable);

            var queryResult = Test3.Select(queryOptions)
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
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
            Assert.Equal(validateColumns.Count, queryResult.Columns.Count);
            Assert.All(queryResult.Columns, validateColumns.VerifyColumn);
        }


        [Theory]
        [ClassData(typeof(Inner_Join_OrderBy_three_tables_Where_In_TestData))]
        public void Should_generate_the_inner_join_three_tables_orderBy_query_with_where_in(QueryOptions queryOptions, string query)
        {
            var firstTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test3, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var seconfTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test6, object>(x => new { x.Ids, x.Names, x.Creates, x.IsTests });
            var thirdTable = ExpressionExtension.GeTQueryOptionsAndMembers<Test1, object>(x => new { x.Id, x.Name, x.Create, x.IsTest });
            ValidateColumnsJoin validateColumns = new ValidateColumnsJoin(queryOptions.Formats, firstTable, seconfTable, thirdTable);

            var queryResult = Test3.Select(queryOptions)
                              .InnerJoin<Test6>().Equal(x => x.Table1.Ids, x => x.Table2.Ids)
                              .RightJoin<Test1>().Equal(x => x.Table2.Ids, x => x.Table3.Id)
                              .Where()
                              .In(x => x.Table1.Ids, [1, 2, 3])
                              .AndNotIn(x => x.Table2.Ids, [1, 2, 3])
                              .OrderBy(x => new { x.Table2.IsTests, x.Table1.Names, x.Table3.Id }, OrderBy.ASC)
                              .Build();

            string result = queryResult.Text;
            if (queryResult.Criteria != null)
            {
                foreach (var item in queryResult.Criteria)
                {
                    result = item.ParameterReplaceInQuery(result);
                }
            }

            Assert.NotNull(queryResult);
            Assert.NotEmpty(queryResult.Text);
            Assert.Equal(query, result);
            Assert.Equal(validateColumns.Count, queryResult.Columns.Count);
            Assert.All(queryResult.Columns, validateColumns.VerifyColumn);
        }

        [Theory]
        [ClassData(typeof(Delete_Test3_TestData3))]
        public void Should_generate_the_delete_query_with_entity(QueryOptions queryOptions, string queryText)
        {
            Expression<Func<Test3, object>> expression = x => new { x.Ids, x.Names, x.Creates, x.IsTests };
            ValidateColumns validateColumns = new ValidateColumns(ExpressionExtension.GetPropertyOptionsCollections(expression));

            Test3 test3 = new Test3(1, "Names", DateTime.Now, true);
            var query = Test3.Delete(queryOptions, test3).Build();

            string result = query.Text;
            foreach (var item in query.Criteria)
            {
                result = item.ParameterReplaceInQuery(result);
            }

            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.QueryOptions);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.Equal(queryText, result);
            Assert.Equal(validateColumns.Count, query.Columns.Count);
            Assert.All(query.Columns, validateColumns.VerifyColumn);
        }

    }
}