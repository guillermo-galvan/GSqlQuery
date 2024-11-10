using GSqlQuery.Runner.Test.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Xunit;

namespace GSqlQuery.Runner.Test.DataBase
{
    public class DatabaseManagementEventsTest
    {
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;
        public DatabaseManagementEventsTest()
        {
            _connectionOptions = new ConnectionOptions<IDbConnection>(new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void GetParameter()
        {
            var query = EntityExecute<Test1>.Select(_connectionOptions).Build();

            Queue<ParameterDetail> parameters = new Queue<ParameterDetail>();

            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria.Where(x => x.ParameterDetails != null))
                {
                    foreach (var item2 in item.ParameterDetails)
                    {
                        parameters.Enqueue(item2);
                    }
                }
            }

            var events = new TestDatabaseManagmentEvents();

            var result = events.GetParameter<Test1>(parameters);
            Assert.NotNull(result);
            Assert.Equal(parameters.Count, result.Count());
        }

        [Fact]
        public void OnGetParameter()
        {
            var query = EntityExecute<Test1>.Select(_connectionOptions).Build();

            Queue<ParameterDetail> parameters = new Queue<ParameterDetail>();
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria.Where(x => x.ParameterDetails != null))
                {
                    foreach (var item2 in item.ParameterDetails)
                    {
                        parameters.Enqueue(item2);
                    }
                }
            }

            var events = new TestDatabaseManagmentEvents();

            var result = events.GetParameter<Test1>(parameters);
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void WriteTrace()
        {
            var events = new TestDatabaseManagmentEvents() { IsTraceActive = true };

            events.WriteTrace("test", new object[] { "test"});
        }

        [Fact]
        public void GetTransformTo_join_query()
        {
            IQuery<Join<Test1, Test3>, ConnectionOptions<IDbConnection>> query = EntityExecute<Test1>.Select(_connectionOptions).InnerJoin<Test3>().Equal(x => x.Table1.Id, x => x.Table2.Ids).Build();
            var events = new TestDatabaseManagmentEvents();

            var result = events.GetTransformTo<Join<Test1,Test3>, DbDataReader>(ClassOptionsFactory.GetClassOptions(typeof(Join<Test1, Test3>)));
            Assert.IsType<Transforms.JoinTransformTo<Join<Test1, Test3>, DbDataReader>>(result);
        }

        [Fact]
        public void TransformToByField_query()
        {
            var events = new TestDatabaseManagmentEvents();
            var result = events.GetTransformTo<Test5, DbDataReader>(ClassOptionsFactory.GetClassOptions(typeof(Test5)));
            Assert.IsType<Transforms.TransformToByField<Test5, DbDataReader>>(result);
        }

        [Fact]
        public void TransformToByConstructor_query()
        {
            var events = new TestDatabaseManagmentEvents();
            var result = events.GetTransformTo<Test1, DbDataReader>(ClassOptionsFactory.GetClassOptions(typeof(Test1)));
            Assert.IsType<Transforms.TransformToByConstructor<Test1, DbDataReader>>(result);
        }
    }
}
