using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSqlQuery.Test.Data
{
    internal class Inner_Join_OrderBy_two_tables_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create,Test3.IsTests,TableName.Id,TableName.Name,TableName.Create,TableName.IsTests FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id ORDER BY Test3.Create DESC,TableName.Name ASC;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create],[Test3].[IsTests],[TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] ORDER BY [Test3].[Create] DESC,[TableName].[Name] ASC;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Inner_Join_OrderBy_two_tables_TestData2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,TableName.Create FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id ORDER BY Test3.Name,Test3.Create,TableName.Name,TableName.Create DESC;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[TableName].[Create] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] ORDER BY [Test3].[Name],[Test3].[Create],[TableName].[Name],[TableName].[Create] DESC;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Inner_Join_OrderBy_two_tables_with_where_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create,Test3.IsTests,TableName.Id,TableName.Name,TableName.Create,TableName.IsTests FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id WHERE Test3.Id = @Param AND TableName.IsTests = @Param ORDER BY Test3.Name,Test3.Create,TableName.Name,TableName.Create DESC;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create],[Test3].[IsTests],[TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] WHERE [Test3].[Id] = @Param AND [TableName].[IsTests] = @Param ORDER BY [Test3].[Name],[Test3].[Create],[TableName].[Name],[TableName].[Create] DESC;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Inner_Join_OrderBy_three_tables_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create,Test3.IsTests,TableName.Id,TableName.Name,TableName.Create,TableName.IsTests,Test1.Id,Test1.Name,Test1.Create,Test1.IsTest FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id RIGHT JOIN Test1 ON TableName.Id = Test1.Id WHERE Test3.Id = @Param AND TableName.IsTests = @Param ORDER BY Test1.Id,Test3.Name,Test3.IsTests,TableName.Name,TableName.IsTests ASC;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create],[Test3].[IsTests],[TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests],[Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] RIGHT JOIN [Test1] ON [TableName].[Id] = [Test1].[Id] WHERE [Test3].[Id] = @Param AND [TableName].[IsTests] = @Param ORDER BY [Test1].[Id],[Test3].[Name],[Test3].[IsTests],[TableName].[Name],[TableName].[IsTests] ASC;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}