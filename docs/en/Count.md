# Count

This method generates the `Count` query.

> **Note**
>All queries implement the [IQuery](IQuery.md) interface.

The `Count` query can be performed in the following ways:

## All columns

```csharp
using GSqlQuery;

IQuery query = City.Select(new DefaultFormats()).Count().Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT COUNT(sakila.city.city_id,sakila.city.city,sakila.city.country_id,sakila.city.last_update) FROM sakila.city;
```

## Specific columns
```csharp
using GSqlQuery;

IQuery query = City.Select(new DefaultFormats(), x => new { x.City_id }).Count().Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT COUNT(sakila.city.city_id) FROM sakila.city;
```

> **Note**
>This query can use the [Where](Where.md) method.