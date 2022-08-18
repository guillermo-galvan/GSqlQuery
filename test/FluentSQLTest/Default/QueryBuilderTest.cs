using FluentSQL.Default;
using FluentSQL.Models;
using FluentSQLTest.Models;

namespace FluentSQLTest.Default
{
    public class QueryBuilderTest
    {
        [Fact]
        public void Properties_cannot_be_null()
        {
            QueryBuilder<Test1> queryBuilder = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new FluentSQL.Default.Statements(), QueryType.Select);

            Assert.NotNull(queryBuilder);            
            Assert.NotNull(queryBuilder.Statements);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters()
        {
            Assert.Throws<ArgumentNullException>(() => new QueryBuilder<Test1>(null, new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, new FluentSQL.Default.Statements(), QueryType.Select));
            Assert.Throws<ArgumentNullException>(() => new QueryBuilder<Test1>(new ClassOptions(typeof(Test1)), null, new FluentSQL.Default.Statements(), QueryType.Select));
            Assert.Throws<ArgumentNullException>(() => new QueryBuilder<Test1>(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) }, null, QueryType.Select));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface()
        {
            QueryBuilder<Test1> queryBuilder = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new FluentSQL.Default.Statements(), QueryType.Select);
            IWhere<Test1> where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_insert_query()
        {
            QueryBuilder<Test3> queryBuilder = new(new ClassOptions(typeof(Test3)), new List<string> { nameof(Test3.Ids), nameof(Test3.Names), nameof(Test3.Creates), nameof(Test3.Creates) },
                new FluentSQL.Default.Statements(), QueryType.Insert, new Test3(1, null, DateTime.Now, true));
            IQuery<Test3> query = queryBuilder.Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
        }

        [Fact]
        public void Should_return_an_update_query()
        {
            Dictionary<ColumnAttribute, object?> columnsValue = new();
            columnsValue.Add(new ColumnAttribute("Create"), DateTime.Now.Ticks);
            columnsValue.Add(new ColumnAttribute("Id"), "test");
            columnsValue.Add(new ColumnAttribute(nameof(Test3.IsTests)), true);

            QueryBuilder<Test3> queryBuilder = new(new ClassOptions(typeof(Test3)), new FluentSQL.Default.Statements(), QueryType.Update, columnsValue);
            IQuery<Test3> query = queryBuilder.Build();
            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);

        }

        [Fact]
        public void Should_return_an_delete_query()
        {
            QueryBuilder<Test1> queryBuilder = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new FluentSQL.Default.Statements(), QueryType.Delete);
            IQuery<Test1> query = queryBuilder.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Statements);
            Assert.Null(query.Criteria);
        }
    }
}
