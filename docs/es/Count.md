# Count

Este método genera la consulta `Count`.

> **Note**
>Todas las consultas implementan la interfaz [IQuery](IQuery.md).

La consulta `Count` se puede realizar de las siguientes maneras:

## Todas las columnas

```csharp
using GSqlQuery;

IQuery query = City.Select(new DefaultFormats()).Count().Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT COUNT(sakila.city.city_id,sakila.city.city,sakila.city.country_id,sakila.city.last_update) FROM sakila.city;
```

## Columnas específicas
```csharp
using GSqlQuery;

IQuery query = City.Select(new DefaultFormats(), x => new { x.City_id }).Count().Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT COUNT(sakila.city.city_id) FROM sakila.city;
```

> **Note**
>Esta consulta se puede utilizar el método [Where](Where.md).