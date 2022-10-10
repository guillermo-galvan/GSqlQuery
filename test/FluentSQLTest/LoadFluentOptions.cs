using FluentSQL.Default;
using FluentSQL.Models;
using FluentSQLTest.Models;
using Microsoft.Data.SqlClient;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest
{
    internal static class LoadFluentOptions
    {
        public static IDatabaseManagement<DbConnection> GetDatabaseManagmentMock()
        {
            Mock<IDatabaseManagement<DbConnection>> mock = new();

            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.ExecuteReader<Test1>(It.IsAny<IQuery>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<IQuery, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((q,p,pa) => {

                    if (q.Text == "SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test1];" ||
                        q.Text == "SELECT Test1.Id FROM Test1 ORDER BY Test1.Id ASC,Test1.Name,Test1.Create DESC;")
                    {
                        return new Test1[] { new Test1(1, "Name", DateTime.Now, true) }.AsEnumerable();
                    }

                    return Enumerable.Empty<Test1>();
                });

            mock.Setup(x => x.ExecuteReader<Test1>(It.IsAny<DbConnection>(),It.IsAny<IQuery>(), It.IsAny<IEnumerable<PropertyOptions>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<DbConnection, IQuery, IEnumerable<PropertyOptions>, IEnumerable<IDataParameter>>((c,q, p, pa) => {

                    if (q.Text == "SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test1];" ||
                        q.Text == "SELECT Test1.Id FROM Test1 ORDER BY Test1.Id ASC,Test1.Name,Test1.Create DESC;")
                    {
                        return new Test1[] { new Test1(1, "Name", DateTime.Now, true) }.AsEnumerable();
                    }

                    return Enumerable.Empty<Test1>();
                });

            mock.Setup(x => x.ExecuteScalar(It.IsAny<InsertQuery<Test3,DbConnection>>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<Type>()))
                .Returns<InsertQuery<Test3, DbConnection>, IEnumerable<IDataParameter>, Type>((q,pa,t) => {

                    if (q.Text.Contains("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteScalar(It.IsAny<DbConnection>(), It.IsAny<InsertQuery<Test3, DbConnection>>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<Type>()))
               .Returns<DbConnection, InsertQuery<Test3, DbConnection>, IEnumerable<IDataParameter>, Type>((c,q, pa, t) => {

                   if (q.Text.Contains("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])"))
                   {
                       return 1;
                   }

                   return 0;
               });

            mock.Setup(x => x.ExecuteScalar(It.IsAny<CountQuery<Test1,DbConnection>>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<Type>()))
                .Returns<CountQuery<Test1, DbConnection>, IEnumerable<IDataParameter>, Type>((q, pa, t) => {

                    if (q.Text.Contains("SELECT COUNT([Test1].[Id]) FROM [Test1];"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteScalar(It.IsAny<DbConnection>(), It.IsAny<CountQuery<Test1, DbConnection>>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<Type>()))
               .Returns<DbConnection, CountQuery<Test1, DbConnection>, IEnumerable<IDataParameter>, Type>((c, q, pa, t) => {

                   if (q.Text.Contains("SELECT COUNT([Test1].[Id]) FROM [Test1];"))
                   {
                       return 1;
                   }

                   return 0;
               });

            mock.Setup(x => x.ExecuteScalar(It.IsAny<CountQuery<Test3, DbConnection>>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<Type>()))
                .Returns<CountQuery<Test3, DbConnection>, IEnumerable<IDataParameter>, Type>((q, pa, t) => {

                    if (q.Text.Contains("SELECT COUNT(TableName.Id) FROM TableName;"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteScalar(It.IsAny<DbConnection>(), It.IsAny<CountQuery<Test3, DbConnection>>(), It.IsAny<IEnumerable<IDataParameter>>(), It.IsAny<Type>()))
               .Returns<DbConnection, CountQuery<Test3, DbConnection>, IEnumerable<IDataParameter>, Type>((c, q, pa, t) => {

                   if (q.Text.Contains("SELECT COUNT([TableName].[Id]) FROM [TableName];"))
                   {
                       return 1;
                   }

                   return 0;
               });

            mock.Setup(x => x.ExecuteNonQuery(It.IsAny<InsertQuery<Test6>>(),  It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<InsertQuery<Test6>, IEnumerable<IDataParameter>>((q, pa) => {

                    if (q.Text.Contains("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteNonQuery(It.IsAny<DbConnection>(), It.IsAny<InsertQuery<Test6>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<DbConnection,InsertQuery<Test6>, IEnumerable<IDataParameter>>((c, q, pa) => {

                    if (q.Text.Contains("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteNonQuery(It.IsAny<UpdateQuery<Test3,DbConnection>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<UpdateQuery<Test3, DbConnection>, IEnumerable<IDataParameter>>((q, pa) => {

                    if (q.Text.Contains("UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param;"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteNonQuery(It.IsAny<DbConnection>(), It.IsAny<UpdateQuery<Test3, DbConnection>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<DbConnection,UpdateQuery<Test3, DbConnection>, IEnumerable<IDataParameter>>((c,q, pa) => {

                    if (q.Text.Contains("UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param;"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteNonQuery(It.IsAny<DeleteQuery<Test3,DbConnection>>(), It.IsAny<IEnumerable<IDataParameter>>()))
                .Returns<DeleteQuery<Test3, DbConnection>, IEnumerable<IDataParameter>>((q, pa) => {

                    if (q.Text.Contains("DELETE FROM [TableName];"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteNonQuery(It.IsAny<DbConnection>(),It.IsAny<DeleteQuery<Test3, DbConnection>>(), It.IsAny<IEnumerable<IDataParameter>>()))
               .Returns<DbConnection,DeleteQuery<Test3, DbConnection>, IEnumerable<IDataParameter>>((c,q, pa) => {

                   if (q.Text.Contains("DELETE FROM [TableName];"))
                   {
                       return 1;
                   }

                   return 0;
               });

            mock.Setup(x => x.GetConnection()).Returns(() => GetDbConnection());

            return mock.Object;
        }

        public static DbConnection GetDbConnection()
        {
            Mock<DbConnection> mock = new();

            //mock.Setup(x => x.BeginTransaction()).Returns(GetDbTransaction());

            return mock.Object;
        }

        public static DbTransaction GetDbTransaction()
        {
            Mock<DbTransaction> mock = new();

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
