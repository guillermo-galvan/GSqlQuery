# TableAttribute

It is an attribute with which you can name the table, to generate queries.

### Properties

|         |                                          |
|---------|------------------------------------------|
| Name    | Name of the table in the database.       |
| Scheme  | Name of the database schema.             |

## Example

### No scheme

```csharp
using GSqlQuery;

[Table("city")]
public class City
{
    public long CityId { get; set; }

    public string Name { get; set; }

    public long CountryId { get; set; }

    public DateTime LastUpdate { get; set; }
}

IQuery query = Entity<City>.Select(new DefaultFormats()).Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT city.CityId,city.Name,city.CountryId,city.LastUpdate FROM city;
```

### With scheme

```csharp
using GSqlQuery;

[Table("sakila", "city")]
public class City
{
    public long CityId { get; set; }

    public string Name { get; set; }

    public long CountryId { get; set; }

    public DateTime LastUpdate { get; set; }
}

IQuery query = Entity<City>.Select(new DefaultFormats()).Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT sakila.city.CityId,sakila.city.Name,sakila.city.CountryId,sakila.city.LastUpdate FROM sakila.city;
```