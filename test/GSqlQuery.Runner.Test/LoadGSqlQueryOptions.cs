﻿using GSqlQuery.Cache;
using GSqlQuery.Runner.Test.Models;
using Moq;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Runner.Test
{
    internal class TestDatabaseManagmentEvents : DatabaseManagementEvents
    {
        protected override ITypeHandler<TDbDataReader> GetTypeHandler<TDbDataReader>(Type property)
        {
            return null;
        }
    }

    internal static class LoadGSqlQueryOptions
    {
        public static IDatabaseManagement<IDbConnection> GetDatabaseManagmentMock()
        {
            Mock<IDatabaseManagement<IDbConnection>> mock = new Mock<IDatabaseManagement<IDbConnection>>();

            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.ExecuteReader<Test1>(It.IsAny<IQuery<Test1>>(), It.IsAny<PropertyOptionsCollection>()))
                .Returns<IQuery, PropertyOptionsCollection>((q, p) =>
                {

                    if (q.Text == "SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test1];" ||
                        q.Text == "SELECT Test1.Id FROM Test1 ORDER BY Test1.Id ASC,Test1.Name,Test1.Create DESC;")
                    {
                        return new Test1[] { new Test1(1, "Name", DateTime.Now, true) }.AsEnumerable();
                    }

                    return Enumerable.Empty<Test1>();
                });

            mock.Setup(x => x.ExecuteReader<Test1>(It.IsAny<IDbConnection>(), It.IsAny<IQuery<Test1>>(), It.IsAny<PropertyOptionsCollection>()))
                .Returns<IDbConnection, IQuery, PropertyOptionsCollection>((c, q, p) =>
                {

                    if (q.Text == "SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test1];" ||
                        q.Text == "SELECT Test1.Id FROM Test1 ORDER BY Test1.Id ASC,Test1.Name,Test1.Create DESC;")
                    {
                        return new Test1[] { new Test1(1, "Name", DateTime.Now, true) }.AsEnumerable();
                    }

                    return Enumerable.Empty<Test1>();
                });

            mock.Setup(x => x.ExecuteScalar<object>(It.IsAny<InsertQuery<Test3, IDbConnection>>()))
                .Returns<InsertQuery<Test3, IDbConnection>>((q) =>
                {

                    if (q.Text.Contains("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteScalar<object>(It.IsAny<IDbConnection>(), It.IsAny<InsertQuery<Test3, IDbConnection>>()))
               .Returns<IDbConnection, InsertQuery<Test3, IDbConnection>>((c, q) =>
               {

                   if (q.Text.Contains("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])"))
                   {
                       return 1;
                   }

                   return 0;
               });

            mock.Setup(x => x.ExecuteScalar<object>(It.IsAny<CountQuery<Test1, IDbConnection>>()))
                .Returns<CountQuery<Test1, IDbConnection>>((q) =>
                {
                    if (q.Text.Contains("SELECT COUNT([Test1].[Id]) FROM [Test1];"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteScalar<object>(It.IsAny<IDbConnection>(), It.IsAny<CountQuery<Test1, IDbConnection>>()))
               .Returns<IDbConnection, CountQuery<Test1, IDbConnection>>((c, q) =>
               {
                   if (q.Text.Contains("SELECT COUNT([Test1].[Id]) FROM [Test1];"))
                   {
                       return 1;
                   }

                   return 0;
               });

            mock.Setup(x => x.ExecuteScalar<object>(It.IsAny<CountQuery<Test3, IDbConnection>>()))
                .Returns<CountQuery<Test3, IDbConnection>>((q) =>
                {
                    if (q.Text.Contains("SELECT COUNT(TableName.Id) FROM TableName;"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteScalar<object>(It.IsAny<IDbConnection>(), It.IsAny<CountQuery<Test3, IDbConnection>>()))
               .Returns<IDbConnection, CountQuery<Test3, IDbConnection>>((c, q) =>
               {
                   if (q.Text.Contains("SELECT COUNT([TableName].[Id]) FROM [TableName];"))
                   {
                       return 1;
                   }

                   return 0;
               });

            mock.Setup(x => x.ExecuteNonQuery(It.IsAny<InsertQuery<Test6>>()))
                .Returns<InsertQuery<Test6>>((q) =>
                {
                    if (q.Text.Contains("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteNonQuery(It.IsAny<IDbConnection>(), It.IsAny<InsertQuery<Test6>>()))
                .Returns<IDbConnection, InsertQuery<Test6>>((c, q) =>
                {
                    if (q.Text.Contains("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteNonQuery(It.IsAny<UpdateQuery<Test3, IDbConnection>>()))
                .Returns<UpdateQuery<Test3, IDbConnection>>((q) =>
                {

                    if (q.Text.Contains("UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param;"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteNonQuery(It.IsAny<IDbConnection>(), It.IsAny<UpdateQuery<Test3, IDbConnection>>()))
                .Returns<IDbConnection, UpdateQuery<Test3, IDbConnection>>((c, q) =>
                {

                    if (q.Text.Contains("UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param;"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteNonQuery(It.IsAny<DeleteQuery<Test3, IDbConnection>>()))
                .Returns<DeleteQuery<Test3, IDbConnection>>((q) =>
                {

                    if (q.Text.Contains("DELETE FROM [TableName];"))
                    {
                        return 1;
                    }

                    return 0;
                });

            mock.Setup(x => x.ExecuteNonQuery(It.IsAny<IDbConnection>(), It.IsAny<DeleteQuery<Test3, IDbConnection>>()))
               .Returns<IDbConnection, DeleteQuery<Test3, IDbConnection>>((c, q) =>
               {

                   if (q.Text.Contains("DELETE FROM [TableName];"))
                   {
                       return 1;
                   }

                   return 0;
               });

            mock.Setup(x => x.GetConnection()).Returns(() => GetIDbConnection());

            return mock.Object;
        }

        public static IDatabaseManagement<IDbConnection> GetDatabaseManagmentMockAsync()
        {
            Mock<IDatabaseManagement<IDbConnection>> mock = new Mock<IDatabaseManagement<IDbConnection>>();

            mock.Setup(x => x.Events).Returns(new TestDatabaseManagmentEvents());
            mock.Setup(x => x.ExecuteReaderAsync<Test1>(It.IsAny<IQuery<Test1>>(), It.IsAny<PropertyOptionsCollection>(), It.IsAny<CancellationToken>()))
                .Returns<IQuery, PropertyOptionsCollection, CancellationToken>((q, p, t) =>
                {
                    if (q.Text == "SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test1];" ||
                        q.Text == "SELECT Test1.Id FROM Test1 ORDER BY Test1.Id ASC,Test1.Name,Test1.Create DESC;")
                    {
                        return Task.FromResult(new Test1[] { new Test1(1, "Name", DateTime.Now, true) }.AsEnumerable());
                    }

                    return Task.FromResult(Enumerable.Empty<Test1>());
                });

            mock.Setup(x => x.ExecuteReaderAsync<Test1>(It.IsAny<IDbConnection>(), It.IsAny<IQuery<Test1>>(), It.IsAny<PropertyOptionsCollection>(), It.IsAny<CancellationToken>()))
                .Returns<IDbConnection, IQuery, PropertyOptionsCollection, CancellationToken>((c, q, p, t) =>
                {

                    if (q.Text == "SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test1];" ||
                        q.Text == "SELECT Test1.Id FROM Test1 ORDER BY Test1.Id ASC,Test1.Name,Test1.Create DESC;")
                    {
                        return Task.FromResult(new Test1[] { new Test1(1, "Name", DateTime.Now, true) }.AsEnumerable());
                    }

                    return Task.FromResult(Enumerable.Empty<Test1>());
                });

            mock.Setup(x => x.ExecuteScalarAsync<object>(It.IsAny<InsertQuery<Test3, IDbConnection>>(), It.IsAny<CancellationToken>()))
                .Returns<InsertQuery<Test3, IDbConnection>, CancellationToken>((q, t) =>
                {

                    if (q.Text.Contains("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])"))
                    {
                        return Task.FromResult((object)1);
                    }

                    return Task.FromResult((object)0);
                });

            mock.Setup(x => x.ExecuteScalarAsync<object>(It.IsAny<IDbConnection>(), It.IsAny<InsertQuery<Test3, IDbConnection>>(), It.IsAny<CancellationToken>()))
               .Returns<IDbConnection, InsertQuery<Test3, IDbConnection>, CancellationToken>((c, q, t) =>
               {

                   if (q.Text.Contains("INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests])"))
                   {
                       return Task.FromResult((object)1);
                   }

                   return Task.FromResult((object)0);
               });

            mock.Setup(x => x.ExecuteScalarAsync<int>(It.IsAny<CountQuery<Test1, IDbConnection>>(), It.IsAny<CancellationToken>()))
                .Returns<CountQuery<Test1, IDbConnection>, CancellationToken>((q, CancellationToken) =>
                {

                    if (q.Text.Contains("SELECT COUNT([Test1].[Id]) FROM [Test1];"))
                    {
                        return Task.FromResult(1);
                    }

                    return Task.FromResult(0);
                });

            mock.Setup(x => x.ExecuteScalarAsync<int>(It.IsAny<IDbConnection>(), It.IsAny<CountQuery<Test1, IDbConnection>>(), It.IsAny<CancellationToken>()))
               .Returns<IDbConnection, CountQuery<Test1, IDbConnection>, CancellationToken>((c, q, CancellationToken) =>
               {

                   if (q.Text.Contains("SELECT COUNT([Test1].[Id]) FROM [Test1];"))
                   {
                       return Task.FromResult(1);
                   }

                   return Task.FromResult(0);
               });

            mock.Setup(x => x.ExecuteScalarAsync<int>(It.IsAny<CountQuery<Test3, IDbConnection>>(), It.IsAny<CancellationToken>()))
                .Returns<CountQuery<Test3, IDbConnection>, CancellationToken>((q, CancellationToken) =>
                {

                    if (q.Text.Contains("SELECT COUNT(TableName.Id) FROM TableName;"))
                    {
                        return Task.FromResult(1);
                    }

                    return Task.FromResult(0);
                });

            mock.Setup(x => x.ExecuteScalarAsync<int>(It.IsAny<IDbConnection>(), It.IsAny<CountQuery<Test3, IDbConnection>>(), It.IsAny<CancellationToken>()))
               .Returns<IDbConnection, CountQuery<Test3, IDbConnection>, CancellationToken>((c, q, t) =>
               {

                   if (q.Text.Contains("SELECT COUNT([TableName].[Id]) FROM [TableName];"))
                   {
                       return Task.FromResult(1);
                   }

                   return Task.FromResult(0);
               });

            mock.Setup(x => x.ExecuteNonQueryAsync(It.IsAny<InsertQuery<Test6>>(), It.IsAny<CancellationToken>()))
                .Returns<InsertQuery<Test6>, CancellationToken>((q, t) =>
                {

                    if (q.Text.Contains("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])"))
                    {
                        return Task.FromResult(1);
                    }

                    return Task.FromResult(0);
                });

            mock.Setup(x => x.ExecuteNonQueryAsync(It.IsAny<IDbConnection>(), It.IsAny<InsertQuery<Test6>>(), It.IsAny<CancellationToken>()))
                .Returns<IDbConnection, InsertQuery<Test6>, CancellationToken>((c, q, t) =>
                {

                    if (q.Text.Contains("INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests])"))
                    {
                        return Task.FromResult(1);
                    }

                    return Task.FromResult(0);
                });

            mock.Setup(x => x.ExecuteNonQueryAsync(It.IsAny<UpdateQuery<Test3, IDbConnection>>(), It.IsAny<CancellationToken>()))
                .Returns<UpdateQuery<Test3, IDbConnection>, CancellationToken>((q, t) =>
                {

                    if (q.Text.Contains("UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param;"))
                    {
                        return Task.FromResult(1);
                    }

                    return Task.FromResult(0);
                });

            mock.Setup(x => x.ExecuteNonQueryAsync(It.IsAny<IDbConnection>(), It.IsAny<UpdateQuery<Test3, IDbConnection>>(), It.IsAny<CancellationToken>()))
                .Returns<IDbConnection, UpdateQuery<Test3, IDbConnection>, CancellationToken>((c, q, t) =>
                {

                    if (q.Text.Contains("UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param;"))
                    {
                        return Task.FromResult(1);
                    }

                    return Task.FromResult(0);
                });

            mock.Setup(x => x.ExecuteNonQueryAsync(It.IsAny<DeleteQuery<Test3, IDbConnection>>(), It.IsAny<CancellationToken>()))
                .Returns<DeleteQuery<Test3, IDbConnection>, CancellationToken>((q, t) =>
                {

                    if (q.Text.Contains("DELETE FROM [TableName];"))
                    {
                        return Task.FromResult(1);
                    }

                    return Task.FromResult(0);
                });

            mock.Setup(x => x.ExecuteNonQueryAsync(It.IsAny<IDbConnection>(), It.IsAny<DeleteQuery<Test3, IDbConnection>>(), It.IsAny<CancellationToken>()))
               .Returns<IDbConnection, DeleteQuery<Test3, IDbConnection>, CancellationToken>((c, q, t) =>
               {

                   if (q.Text.Contains("DELETE FROM [TableName];"))
                   {
                       return Task.FromResult(1);
                   }

                   return Task.FromResult(0);
               });

            mock.Setup(x => x.GetConnection()).Returns(() => GetIDbConnection());

            return mock.Object;
        }

        public static IDbConnection GetIDbConnection()
        {
            Mock<IDbConnection> mock = new Mock<IDbConnection>();

            mock.Setup(x => x.BeginTransaction()).Returns(GetDbTransaction());

            return mock.Object;
        }

        public static IDbTransaction GetDbTransaction()
        {
            Mock<IDbTransaction> mock = new Mock<IDbTransaction>();

            return mock.Object;
        }
    }
}