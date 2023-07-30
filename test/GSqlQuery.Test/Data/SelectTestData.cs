using System.Collections;
using System.Collections.Generic;

namespace GSqlQuery.Test.Data
{
    internal class Select_Test3_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create,Test3.IsTests FROM Test3;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create],[Test3].[IsTests] FROM [Test3];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Select_Test3_TestData2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
               new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create FROM Test3;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create] FROM [Test3];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Select_Test3_TestData3 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create FROM Test3 WHERE Test3.IsTests = @Param AND Test3.Id = @Param;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create] FROM [Test3] WHERE [Test3].[IsTests] = @Param AND [Test3].[Id] = @Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Select_Test1_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test1.Id,Test1.Name,Test1.Create,Test1.IsTest FROM Test1;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test1];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Select_Test1_TestData2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
               new Statements(),"SELECT Test1.Id,Test1.Name,Test1.Create FROM Test1;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test1].[Id],[Test1].[Name],[Test1].[Create] FROM [Test1];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Select_Test4_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Scheme.TableName.Id,Scheme.TableName.Name,Scheme.TableName.Create,Scheme.TableName.IsTests FROM Scheme.TableName;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Scheme].[TableName].[Id],[Scheme].[TableName].[Name],[Scheme].[TableName].[Create],[Scheme].[TableName].[IsTests] FROM [Scheme].[TableName];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}