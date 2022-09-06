using FluentSQL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest.Data
{
    internal class Insert_Test3_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new FluentSQL.Default.Statements(),"INSERT INTO TableName (TableName.Name,TableName.Create,TableName.IsTests) VALUES (@Param,@Param,@Param); "
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
                new FluentSQL.Default.Statements(),"INSERT INTO TableName (TableName.Id,TableName.Name,TableName.Create,TableName.IsTests) VALUES (@Param,@Param,@Param,@Param);"
            };

            yield return new object[]
            {
                new Models.Statements(),"INSERT INTO [TableName] ([TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests]) VALUES (@Param,@Param,@Param,@Param);"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
