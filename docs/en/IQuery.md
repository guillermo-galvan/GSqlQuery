# IQuery

This interface represents the query.

### Properties

|            |                                       |
|------------|---------------------------------------|
| Text       | Query text                            |
| Columns    | Query columns                         |
| Criteria   | Search criteria and their values      |

### Example

#### Retrieve columns

```csharp
public static void WriteColumns(IQuery query)
{
    Console.WriteLine("---------------------------------------Columns--------------------------------------------------");

    foreach (var item in query.Columns)
    {
        Console.WriteLine("Name: {0} Size: {1} IsPrimaryKey: {2} IsAutoIncrementing: {3}",
            item.ColumnAttribute.Name, item.ColumnAttribute.Size, item.ColumnAttribute.IsPrimaryKey, item.ColumnAttribute.IsAutoIncrementing);
    }
    Console.WriteLine("------------------------------------------------------------------------------------------------");
    Console.WriteLine();
}
```

#### Retrieve parameters

```csharp
public static void WriteParameters(IQuery query)
{
    Console.WriteLine("---------------------------------------Parameters-----------------------------------------------");

    foreach (var item in query.Criteria.SelectMany(x => x.ParameterDetails))
    {
        Console.WriteLine("Name: {0} Value: {1}", item.Name, item.Value);
    }

    Console.WriteLine("------------------------------------------------------------------------------------------------");
    Console.WriteLine();
}
```