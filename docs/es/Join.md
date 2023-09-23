# Join

Este método genera la consulta `Join`.

> **Note**
>Todas las consultas implementan la interfaz [IQuery](IQuery.md).

La sentencia `Join` solo puede hacerlo con 3 tablas, si necesita realizar un `Join` con más tablas se recomienda utilizar una vista.

La consulta `Join` se puede realizar de las siguientes maneras:

## Todas las columnas

```csharp
using GSqlQuery;

IQuery query = City.Select(new DefaultFormats())
                   .InnerJoin<Country>()
                   .Equal(x => x.Table1.Country_id, x => x.Table2.Country_id)
                   .Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT sakila.city.city_id as City_city_id,sakila.city.city as City_city,sakila.city.country_id as City_country_id,sakila.city.last_update as City_last_update,sakila.country.country_id as Country_country_id,sakila.country.country as Country_country,sakila.country.last_update as Country_last_update FROM sakila.city INNER JOIN sakila.country ON sakila.city.country_id = sakila.country.country_id;
```

## Columnas especificas
```csharp
using GSqlQuery;

IQuery query = City.Select(new DefaultFormats(), x => new { x.City_id, x.Name, x.Country_id})
                   .InnerJoin<Country>(x => new { x.Country_id, x.Name})
                   .Equal(x => x.Table1.Country_id, x => x.Table2.Country_id)
                   .Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT sakila.city.city_id as City_city_id,sakila.city.city as City_city,sakila.city.country_id as City_country_id,sakila.country.country_id as Country_country_id,sakila.country.country as Country_country FROM sakila.city INNER JOIN sakila.country ON sakila.city.country_id = sakila.country.country_id;
```

> **Note**
>Esta consulta se puede utilizar el método [Where](Where.md).