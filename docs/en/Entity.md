# Entity

With the entity class you can generate queries for models and classes.

### Methods

|         |                                         |
|---------|-----------------------------------------|
| Insert  | Method to generate the query insert.     |
| Update  | Method to generate the update query.    |

### Static methods

|         |                                         |
|---------|-----------------------------------------|
| Select  | Method to generate the Select query  |
| Insert  | Method to generate the Insert query  |
| Update  | Method to generate the Update query  |
| Delete  | Method to generate the Delete query  |


## Example

### Static methods

```csharp
using GSqlQuery;

[Table("sakila", "city")]
public class City
{
    [Column("city_id", Size = 5, IsAutoIncrementing = true, IsPrimaryKey = true)]
    public long City_id { get; set; }

    [Column("city", Size = 50)]
    public string Name { get; set; }

    [Column("country_id", Size = 5)]
    public long Country_id { get; set; }

    [Column("last_update", Size = 19)]
    public DateTime Last_update { get; set; }
}

IQuery query = Entity<City>.Select(new DefaultFormats()).Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT sakila.city.CityId,sakila.city.Name,sakila.city.CountryId,sakila.city.LastUpdate FROM sakila.city;
```

### Expanding the class

```csharp
using GSqlQuery;

[Table("sakila", "city")]
public class City : Entity<City>
{
    [Column("city_id", Size = 5, IsAutoIncrementing = true, IsPrimaryKey = true)]
    public long City_id { get; set; }

    [Column("city", Size = 50)]
    public string Name { get; set; }

    [Column("country_id", Size = 5)]
    public long Country_id { get; set; }

    [Column("last_update", Size = 19)]
    public DateTime Last_update { get; set; }
}

IQuery query = City.Update(new DefaultFormats(), x => x.Name, "Abha")
                   .Where()
                   .Equal(x => x.City_id, 1)
                   .Build();

Console.WriteLine("{0}", query.Text);

// output: UPDATE sakila.city SET sakila.city.city=@PU0 WHERE sakila.city.city_id = @PE4;
```