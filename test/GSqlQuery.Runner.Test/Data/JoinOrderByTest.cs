using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace GSqlQuery.Runner.Test.Data
{
    internal class Inner_Join_OrderBy_two_tables_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id ORDER BY Test3.Create DESC,TableName.Name ASC;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] as [Test3_Id],[Test3].[Name] as [Test3_Name],[Test3].[Create] as [Test3_Create],[Test3].[IsTests] as [Test3_IsTests],[TableName].[Id] as [Test6_Id],[TableName].[Name] as [Test6_Name],[TableName].[Create] as [Test6_Create],[TableName].[IsTests] as [Test6_IsTests] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] ORDER BY [Test3].[Create] DESC,[TableName].[Name] ASC;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Inner_Join_OrderBy_two_tables_TestData2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id as Test3_Id,Test3.Name as Test3_Name,TableName.Create as Test6_Create FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id ORDER BY Test3.Name,Test3.Create,TableName.Name,TableName.Create DESC;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] as [Test3_Id],[Test3].[Name] as [Test3_Name],[TableName].[Create] as [Test6_Create] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] ORDER BY [Test3].[Name],[Test3].[Create],[TableName].[Name],[TableName].[Create] DESC;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Inner_Join_OrderBy_two_tables_with_where_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id WHERE Test3.Id = @Param AND TableName.IsTests = @Param ORDER BY Test3.Name,Test3.Create,TableName.Name,TableName.Create DESC;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] as [Test3_Id],[Test3].[Name] as [Test3_Name],[Test3].[Create] as [Test3_Create],[Test3].[IsTests] as [Test3_IsTests],[TableName].[Id] as [Test6_Id],[TableName].[Name] as [Test6_Name],[TableName].[Create] as [Test6_Create],[TableName].[IsTests] as [Test6_IsTests] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] WHERE [Test3].[Id] = @Param AND [TableName].[IsTests] = @Param ORDER BY [Test3].[Name],[Test3].[Create],[TableName].[Name],[TableName].[Create] DESC;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Inner_Join_OrderBy_three_tables_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests,Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id RIGHT JOIN Test1 ON TableName.Id = Test1.Id WHERE Test3.Id = @Param AND TableName.IsTests = @Param ORDER BY Test1.Id,Test3.Name,Test3.IsTests,TableName.Name,TableName.IsTests ASC;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] as [Test3_Id],[Test3].[Name] as [Test3_Name],[Test3].[Create] as [Test3_Create],[Test3].[IsTests] as [Test3_IsTests],[TableName].[Id] as [Test6_Id],[TableName].[Name] as [Test6_Name],[TableName].[Create] as [Test6_Create],[TableName].[IsTests] as [Test6_IsTests],[Test1].[Id] as [Test1_Id],[Test1].[Name] as [Test1_Name],[Test1].[Create] as [Test1_Create],[Test1].[IsTest] as [Test1_IsTest] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] RIGHT JOIN [Test1] ON [TableName].[Id] = [Test1].[Id] WHERE [Test3].[Id] = @Param AND [TableName].[IsTests] = @Param ORDER BY [Test1].[Id],[Test3].[Name],[Test3].[IsTests],[TableName].[Name],[TableName].[IsTests] ASC;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}