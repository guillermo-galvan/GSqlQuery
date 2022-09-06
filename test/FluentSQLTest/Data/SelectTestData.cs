using FluentSQL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest.Data
{
    internal class Select_Test3_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] 
            {
                new FluentSQL.Default.Statements(),"SELECT TableName.Id,TableName.Name,TableName.Create,TableName.IsTests FROM TableName;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests] FROM [TableName];"
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
               new FluentSQL.Default.Statements(),"SELECT TableName.Id,TableName.Name,TableName.Create FROM TableName;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [TableName].[Id],[TableName].[Name],[TableName].[Create] FROM [TableName];"
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
                new FluentSQL.Default.Statements(),"SELECT TableName.Id,TableName.Name,TableName.Create FROM TableName WHERE TableName.IsTests = @Param AND TableName.Id = @Param;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [TableName].[Id],[TableName].[Name],[TableName].[Create] FROM [TableName] WHERE [TableName].[IsTests] = @Param AND [TableName].[Id] = @Param;"
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
                new FluentSQL.Default.Statements(),"SELECT Test1.Id,Test1.Name,Test1.Create,Test1.IsTest FROM Test1;"
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
               new FluentSQL.Default.Statements(),"SELECT Test1.Id,Test1.Name,Test1.Create FROM Test1;"
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
                new FluentSQL.Default.Statements(),"SELECT Scheme.TableName.Id,Scheme.TableName.Name,Scheme.TableName.Create,Scheme.TableName.IsTests FROM Scheme.TableName;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Scheme].[TableName].[Id],[Scheme].[TableName].[Name],[Scheme].[TableName].[Create],[Scheme].[TableName].[IsTests] FROM [Scheme].[TableName];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
