# Delete

Este método genera la consulta `Delete`.

> **Note**
>Todas las consultas implementan la interfaz [IQuery](IQuery.md).

```csharp
using GSqlQuery;

IQuery query = City.Delete(new DefaultFormats()).Build();

Console.WriteLine("{0}", query.Text);

// output: DELETE FROM sakila.city;
```

> **Note**
>Esta consulta se puede utilizar el método [Where](Where.md).