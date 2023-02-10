using System.Collections;
using System.Collections.Generic;

namespace GSqlQuery.Test.Data
{
    internal class Inner_Join_two_tables_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create,Test3.IsTests,TableName.Id,TableName.Name,TableName.Create,TableName.IsTests FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create],[Test3].[IsTests],[TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Inner_Join_two_tables_TestData2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,TableName.Create FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[TableName].[Create] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Inner_Join_two_tables_with_where_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create,Test3.IsTests,TableName.Id,TableName.Name,TableName.Create,TableName.IsTests FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id WHERE Test3.Id = @Param AND TableName.IsTests = @Param;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create],[Test3].[IsTests],[TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] WHERE [Test3].[Id] = @Param AND [TableName].[IsTests] = @Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Left_Join_two_tables_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create,Test3.IsTests,TableName.Id,TableName.Name,TableName.Create,TableName.IsTests FROM Test3 LEFT JOIN TableName ON Test3.Id = TableName.Id;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create],[Test3].[IsTests],[TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests] FROM [Test3] LEFT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Left_Join_two_tables_TestData2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,TableName.Create FROM Test3 LEFT JOIN TableName ON Test3.Id = TableName.Id;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[TableName].[Create] FROM [Test3] LEFT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Left_Join_two_tables_with_where_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create,Test3.IsTests,TableName.Id,TableName.Name,TableName.Create,TableName.IsTests FROM Test3 LEFT JOIN TableName ON Test3.Id = TableName.Id WHERE Test3.Id = @Param AND TableName.IsTests = @Param;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create],[Test3].[IsTests],[TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests] FROM [Test3] LEFT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] WHERE [Test3].[Id] = @Param AND [TableName].[IsTests] = @Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Right_Join_two_tables_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create,Test3.IsTests,TableName.Id,TableName.Name,TableName.Create,TableName.IsTests FROM Test3 RIGHT JOIN TableName ON Test3.Id = TableName.Id;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create],[Test3].[IsTests],[TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests] FROM [Test3] RIGHT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Right_Join_two_tables_TestData2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,TableName.Create FROM Test3 RIGHT JOIN TableName ON Test3.Id = TableName.Id;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[TableName].[Create] FROM [Test3] RIGHT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Right_Join_two_tables_with_where_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create,Test3.IsTests,TableName.Id,TableName.Name,TableName.Create,TableName.IsTests FROM Test3 RIGHT JOIN TableName ON Test3.Id = TableName.Id WHERE Test3.Id = @Param AND TableName.IsTests = @Param;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create],[Test3].[IsTests],[TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests] FROM [Test3] RIGHT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] WHERE [Test3].[Id] = @Param AND [TableName].[IsTests] = @Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Inner_Join_three_tables_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create,Test3.IsTests,TableName.Id,TableName.Name,TableName.Create,TableName.IsTests,Test1.Id,Test1.Name,Test1.Create,Test1.IsTest FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id INNER JOIN Test1 ON TableName.Id = Test1.Id;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create],[Test3].[IsTests],[TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests],[Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] INNER JOIN [Test1] ON [TableName].[Id] = [Test1].[Id];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Inner_Join_three_tables_with_where_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create,Test3.IsTests,TableName.Id,TableName.Name,TableName.Create,TableName.IsTests,Test1.Id,Test1.Name,Test1.Create,Test1.IsTest FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id INNER JOIN Test1 ON TableName.Id = Test1.Id WHERE Test3.Id = @Param AND TableName.IsTests = @Param;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create],[Test3].[IsTests],[TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests],[Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] INNER JOIN [Test1] ON [TableName].[Id] = [Test1].[Id] WHERE [Test3].[Id] = @Param AND [TableName].[IsTests] = @Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Inner_Join_three_tables_TestData2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,TableName.Create,Test1.IsTest FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id INNER JOIN Test1 ON TableName.Id = Test1.Id;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[TableName].[Create],[Test1].[IsTest] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] INNER JOIN [Test1] ON [TableName].[Id] = [Test1].[Id];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Left_Join_three_tables_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create,Test3.IsTests,TableName.Id,TableName.Name,TableName.Create,TableName.IsTests,Test1.Id,Test1.Name,Test1.Create,Test1.IsTest FROM Test3 LEFT JOIN TableName ON Test3.Id = TableName.Id LEFT JOIN Test1 ON TableName.Id = Test1.Id;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create],[Test3].[IsTests],[TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests],[Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test3] LEFT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] LEFT JOIN [Test1] ON [TableName].[Id] = [Test1].[Id];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Left_Join_three_tables_with_where_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create,Test3.IsTests,TableName.Id,TableName.Name,TableName.Create,TableName.IsTests,Test1.Id,Test1.Name,Test1.Create,Test1.IsTest FROM Test3 LEFT JOIN TableName ON Test3.Id = TableName.Id LEFT JOIN Test1 ON TableName.Id = Test1.Id WHERE Test3.Id = @Param AND TableName.IsTests = @Param;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create],[Test3].[IsTests],[TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests],[Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test3] LEFT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] LEFT JOIN [Test1] ON [TableName].[Id] = [Test1].[Id] WHERE [Test3].[Id] = @Param AND [TableName].[IsTests] = @Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Left_Join_three_tables_TestData2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,TableName.Create,Test1.IsTest FROM Test3 LEFT JOIN TableName ON Test3.Id = TableName.Id LEFT JOIN Test1 ON TableName.Id = Test1.Id;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[TableName].[Create],[Test1].[IsTest] FROM [Test3] LEFT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] LEFT JOIN [Test1] ON [TableName].[Id] = [Test1].[Id];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Right_Join_three_tables_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create,Test3.IsTests,TableName.Id,TableName.Name,TableName.Create,TableName.IsTests,Test1.Id,Test1.Name,Test1.Create,Test1.IsTest FROM Test3 RIGHT JOIN TableName ON Test3.Id = TableName.Id RIGHT JOIN Test1 ON TableName.Id = Test1.Id;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create],[Test3].[IsTests],[TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests],[Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test3] RIGHT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] RIGHT JOIN [Test1] ON [TableName].[Id] = [Test1].[Id];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Right_Join_three_tables_with_where_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,Test3.Create,Test3.IsTests,TableName.Id,TableName.Name,TableName.Create,TableName.IsTests,Test1.Id,Test1.Name,Test1.Create,Test1.IsTest FROM Test3 RIGHT JOIN TableName ON Test3.Id = TableName.Id RIGHT JOIN Test1 ON TableName.Id = Test1.Id WHERE Test3.Id = @Param AND TableName.IsTests = @Param;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[Test3].[Create],[Test3].[IsTests],[TableName].[Id],[TableName].[Name],[TableName].[Create],[TableName].[IsTests],[Test1].[Id],[Test1].[Name],[Test1].[Create],[Test1].[IsTest] FROM [Test3] RIGHT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] RIGHT JOIN [Test1] ON [TableName].[Id] = [Test1].[Id] WHERE [Test3].[Id] = @Param AND [TableName].[IsTests] = @Param;"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Right_Join_three_tables_TestData2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Statements(),"SELECT Test3.Id,Test3.Name,TableName.Create,Test1.IsTest FROM Test3 RIGHT JOIN TableName ON Test3.Id = TableName.Id RIGHT JOIN Test1 ON TableName.Id = Test1.Id;"
            };

            yield return new object[]
            {
                new Models.Statements(),"SELECT [Test3].[Id],[Test3].[Name],[TableName].[Create],[Test1].[IsTest] FROM [Test3] RIGHT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] RIGHT JOIN [Test1] ON [TableName].[Id] = [Test1].[Id];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
