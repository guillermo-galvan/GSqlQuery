using System.Collections;

namespace GSqlQuery.Test.Data
{
    internal class OrderBy_Test3_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT TableName.Id FROM TableName ORDER BY TableName.Name ASC,TableName.Create DESC;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [TableName].[Id] FROM [TableName] ORDER BY [TableName].[Name] ASC,[TableName].[Create] DESC;"
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
               new Statements(),"SELECT TableName.Id,TableName.Name,TableName.Create FROM TableName ORDER BY TableName.Name ASC,TableName.Create DESC;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [TableName].[Id],[TableName].[Name],[TableName].[Create] FROM [TableName] ORDER BY [TableName].[Name] ASC,[TableName].[Create] DESC;"
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
                new Statements(),"SELECT TableName.Id,TableName.Name,TableName.Create FROM TableName WHERE TableName.IsTests = @Param AND TableName.Id = @Param ORDER BY TableName.Name ASC,TableName.Create DESC;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [TableName].[Id],[TableName].[Name],[TableName].[Create] FROM [TableName] WHERE [TableName].[IsTests] = @Param AND [TableName].[Id] = @Param ORDER BY [TableName].[Name] ASC,[TableName].[Create] DESC;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
