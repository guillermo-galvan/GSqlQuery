using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace GSqlQuery.Runner.Test.Data
{
    internal class Select_Test1_TestData_ConnectionOptions : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions<IDbConnection>(new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test1.Id,Test1.Name,Test1.Create,Test1.IsTest FROM Test1;"
            };

            yield return new object[]
            {
               new ConnectionOptions<IDbConnection>(new Models.Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test1];"
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
               new ConnectionOptions<IDbConnection>(new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test1.Id,Test1.Name,Test1.Create FROM Test1;"
            };

            yield return new object[]
            {
                new ConnectionOptions<IDbConnection>(new Models.Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create] FROM [Test1];"
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
                new ConnectionOptions<IDbConnection>(new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id,Test3.Name,Test3.Create,Test3.IsTests FROM Test3;"
            };

            yield return new object[]
            {
                new ConnectionOptions<IDbConnection>(new Models.Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create],[Test3].[IsTests] FROM [Test3];"
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
                new ConnectionOptions<IDbConnection>(new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Scheme.TableName.Id,Scheme.TableName.Name,Scheme.TableName.Create,Scheme.TableName.IsTests FROM Scheme.TableName;"
            };

            yield return new object[]
            {
                new ConnectionOptions<IDbConnection>(new Models.Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Scheme].[TableName].[Id],[Scheme].[TableName].[Name],[Scheme].[TableName].[Create],[Scheme].[TableName].[IsTests] FROM [Scheme].[TableName];"
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
               new ConnectionOptions<IDbConnection>(new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id,Test3.Name,Test3.Create FROM Test3;"
            };

            yield return new object[]
            {
                new ConnectionOptions<IDbConnection>(new Models.Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create] FROM [Test3];"
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
                new ConnectionOptions<IDbConnection>(new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id,Test3.Name,Test3.Create FROM Test3 WHERE Test3.IsTests = @Param AND Test3.Id = @Param;"
            };

            yield return new object[]
            {
                new ConnectionOptions<IDbConnection>(new Models.Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create] FROM [Test3] WHERE [Test3].[IsTests] = @Param AND [Test3].[Id] = @Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}