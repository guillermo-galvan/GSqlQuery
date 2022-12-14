using GSqlQuery.Runner.Models;
using System.Collections;
using System.Data.Common;

namespace GSqlQuery.Runner.Test.Data
{
    internal class Insert_Test3_TestData_ConnectionOptions : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions<DbConnection> (new Statements(),LoadFluentOptions.GetDatabaseManagmentMock()),"INSERT INTO TableName (TableName.Name,TableName.Create,TableName.IsTests) VALUES (@Param,@Param,@Param); "
            };

            yield return new object[]
            {
                new ConnectionOptions<DbConnection> (new Models.Statements(),LoadFluentOptions.GetDatabaseManagmentMock()),"INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests]) VALUES (@Param,@Param,@Param); SELECT SCOPE_IDENTITY();"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Insert_Test6_TestData_ConnectionOptions : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions<DbConnection> (new Statements(),LoadFluentOptions.GetDatabaseManagmentMock()),"INSERT INTO TableName (TableName.Id,TableName.Name,TableName.Create,TableName.IsTests) VALUES (@Param,@Param,@Param,@Param);"
            };

            yield return new object[]
            {
                new ConnectionOptions<DbConnection> (new Models.Statements(),LoadFluentOptions.GetDatabaseManagmentMock()),"INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests]) VALUES (@Param,@Param,@Param,@Param);"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
