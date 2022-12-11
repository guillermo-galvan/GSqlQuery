using FluentSQL.Default;
using FluentSQL.Extensions;
using FluentSQL.SearchCriteria;
using FluentSQLTest.Data;
using FluentSQLTest.Extensions;
using FluentSQLTest.Models;

namespace FluentSQLTest
{
    public class EntityTest
    {
        private readonly IStatements _stantements;

        public EntityTest()
        {
            _stantements = new FluentSQL.Default.Statements();
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
            Assert.Throws<InvalidOperationException>(() => Test3.Select(_stantements,x => x));
        }

        [Theory]
        [ClassData(typeof(Select_Test3_TestData))]
        public void Retrieve_all_properties_of_the_query(IStatements statements, string query)
        {
            IQueryBuilderWithWhere<Test3, SelectQuery<Test3>> queryBuilder = Test3.Select(statements);
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Build().Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Theory]
        [ClassData(typeof(Select_Test3_TestData2))]
        public void Retrieve_some_properties_from_the_query(IStatements statements, string query)
        {
            IQueryBuilderWithWhere<Test3, SelectQuery<Test3>> queryBuilder = Test3.Select(statements, x => new { x.Ids, x.Names, x.Creates });
            Assert.NotNull(queryBuilder);
            Assert.NotEmpty(queryBuilder.Build().Text);
            Assert.Equal(query, queryBuilder.Build().Text);
        }

