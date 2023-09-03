using System.Collections;
using System.Collections.Generic;

namespace GSqlQuery.Test.Data
{
    internal class Update_Test3_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"UPDATE Test3 SET Test3.Id=@Param,Test3.Name=@Param,Test3.Create=@Param,Test3.IsTests=@Param;"
            };

            yield return new object[]
            {
                new Models.Statements(),"UPDATE [Test3] SET [Test3].[Id]=@Param,[Test3].[Name]=@Param,[Test3].[Create]=@Param,[Test3].[IsTests]=@Param;"
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
                new Statements(),"UPDATE Test3 SET Test3.Id=@Param,Test3.Name=@Param,Test3.Create=@Param,Test3.IsTests=@Param WHERE Test3.IsTests = @Param AND Test3.Create = @Param;"
            };

            yield return new object[]
            {
                new Models.Statements(),"UPDATE [Test3] SET [Test3].[Id]=@Param,[Test3].[Name]=@Param,[Test3].[Create]=@Param,[Test3].[IsTests]=@Param WHERE [Test3].[IsTests] = @Param AND [Test3].[Create] = @Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}