using FluentSQL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest.Data
{
    internal class Update_Test3_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions(new FluentSQL.Default.Statements()),"UPDATE TableName SET TableName.Id=@Param,TableName.Name=@Param,TableName.Create=@Param,TableName.IsTests=@Param;"
            };

            yield return new object[]
            {
                new ConnectionOptions(new Models.Statements()),"UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Update_Test3_TestData2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions(new FluentSQL.Default.Statements()),"UPDATE TableName SET TableName.Id=@Param,TableName.Name=@Param,TableName.Create=@Param,TableName.IsTests=@Param WHERE TableName.IsTests = @Param AND TableName.Create = @Param;"
            };

            yield return new object[]
            {
                new ConnectionOptions(new Models.Statements()),"UPDATE [TableName] SET [TableName].[Id]=@Param,[TableName].[Name]=@Param,[TableName].[Create]=@Param,[TableName].[IsTests]=@Param WHERE [TableName].[IsTests] = @Param AND [TableName].[Create] = @Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
