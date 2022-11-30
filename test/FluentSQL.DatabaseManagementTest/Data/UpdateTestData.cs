using FluentSQL.DatabaseManagement.Models;
using System.Collections;
using System.Data.Common;

namespace FluentSQL.DatabaseManagementTest.Data
{
    internal class Update_Test3_TestData_Connection : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions<DbConnection>(new FluentSQL.Default.Statements(),LoadFluentOptions.GetDatabaseManagmentMock()),"UPDATE TableName SET TableName.Id=@Param,TableName.Name=@Param,TableName.Create=@Param,TableName.IsTests=@Param;"
            };

            yield return new object[]
            {
                new ConnectionOptions<DbConnection>(new Models.Statements(),LoadFluentOptions.GetDatabaseManagmentMock()),"UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param;"
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
                new ConnectionOptions<DbConnection>(new FluentSQL.Default.Statements(), LoadFluentOptions.GetDatabaseManagmentMock()),"UPDATE TableName SET TableName.Id=@Param,TableName.Name=@Param,TableName.Create=@Param,TableName.IsTests=@Param WHERE TableName.IsTests = @Param AND TableName.Create = @Param;"
            };

            yield return new object[]
            {
                new ConnectionOptions<DbConnection>(new Models.Statements(),LoadFluentOptions.GetDatabaseManagmentMock()),"UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param WHERE [TableName].[IsTests] = @Param AND [TableName].[Create] = @Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
