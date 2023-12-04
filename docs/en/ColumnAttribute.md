# ColumnAttribute

It is an attribute with which a column of a database can be represented.

### Properties

|                       |                                                              |
|-----------------------|--------------------------------------------------------------|
| Name                  | Name of the column in the database.                          |
| Size                  | Database column size.                                        |
| IsPrimaryKey          | Determines if the column is a primary key.                   |
| IsAutoIncrementing    | Determines whether the column is automatically incremented.  |

## Example

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