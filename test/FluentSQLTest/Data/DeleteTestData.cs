using FluentSQL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest.Data
{
    internal class Delete_Test3_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new FluentSQL.Default.Statements(),"DELETE FROM TableName;"
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
                new FluentSQL.Default.Statements(),"DELETE FROM TableName WHERE TableName.IsTests = @Param AND TableName.Create IS NOT NULL;"
            };

            yield return new object[]
            {
                new Models.Statements(),"DELETE FROM [TableName] WHERE [TableName].[IsTests] = @Param AND [TableName].[Create] IS NOT NULL;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
