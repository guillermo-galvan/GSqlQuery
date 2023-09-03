using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace GSqlQuery.Runner.Test.Data
{
    internal class Update_Test3_TestData_Connection : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions<IDbConnection>(new Statements(),LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"UPDATE Test3 SET Test3.Id=@Param,Test3.Name=@Param,Test3.Create=@Param,Test3.IsTests=@Param;"
            };

            yield return new object[]
            {
                new ConnectionOptions<IDbConnection>(new Models.Statements(),LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"UPDATE [Test3] SET [Test3].[Id]=@Param,[Test3].[Name]=@Param,[Test3].[Create]=@Param,[Test3].[IsTests]=@Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Update_Test3_TestData2_Connection : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions<IDbConnection>(new Statements(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"UPDATE Test3 SET Test3.Id=@Param,Test3.Name=@Param,Test3.Create=@Param,Test3.IsTests=@Param WHERE Test3.IsTests = @Param AND Test3.Create = @Param;"
            };

            yield return new object[]
            {
                new ConnectionOptions<IDbConnection>(new Models.Statements(),LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"UPDATE [Test3] SET [Test3].[Id]=@Param,[Test3].[Name]=@Param,[Test3].[Create]=@Param,[Test3].[IsTests]=@Param WHERE [Test3].[IsTests] = @Param AND [Test3].[Create] = @Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}