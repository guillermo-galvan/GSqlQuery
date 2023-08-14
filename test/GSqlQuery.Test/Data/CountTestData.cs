using System.Collections;
using System.Collections.Generic;

namespace GSqlQuery.Test.Data
{
    internal class Count_Test3_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT COUNT(Test3.Id) FROM Test3;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT COUNT([Test3].[Id]) FROM [Test3];"
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
               new Statements(),"SELECT COUNT(Test3.Id,Test3.Name,Test3.Create) FROM Test3;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT COUNT([Test3].[Id],[Test3].[Name],[Test3].[Create]) FROM [Test3];"
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
                new Statements(),"SELECT COUNT(Test3.Id,Test3.Name,Test3.Create) FROM Test3 WHERE Test3.IsTests = @Param AND Test3.Id = @Param;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT COUNT([Test3].[Id],[Test3].[Name],[Test3].[Create]) FROM [Test3] WHERE [Test3].[IsTests] = @Param AND [Test3].[Id] = @Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}