using System.Collections;

namespace GSqlQuery.Test.Data
{
    internal class Delete_Test3_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new GSqlQuery.Default.Statements(),"DELETE FROM TableName;"
            };

            yield return new object[]
            {
                new Models.Statements(),"DELETE FROM [TableName];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Delete_Test3_TestData2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new GSqlQuery.Default.Statements(),"DELETE FROM TableName WHERE TableName.IsTests = @Param AND TableName.Create IS NOT NULL;"
            };

            yield return new object[]
            {
                new Models.Statements(),"DELETE FROM [TableName] WHERE [TableName].[IsTests] = @Param AND [TableName].[Create] IS NOT NULL;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
