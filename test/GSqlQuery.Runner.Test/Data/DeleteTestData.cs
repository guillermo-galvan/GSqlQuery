using GSqlQuery.Runner.Models;
using System.Collections;
using System.Data.Common;

namespace GSqlQuery.Runner.Test.Data
{
    internal class Delete_Test3_TestData_Connection : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions<DbConnection>(new Statements(), LoadFluentOptions.GetDatabaseManagmentMock()),"DELETE FROM TableName;"
            };

            yield return new object[]
            {
                new ConnectionOptions<DbConnection>(new Models.Statements(),LoadFluentOptions.GetDatabaseManagmentMock()),"DELETE FROM [TableName];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Delete_Test3_TestData2_Connection : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                 new ConnectionOptions<DbConnection>(new Statements(),LoadFluentOptions.GetDatabaseManagmentMock()),"DELETE FROM TableName WHERE TableName.IsTests = @Param AND TableName.Create IS NOT NULL;"
            };

            yield return new object[]
            {
                 new ConnectionOptions<DbConnection>(new Models.Statements(),LoadFluentOptions.GetDatabaseManagmentMock()),"DELETE FROM [TableName] WHERE [TableName].[IsTests] = @Param AND [TableName].[Create] IS NOT NULL;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
