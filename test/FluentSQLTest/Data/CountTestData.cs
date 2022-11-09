using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest.Data
{
    internal class Count_Test3_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new FluentSQL.Default.Statements(),"SELECT COUNT(TableName.Id) FROM TableName;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT COUNT([TableName].[Id]) FROM [TableName];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Count_Test3_TestData2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
               new FluentSQL.Default.Statements(),"SELECT COUNT(TableName.Id,TableName.Name,TableName.Create) FROM TableName;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT COUNT([TableName].[Id],[TableName].[Name],[TableName].[Create]) FROM [TableName];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Count_Test3_TestData3 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new FluentSQL.Default.Statements(),"SELECT COUNT(TableName.Id,TableName.Name,TableName.Create) FROM TableName WHERE TableName.IsTests = @Param AND TableName.Id = @Param;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT COUNT([TableName].[Id],[TableName].[Name],[TableName].[Create]) FROM [TableName] WHERE [TableName].[IsTests] = @Param AND [TableName].[Id] = @Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
