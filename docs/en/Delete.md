# Delete

This method generates the `Delete` query.

> **Note**
>All queries implement the [IQuery](IQuery.md) interface.

```csharp
using GSqlQuery;

IQuery query = City.Delete(new DefaultFormats()).Build();

Console.WriteLine("{0}", query.Text);

// output: DELETE FROM sakila.city;
```

> **Note**
>This query can use the [Where](Where.md) method.