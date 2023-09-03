using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace GSqlQuery.Runner.Test.Data
{
    internal class OrderBy_Test3_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                 new ConnectionOptions<IDbConnection>(new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id FROM Test3 ORDER BY Test3.Name ASC,Test3.Create DESC;"
            };

            yield return new object[]
            {
                new ConnectionOptions<IDbConnection>(new Models.Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] FROM [Test3] ORDER BY [Test3].[Name] ASC,[Test3].[Create] DESC;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class OrderBy_Test3_TestData2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
               new ConnectionOptions<IDbConnection>(new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id,Test3.Name,Test3.Create FROM Test3 ORDER BY Test3.Name ASC,Test3.Create DESC;"
            };

            yield return new object[]
            {
                new ConnectionOptions<IDbConnection>(new Models.Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create] FROM [Test3] ORDER BY [Test3].[Name] ASC,[Test3].[Create] DESC;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class OrderBy_Test3_TestData3 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions<IDbConnection>(new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id,Test3.Name,Test3.Create FROM Test3 WHERE Test3.IsTests = @Param AND Test3.Id = @Param ORDER BY Test3.Name ASC,Test3.Create DESC;"
            };

            yield return new object[]
            {
                new ConnectionOptions<IDbConnection>(new Models.Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create] FROM [Test3] WHERE [Test3].[IsTests] = @Param AND [Test3].[Id] = @Param ORDER BY [Test3].[Name] ASC,[Test3].[Create] DESC;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}