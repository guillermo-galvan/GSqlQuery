# Where

The `Where` method is only accessible by the [Select](Select.md), [Update](Update.md), [Delete](Delete.md), [Count](Count.md) and [Join](Join.md) queries.

## Example

### Update

```csharp
using GSqlQuery;

IQuery query = City.Update(new DefaultFormats(), x => x.Name, "Abha")
                   .Where()
                   .Equal(x => x.City_id, 1)
                   .Build();

Console.WriteLine("{0}", query.Text);

// output: UPDATE sakila.city SET sakila.city.city=@PU0 WHERE sakila.city.city_id = @PE4;
```

### Select

```csharp
using GSqlQuery;

IQuery query = City.Select(new DefaultFormats())
                   .Where()
                   .Equal(x => x.City_id, 1)
                   .Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT sakila.city.city_id,sakila.city.city,sakila.city.country_id,sakila.city.last_update FROM sakila.city WHERE sakila.city.city_id = @PE4;
```
### Delete

```csharp
using GSqlQuery;

IQuery query = City.Delete(new DefaultFormats())
                   .Where()
                   .Equal(x => x.City_id, 1)
                   .Build();

Console.WriteLine("{0}", query.Text);

// output: DELETE FROM sakila.city WHERE sakila.city.city_id = @PE4;
```
### Count

```csharp
using GSqlQuery;

IQuery query = City.Select(new DefaultFormats(), x => x.Name)
                   .Count()
                   .Where()
                   .Equal(x => x.City_id, 1)
                   .Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT COUNT(sakila.city.city) FROM sakila.city WHERE sakila.city.city_id = @PE4;
```

### Join

```csharp
using GSqlQuery;

IQuery query = City.Select(new DefaultFormats())
                   .InnerJoin<Country>()
                   .Equal(x => x.Table1.Country_id, x => x.Table2.Country_id)
                   .Where()
                   .Equal(x => x.Table1.City_id, 1)
                   .Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT sakila.city.city_id as City_city_id,sakila.city.city as City_city,sakila.city.country_id as City_country_id,sakila.city.last_update as City_last_update,sakila.country.country_id as Country_country_id,sakila.country.country as Country_country,sakila.country.last_update as Country_last_update FROM sakila.city INNER JOIN sakila.country ON sakila.city.country_id = sakila.country.country_id WHERE sakila.city.city_id = @PE4;
```