# Select

Este método genera la consulta `Select`.

> **Note**
>Todas las consultas implementan la interfaz [IQuery](IQuery.md).

La consulta `Select` se puede realizar de las siguientes maneras:

## Todas las columnas

```csharp
using GSqlQuery;

IQuery query = City.Select(new DefaultFormats()).Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT sakila.city.city_id,sakila.city.city,sakila.city.country_id,sakila.city.last_update FROM sakila.city;
```

## Columnas específicas
```csharp
using GSqlQuery;

IQuery query = City.Select(new DefaultFormats(), x => new { x.City_id , x.Country_id}).Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT sakila.city.city_id,sakila.city.country_id FROM sakila.city;
```

> **Note**
>Esta consulta se puede utilizar el método [Where](Where.md).