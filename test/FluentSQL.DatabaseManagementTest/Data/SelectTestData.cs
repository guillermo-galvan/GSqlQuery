﻿using FluentSQL.DatabaseManagement.Models;
using System.Collections;
using System.Data.Common;

namespace FluentSQL.DatabaseManagementTest.Data
{
    internal class Select_Test1_TestData_ConnectionOptions : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions<DbConnection>(new FluentSQL.Default.Statements(), LoadFluentOptions.GetDatabaseManagmentMock()),"SELECT Test1.Id,Test1.Name,Test1.Create,Test1.IsTest FROM Test1;"
            };

            yield return new object[]
            {
               new ConnectionOptions<DbConnection>(new Models.Statements(), LoadFluentOptions.GetDatabaseManagmentMock()),"SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test1];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Select_Test1_TestData2_ConnectionOptions : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
               new ConnectionOptions<DbConnection>(new FluentSQL.Default.Statements(), LoadFluentOptions.GetDatabaseManagmentMock()),"SELECT Test1.Id,Test1.Name,Test1.Create FROM Test1;"
            };

            yield return new object[]
            {
                new ConnectionOptions<DbConnection>(new Models.Statements(), LoadFluentOptions.GetDatabaseManagmentMock()),"SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create] FROM [Test1];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Select_Test3_TestData_ConnectionOptions : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions<DbConnection>(new FluentSQL.Default.Statements(), LoadFluentOptions.GetDatabaseManagmentMock()),"SELECT TableName.Id,TableName.Name,TableName.Create,TableName.IsTests FROM TableName;"
            };

            yield return new object[]
            {
                new ConnectionOptions<DbConnection>(new Models.Statements(), LoadFluentOptions.GetDatabaseManagmentMock()),"SELECT [TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests] FROM [TableName];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Select_Test4_TestData_ConnectionOptions : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions<DbConnection>(new FluentSQL.Default.Statements(), LoadFluentOptions.GetDatabaseManagmentMock()),"SELECT Scheme.TableName.Id,Scheme.TableName.Name,Scheme.TableName.Create,Scheme.TableName.IsTests FROM Scheme.TableName;"
            };

            yield return new object[]
            {
                new ConnectionOptions<DbConnection>(new Models.Statements(), LoadFluentOptions.GetDatabaseManagmentMock()),"SELECT [Scheme].[TableName].[Id],[Scheme].[TableName].[Name],[Scheme].[TableName].[Create],[Scheme].[TableName].[IsTests] FROM [Scheme].[TableName];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Select_Test3_TestData2_ConnectionOptions : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
               new ConnectionOptions<DbConnection>(new FluentSQL.Default.Statements(), LoadFluentOptions.GetDatabaseManagmentMock()),"SELECT TableName.Id,TableName.Name,TableName.Create FROM TableName;"
            };

            yield return new object[]
            {
                new ConnectionOptions<DbConnection>(new Models.Statements(), LoadFluentOptions.GetDatabaseManagmentMock()),"SELECT [TableName].[Id],[TableName].[Name],[TableName].[Create] FROM [TableName];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Select_Test3_TestData3_ConnectionOptions : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions<DbConnection>(new FluentSQL.Default.Statements(), LoadFluentOptions.GetDatabaseManagmentMock()),"SELECT TableName.Id,TableName.Name,TableName.Create FROM TableName WHERE TableName.IsTests = @Param AND TableName.Id = @Param;"
            };

            yield return new object[]
            {
                new ConnectionOptions<DbConnection>(new Models.Statements(), LoadFluentOptions.GetDatabaseManagmentMock()),"SELECT [TableName].[Id],[TableName].[Name],[TableName].[Create] FROM [TableName] WHERE [TableName].[IsTests] = @Param AND [TableName].[Id] = @Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}