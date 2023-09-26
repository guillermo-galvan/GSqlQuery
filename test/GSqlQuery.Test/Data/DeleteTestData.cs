using System.Collections;
using System.Collections.Generic;

namespace GSqlQuery.Test.Data
{
    internal class Delete_Test3_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new DefaultFormats(),"DELETE FROM Test3;"
            };

            yield return new object[]
            {
                new Models.Formats(),"DELETE FROM [Test3];"
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
                new DefaultFormats(),"DELETE FROM Test3 WHERE Test3.IsTests = @Param AND Test3.Create IS NOT NULL;"
            };

            yield return new object[]
            {
                new Models.Formats(),"DELETE FROM [Test3] WHERE [Test3].[IsTests] = @Param AND [Test3].[Create] IS NOT NULL;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}