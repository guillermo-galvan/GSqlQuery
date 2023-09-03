using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace GSqlQuery.Runner.Test.Data
{
    internal class Delete_Test3_TestData_Connection : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions<IDbConnection>(new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"DELETE FROM Test3;"
            };

            yield return new object[]
            {
                new ConnectionOptions<IDbConnection>(new Models.Statements(),LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"DELETE FROM [Test3];"
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
                 new ConnectionOptions<IDbConnection>(new Statements(),LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"DELETE FROM Test3 WHERE Test3.IsTests = @Param AND Test3.Create IS NOT NULL;"
            };

            yield return new object[]
            {
                 new ConnectionOptions<IDbConnection>(new Models.Statements(),LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"DELETE FROM [Test3] WHERE [Test3].[IsTests] = @Param AND [Test3].[Create] IS NOT NULL;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}