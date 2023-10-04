# IQuery

Esta interfaz representa la consulta.

### Propiedades

|            |                                       |
|------------|---------------------------------------|
| Text       | Texto de la consulta                  |
| Columns    | Columnas de la consulta               |
| Criteria   | Criterios de búsqueda y sus valores   |

### Ejemplo

#### Recuperar columnas

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

#### Recuperar parámetros

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