using Xunit;
using GSqlQuery.Runner.Test.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;

namespace GSqlQuery.Runner.Test.DataBase
{
    public class DatabaseManagementEventsTest
    {
        [Fact]
        public void GetParameter()
        {
            var query = EntityExecute<Test1>.Select(new Statements()).Build();

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

            var events = new DatabaseManagementEvents();

            var result = events.GetParameter<Test1>(parameters);
            Assert.NotNull(result);
            Assert.Equal(parameters.Count, result.Count());
        }

        [Fact]
        public void OnGetParameter()
        {
            var query = EntityExecute<Test1>.Select(new Statements()).Build();

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

            var events = new DatabaseManagementEvents();

            var result = events.GetParameter<Test1>(parameters);
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void WriteTrace()
        {
            var loggerMock = new Mock<ILogger<DatabaseManagementEvents>>();
            var events = new DatabaseManagementEvents() { IsTraceActive = true};

            events.WriteTrace(loggerMock.Object, "test", new object[] { "test"});

            loggerMock.Verify();
        }

        [Fact]
        public void OnWriteTrace()
        {
            var loggerMock = new Mock<ILogger<DatabaseManagementEvents>>();
            var events = new DatabaseManagementEvents() { IsTraceActive = true };

            events.OnWriteTrace(true,loggerMock.Object, "test", new object[] { "test" });

            loggerMock.Verify();
        }

        [Fact]
        public void GetTransformTo_join_query()
        {
            JoinQuery<Join<Test1, Test3>> query = EntityExecute<Test1>.Select(new Statements()).InnerJoin<Test3>().Equal(x => x.Table1.Id, x => x.Table2.Ids).Build();
            var loggerMock = new Mock<ILogger<DatabaseManagementEvents>>();
            var events = new DatabaseManagementEvents();

            var result = events.GetTransformTo(ClassOptionsFactory.GetClassOptions(typeof(Join<Test1, Test3>)), query, loggerMock.Object);
            Assert.IsType<Transforms.JoinTransformTo<Join<Test1, Test3>>>(result);
        }

        [Fact]
        public void TransformToByField_query()
        {
            var query = EntityExecute<Test5>.Select(new Statements()).Build();
            var loggerMock = new Mock<ILogger<DatabaseManagementEvents>>();
            var events = new DatabaseManagementEvents();

            var result = events.GetTransformTo(ClassOptionsFactory.GetClassOptions(typeof(Test5)), query, loggerMock.Object);
            Assert.IsType<Transforms.TransformToByField<Test5>>(result);
        }

        [Fact]
        public void TransformToByConstructor_query()
        {
            var query = EntityExecute<Test1>.Select(new Statements()).Build();
            var loggerMock = new Mock<ILogger<DatabaseManagementEvents>>();
            var events = new DatabaseManagementEvents();

            var result = events.GetTransformTo(ClassOptionsFactory.GetClassOptions(typeof(Test1)), query, loggerMock.Object);
            Assert.IsType<Transforms.TransformToByConstructor<Test1>>(result);
        }
    }
}