        [Theory]
        [ClassData(typeof(Select_Test3_TestData3))]
        public void Should_return_the_query_with_where(IStatements statements, string queryText)
        {
            var query = Test3.Select(statements, x => new { x.Ids, x.Names, x.Creates }).Where().Equal(x => x.IsTests, true).AndEqual(x => x.Ids, 12).Build();
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
        public void Should_generate_the_insert_query_with_auto_incrementing(IStatements statements, string queryText)
        {
            Test3 test = new (1, null, DateTime.Now, true);
            var query = test.Insert(statements).Build();

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
        public void Should_generate_the_insert_query(IStatements statements, string queryText)
        {
            Test6 test = new(1, null, DateTime.Now, true);
            var query = test.Insert(statements).Build();

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
        public void Should_generate_the_insert_static_query_with_auto_incrementing(IStatements statements, string queryText)
        {
            Test3 test = new(1, null, DateTime.Now, true);
            var query = Test3.Insert(statements, test).Build();

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
        public void Should_generate_the_insert_static_query(IStatements statements, string queryText)
        {
            Test6 test = new(1, null, DateTime.Now, true);
            var query = Test6.Insert(statements, test).Build();

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
        public void Should_generate_the_update_query(IStatements statements, string queryText)
        {
            Test3 test = new(1, null, DateTime.Now, true);
            var query = test.Update(statements, x => new { x.Ids,x.Names,x.Creates}).Set(x => x.IsTests).Build();

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
        public void Should_generate_the_update_query_with_where(IStatements statements, string queryText)
        {
            Test3 test = new(1, null, DateTime.Now, true);
            var query = test.Update(statements, x => new { x.Ids, x.Names, x.Creates }).Set(x => x.IsTests)
                            .Where().Equal(x => x.IsTests,true).AndEqual(x => x.Creates, DateTime.Now).Build();

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
        public void Should_generate_the_static_update_query(IStatements statements, string queryText)
        {
            var query = Test3.Update(statements, x => x.Ids, 1).Set(x => x.Names, "Test").Set(x => x.Creates, DateTime.Now).Set(x => x.IsTests, false).Build();

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
        public void Should_generate_the_static_update_query_with_where(IStatements statements, string queryText)
        {
            var query = Test3.Update(statements, x => x.Ids, 1).Set(x => x.Names, "Test").Set(x => x.Creates, DateTime.Now).Set(x => x.IsTests, false)
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
        public void Should_generate_the_delete_query(IStatements statements, string queryText)
        {
            var query = Test3.Delete(statements).Build();

            Assert.NotNull(query);
            Assert.NotEmpty(query.Text);
            Assert.Equal(queryText, query.Text);
        }

        [Theory]
        [ClassData(typeof(Delete_Test3_TestData2))]
        public void Should_generate_the_delete_where_query(IStatements statements, string queryText)
        {
            var query = Test3.Delete(statements).Where().Equal(x => x.IsTests, true).AndIsNotNull(x => x.Creates).Build();

            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
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

        [Theory]
        [ClassData(typeof(Count_Test3_TestData))]
        public void Should_generate_the_count_query(IStatements statements, string query)
        {
            IQueryBuilderWithWhere<Test3, SelectQuery<Test3>> queryBuilder = Test3.Select(statements,x => x.Ids);
            IQueryBuilderWithWhere<Test3, CountQuery<Test3>> countQuery = queryBuilder.Count();
            Assert.NotNull(countQuery);            
            Assert.NotEmpty(countQuery.Build().Text);
            Assert.Equal(query, countQuery.Build().Text);
        }

        [Theory]
        [ClassData(typeof(Count_Test3_TestData2))]
        public void Should_generate_some_properties_from_the_count_query(IStatements statements, string query)
        {
            IQueryBuilderWithWhere<Test3, SelectQuery<Test3>> queryBuilder = Test3.Select(statements, x => new { x.Ids, x.Names, x.Creates });
            IQueryBuilderWithWhere<Test3, CountQuery<Test3>> countQuery = queryBuilder.Count();
            Assert.NotNull(countQuery);
            Assert.NotEmpty(countQuery.Build().Text);
            Assert.Equal(query, countQuery.Build().Text);
        }

        [Theory]
        [ClassData(typeof(Count_Test3_TestData3))]
        public void Should_return_the_count_query_with_where(IStatements statements, string queryText)
        {
            var queryBuilder = Test3.Select(statements, x => new { x.Ids, x.Names, x.Creates });
            var query = queryBuilder.Count().Where().Equal(x => x.IsTests, true).AndEqual(x => x.Ids, 12).Build();
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
        public void Should_generate_the_orderby_query(IStatements statements, string query)
        {
            IQueryBuilderWithWhere<Test3, SelectQuery<Test3>> queryBuilder = Test3.Select(statements, x => x.Ids);
            IQueryBuilder<Test3, OrderByQuery<Test3>> countQuery = queryBuilder.OrderBy(x => x.Names, OrderBy.ASC).OrderBy(x => x.Creates, OrderBy.DESC);
            Assert.NotNull(countQuery);
            Assert.NotEmpty(countQuery.Build().Text);
            Assert.Equal(query, countQuery.Build().Text);
        }

        [Theory]
        [ClassData(typeof(OrderBy_Test3_TestData2))]
        public void Should_generate_some_properties_from_the_orderby_query(IStatements statements, string query)
        {
            IQueryBuilderWithWhere<Test3, SelectQuery<Test3>> queryBuilder = Test3.Select(statements, x => new { x.Ids, x.Names, x.Creates });
            IQueryBuilder<Test3, OrderByQuery<Test3>> countQuery = queryBuilder.OrderBy(x => x.Names, OrderBy.ASC).OrderBy(x => x.Creates, OrderBy.DESC);
            Assert.NotNull(countQuery);
            Assert.NotEmpty(countQuery.Build().Text);
            Assert.Equal(query, countQuery.Build().Text);
        }

        [Theory]
        [ClassData(typeof(OrderBy_Test3_TestData3))]
        public void Should_return_the_orderby_query_with_where(IStatements statements, string queryText)
        {
            var queryBuilder = Test3.Select(statements, x => new { x.Ids, x.Names, x.Creates }).Where().Equal(x => x.IsTests, true).AndEqual(x => x.Ids, 12);
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

    }
}
