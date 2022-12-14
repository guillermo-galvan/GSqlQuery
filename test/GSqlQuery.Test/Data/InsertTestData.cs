using System.Collections;

namespace GSqlQuery.Test.Data
{
    internal class Insert_Test3_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"INSERT INTO TableName (TableName.Name,TableName.Create,TableName.IsTests) VALUES (@Param,@Param,@Param); "
            };

            yield return new object[]
            {
                new Models.Statements(),"INSERT INTO [TableName] ([TableName].[Name],[TableName].[Create],[TableName].[IsTests]) VALUES (@Param,@Param,@Param); SELECT SCOPE_IDENTITY();"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Insert_Test6_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"INSERT INTO TableName (TableName.Id,TableName.Name,TableName.Create,TableName.IsTests) VALUES (@Param,@Param,@Param,@Param);"
            };

            yield return new object[]
            {
                new Models.Statements(),"INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests]) VALUES (@Param,@Param,@Param,@Param);"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
