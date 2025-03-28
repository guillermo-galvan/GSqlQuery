﻿using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace GSqlQuery.Runner.Test.Data
{
    internal class Inner_Join_two_tables_TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ConnectionOptions<IDbConnection>(new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id;"
            };

            yield return new object[]
            {
               new ConnectionOptions<IDbConnection>(new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] as [Test3_Id],[Test3].[Name] as [Test3_Name],[Test3].[Create] as [Test3_Create],[Test3].[IsTests] as [Test3_IsTests],[TableName].[Id] as [Test6_Id],[TableName].[Name] as [Test6_Name],[TableName].[Create] as [Test6_Create],[TableName].[IsTests] as [Test6_IsTests] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id];"
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
                new ConnectionOptions<IDbConnection>(new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Name as Test3_Name,Test3.Id as Test3_Id,TableName.Create as Test6_Create FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Name] as [Test3_Name],[Test3].[Id] as [Test3_Id],[TableName].[Create] as [Test6_Create] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id];"
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
                new ConnectionOptions < IDbConnection > (new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id WHERE Test3.Id = @Param AND TableName.IsTests = @Param;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] as [Test3_Id],[Test3].[Name] as [Test3_Name],[Test3].[Create] as [Test3_Create],[Test3].[IsTests] as [Test3_IsTests],[TableName].[Id] as [Test6_Id],[TableName].[Name] as [Test6_Name],[TableName].[Create] as [Test6_Create],[TableName].[IsTests] as [Test6_IsTests] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] WHERE [Test3].[Id] = @Param AND [TableName].[IsTests] = @Param;"
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
                new ConnectionOptions < IDbConnection > (new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test3 LEFT JOIN TableName ON Test3.Id = TableName.Id;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] as [Test3_Id],[Test3].[Name] as [Test3_Name],[Test3].[Create] as [Test3_Create],[Test3].[IsTests] as [Test3_IsTests],[TableName].[Id] as [Test6_Id],[TableName].[Name] as [Test6_Name],[TableName].[Create] as [Test6_Create],[TableName].[IsTests] as [Test6_IsTests] FROM [Test3] LEFT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id];"
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
                new ConnectionOptions < IDbConnection > (new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Name as Test3_Name,Test3.Id as Test3_Id,TableName.Create as Test6_Create FROM Test3 LEFT JOIN TableName ON Test3.Id = TableName.Id;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Name] as [Test3_Name],[Test3].[Id] as [Test3_Id],[TableName].[Create] as [Test6_Create] FROM [Test3] LEFT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id];"
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
                new ConnectionOptions < IDbConnection > (new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test3 LEFT JOIN TableName ON Test3.Id = TableName.Id WHERE Test3.Id = @Param AND TableName.IsTests = @Param;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] as [Test3_Id],[Test3].[Name] as [Test3_Name],[Test3].[Create] as [Test3_Create],[Test3].[IsTests] as [Test3_IsTests],[TableName].[Id] as [Test6_Id],[TableName].[Name] as [Test6_Name],[TableName].[Create] as [Test6_Create],[TableName].[IsTests] as [Test6_IsTests] FROM [Test3] LEFT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] WHERE [Test3].[Id] = @Param AND [TableName].[IsTests] = @Param;"
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
                new ConnectionOptions < IDbConnection > (new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test3 RIGHT JOIN TableName ON Test3.Id = TableName.Id;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] as [Test3_Id],[Test3].[Name] as [Test3_Name],[Test3].[Create] as [Test3_Create],[Test3].[IsTests] as [Test3_IsTests],[TableName].[Id] as [Test6_Id],[TableName].[Name] as [Test6_Name],[TableName].[Create] as [Test6_Create],[TableName].[IsTests] as [Test6_IsTests] FROM [Test3] RIGHT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id];"
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
                new ConnectionOptions < IDbConnection > (new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Name as Test3_Name,Test3.Id as Test3_Id,TableName.Create as Test6_Create FROM Test3 RIGHT JOIN TableName ON Test3.Id = TableName.Id;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Name] as [Test3_Name],[Test3].[Id] as [Test3_Id],[TableName].[Create] as [Test6_Create] FROM [Test3] RIGHT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id];"
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
                new ConnectionOptions < IDbConnection > (new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests FROM Test3 RIGHT JOIN TableName ON Test3.Id = TableName.Id WHERE Test3.Id = @Param AND TableName.IsTests = @Param;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] as [Test3_Id],[Test3].[Name] as [Test3_Name],[Test3].[Create] as [Test3_Create],[Test3].[IsTests] as [Test3_IsTests],[TableName].[Id] as [Test6_Id],[TableName].[Name] as [Test6_Name],[TableName].[Create] as [Test6_Create],[TableName].[IsTests] as [Test6_IsTests] FROM [Test3] RIGHT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] WHERE [Test3].[Id] = @Param AND [TableName].[IsTests] = @Param;"
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
                new ConnectionOptions < IDbConnection > (new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests,Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id INNER JOIN Test1 ON TableName.Id = Test1.Id;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] as [Test3_Id],[Test3].[Name] as [Test3_Name],[Test3].[Create] as [Test3_Create],[Test3].[IsTests] as [Test3_IsTests],[TableName].[Id] as [Test6_Id],[TableName].[Name] as [Test6_Name],[TableName].[Create] as [Test6_Create],[TableName].[IsTests] as [Test6_IsTests],[Test1].[Id] as [Test1_Id],[Test1].[Name] as [Test1_Name],[Test1].[Create] as [Test1_Create],[Test1].[IsTest] as [Test1_IsTest] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] INNER JOIN [Test1] ON [TableName].[Id] = [Test1].[Id];"
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
                new ConnectionOptions < IDbConnection > (new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests,Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id INNER JOIN Test1 ON TableName.Id = Test1.Id WHERE Test3.Id = @Param AND TableName.IsTests = @Param;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] as [Test3_Id],[Test3].[Name] as [Test3_Name],[Test3].[Create] as [Test3_Create],[Test3].[IsTests] as [Test3_IsTests],[TableName].[Id] as [Test6_Id],[TableName].[Name] as [Test6_Name],[TableName].[Create] as [Test6_Create],[TableName].[IsTests] as [Test6_IsTests],[Test1].[Id] as [Test1_Id],[Test1].[Name] as [Test1_Name],[Test1].[Create] as [Test1_Create],[Test1].[IsTest] as [Test1_IsTest] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] INNER JOIN [Test1] ON [TableName].[Id] = [Test1].[Id] WHERE [Test3].[Id] = @Param AND [TableName].[IsTests] = @Param;"
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
                new ConnectionOptions < IDbConnection > (new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id as Test3_Id,Test3.Name as Test3_Name,TableName.Create as Test6_Create,Test1.IsTest as Test1_IsTest FROM Test3 INNER JOIN TableName ON Test3.Id = TableName.Id INNER JOIN Test1 ON TableName.Id = Test1.Id;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] as [Test3_Id],[Test3].[Name] as [Test3_Name],[TableName].[Create] as [Test6_Create],[Test1].[IsTest] as [Test1_IsTest] FROM [Test3] INNER JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] INNER JOIN [Test1] ON [TableName].[Id] = [Test1].[Id];"
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
                new ConnectionOptions < IDbConnection > (new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests,Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest FROM Test3 LEFT JOIN TableName ON Test3.Id = TableName.Id LEFT JOIN Test1 ON TableName.Id = Test1.Id;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] as [Test3_Id],[Test3].[Name] as [Test3_Name],[Test3].[Create] as [Test3_Create],[Test3].[IsTests] as [Test3_IsTests],[TableName].[Id] as [Test6_Id],[TableName].[Name] as [Test6_Name],[TableName].[Create] as [Test6_Create],[TableName].[IsTests] as [Test6_IsTests],[Test1].[Id] as [Test1_Id],[Test1].[Name] as [Test1_Name],[Test1].[Create] as [Test1_Create],[Test1].[IsTest] as [Test1_IsTest] FROM [Test3] LEFT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] LEFT JOIN [Test1] ON [TableName].[Id] = [Test1].[Id];"
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
                new ConnectionOptions < IDbConnection > (new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests,Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest FROM Test3 LEFT JOIN TableName ON Test3.Id = TableName.Id LEFT JOIN Test1 ON TableName.Id = Test1.Id WHERE Test3.Id = @Param AND TableName.IsTests = @Param;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] as [Test3_Id],[Test3].[Name] as [Test3_Name],[Test3].[Create] as [Test3_Create],[Test3].[IsTests] as [Test3_IsTests],[TableName].[Id] as [Test6_Id],[TableName].[Name] as [Test6_Name],[TableName].[Create] as [Test6_Create],[TableName].[IsTests] as [Test6_IsTests],[Test1].[Id] as [Test1_Id],[Test1].[Name] as [Test1_Name],[Test1].[Create] as [Test1_Create],[Test1].[IsTest] as [Test1_IsTest] FROM [Test3] LEFT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] LEFT JOIN [Test1] ON [TableName].[Id] = [Test1].[Id] WHERE [Test3].[Id] = @Param AND [TableName].[IsTests] = @Param;"
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
                new ConnectionOptions < IDbConnection > (new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Name as Test3_Name,Test3.Id as Test3_Id,TableName.Create as Test6_Create,Test1.IsTest as Test1_IsTest FROM Test3 LEFT JOIN TableName ON Test3.Id = TableName.Id LEFT JOIN Test1 ON TableName.Id = Test1.Id;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Name] as [Test3_Name],[Test3].[Id] as [Test3_Id],[TableName].[Create] as [Test6_Create],[Test1].[IsTest] as [Test1_IsTest] FROM [Test3] LEFT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] LEFT JOIN [Test1] ON [TableName].[Id] = [Test1].[Id];"
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
                new ConnectionOptions < IDbConnection > (new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests,Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest FROM Test3 RIGHT JOIN TableName ON Test3.Id = TableName.Id RIGHT JOIN Test1 ON TableName.Id = Test1.Id;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] as [Test3_Id],[Test3].[Name] as [Test3_Name],[Test3].[Create] as [Test3_Create],[Test3].[IsTests] as [Test3_IsTests],[TableName].[Id] as [Test6_Id],[TableName].[Name] as [Test6_Name],[TableName].[Create] as [Test6_Create],[TableName].[IsTests] as [Test6_IsTests],[Test1].[Id] as [Test1_Id],[Test1].[Name] as [Test1_Name],[Test1].[Create] as [Test1_Create],[Test1].[IsTest] as [Test1_IsTest] FROM [Test3] RIGHT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] RIGHT JOIN [Test1] ON [TableName].[Id] = [Test1].[Id];"
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
                new ConnectionOptions < IDbConnection > (new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Id as Test3_Id,Test3.Name as Test3_Name,Test3.Create as Test3_Create,Test3.IsTests as Test3_IsTests,TableName.Id as Test6_Id,TableName.Name as Test6_Name,TableName.Create as Test6_Create,TableName.IsTests as Test6_IsTests,Test1.Id as Test1_Id,Test1.Name as Test1_Name,Test1.Create as Test1_Create,Test1.IsTest as Test1_IsTest FROM Test3 RIGHT JOIN TableName ON Test3.Id = TableName.Id RIGHT JOIN Test1 ON TableName.Id = Test1.Id WHERE Test3.Id = @Param AND TableName.IsTests = @Param;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Id] as [Test3_Id],[Test3].[Name] as [Test3_Name],[Test3].[Create] as [Test3_Create],[Test3].[IsTests] as [Test3_IsTests],[TableName].[Id] as [Test6_Id],[TableName].[Name] as [Test6_Name],[TableName].[Create] as [Test6_Create],[TableName].[IsTests] as [Test6_IsTests],[Test1].[Id] as [Test1_Id],[Test1].[Name] as [Test1_Name],[Test1].[Create] as [Test1_Create],[Test1].[IsTest] as [Test1_IsTest] FROM [Test3] RIGHT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] RIGHT JOIN [Test1] ON [TableName].[Id] = [Test1].[Id] WHERE [Test3].[Id] = @Param AND [TableName].[IsTests] = @Param;"
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
                new ConnectionOptions < IDbConnection > (new DefaultFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT Test3.Name as Test3_Name,Test3.Id as Test3_Id,TableName.Create as Test6_Create,Test1.IsTest as Test1_IsTest FROM Test3 RIGHT JOIN TableName ON Test3.Id = TableName.Id RIGHT JOIN Test1 ON TableName.Id = Test1.Id;"
            };

            yield return new object[]
            {
                new ConnectionOptions < IDbConnection > (new Models.TestFormats(), LoadGSqlQueryOptions.GetDatabaseManagmentMock()),"SELECT [Test3].[Name] as [Test3_Name],[Test3].[Id] as [Test3_Id],[TableName].[Create] as [Test6_Create],[Test1].[IsTest] as [Test1_IsTest] FROM [Test3] RIGHT JOIN [TableName] ON [Test3].[Id] = [TableName].[Id] RIGHT JOIN [Test1] ON [TableName].[Id] = [Test1].[Id];"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}