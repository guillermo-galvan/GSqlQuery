using System.Collections;

namespace GSqlQuery.Test.Data
{
    internal class Count_Test3_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new GSqlQuery.Default.Statements(),"SELECT COUNT(TableName.Id) FROM TableName;"
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
               new GSqlQuery.Default.Statements(),"SELECT COUNT(TableName.Id,TableName.Name,TableName.Create) FROM TableName;"
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
                new GSqlQuery.Default.Statements(),"SELECT COUNT(TableName.Id,TableName.Name,TableName.Create) FROM TableName WHERE TableName.IsTests = @Param AND TableName.Id = @Param;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT COUNT([TableName].[Id],[TableName].[Name],[TableName].[Create]) FROM [TableName] WHERE [TableName].[IsTests] = @Param AND [TableName].[Id] = @Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
