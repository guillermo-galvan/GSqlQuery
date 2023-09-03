using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace GSqlQuery.Runner.Test.Data
{
    internal class Insert_Test3_TestData_ConnectionOptions : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions<IDbConnection> (new Statements(),LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"INSERT INTO Test3 (Test3.Name,Test3.Create,Test3.IsTests) VALUES (@Param,@Param,@Param); "
            };

            yield return new object[]
            {
                new ConnectionOptions<IDbConnection> (new Models.Statements(),LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"INSERT INTO [Test3] ([Test3].[Name],[Test3].[Create],[Test3].[IsTests]) VALUES (@Param,@Param,@Param); SELECT SCOPE_IDENTITY();"
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
                 new ConnectionOptions<IDbConnection> (new Statements(),LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"INSERT INTO TableName (TableName.Id,TableName.Name,TableName.Create,TableName.IsTests) VALUES (@Param,@Param,@Param,@Param);"
            };

            yield return new object[]
            {
                new ConnectionOptions<IDbConnection> (new Models.Statements(),LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests]) VALUES (@Param,@Param,@Param,@Param);"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}