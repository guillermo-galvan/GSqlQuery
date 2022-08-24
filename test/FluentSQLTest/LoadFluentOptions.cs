using FluentSQL.Default;
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
        public static IDatabaseManagment GetDatabaseManagmentMock()
        {
            Mock<IDatabaseManagment> mock = new();

            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.ExecuteReader(It.IsAny<SelectQuery<Test1>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<SelectQuery<Test1>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((q,p,pa) => {

                    if (q.Text == "SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test1];")
                    {
                        return new Test1[] { new Test1(1, "Name", DateTime.Now, true) }.AsEnumerable();
                    }

                    return Enumerable.Empty<Test1>();
                });
            mock.Setup(x => x.ExecuteScalar(It.IsAny<InsertQuery<Test3>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<Type>()))
                .Returns<InsertQuery<Test3>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>, Type>((q,p,pa,t) => {

                    if (q.Text.Contains("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteNonQuery(It.IsAny<InsertQuery<Test6>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<InsertQuery<Test6>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((q, p, pa) => {

                    if (q.Text.Contains("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteNonQuery(It.IsAny<UpdateQuery<Test3>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<UpdateQuery<Test3>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((q, p, pa) => {

                    if (q.Text.Contains("UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param;"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteNonQuery(It.IsAny<DeleteQuery<Test3>>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<DeleteQuery<Test3>, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((q, p, pa) => {

                    if (q.Text.Contains("DELETE FROM [TableName];"))
                    {
                        return 1;
                    }

                    return 0;
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
