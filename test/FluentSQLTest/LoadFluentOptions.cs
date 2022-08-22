using FluentSQL.Models;
using FluentSQLTest.Models;
using Microsoft.Data.SqlClient;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest
{
    internal static class LoadFluentOptions
    {
        public static void Load()
        {
            if (!FluentSQLManagement.Options.ConnectionCollection.GetAllKeys().Any())
            {
                FluentSQLOptions options = new();
                options.ConnectionCollection.Add("Default", new ConnectionOptions(new FluentSQL.Default.Statements()));
                options.ConnectionCollection.Add("My", new ConnectionOptions(new Statements()));
                FluentSQLManagement.SetOptions(options);
            }
        }

        public static IDatabaseManagment GetDatabaseManagmentMock()
        {
            Mock<IDatabaseManagment> mock = new();

            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.ExecuteReader(It.IsAny<IQuery<Test1>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<IQuery<Test1>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((q,p,pa) => {

                    if (q.Text == "SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test1];")
                    {
                        return new Test1[] { new Test1(1, "Name", DateTime.Now, true) }.AsEnumerable();
                    }

                    return Enumerable.Empty<Test1>();
                });

            return mock.Object;
        }
    }

    internal class TestDatabaseManagmentEvents : DatabaseManagmentEvents
    {
        public override Func<Type, IEnumerable<ParameterDetail>, IEnumerable<IDataParameter>> OnGetParameter { get; set; } = (type, parametersDetail) =>
        {
            return parametersDetail.Select(x => new SqlParameter(x.Name, x.Value));
        };
    }
}
