# ColumnAttribute

Es un atributo con el cual le podrá representar una columna de la base de datos.

### Propiedades

|                       |                                                |
|-----------------------|------------------------------------------------|
| Name                  | Nombre de la columna en la base de datos.      |
| Size                  | Tamaño de la columna de la base de datos.      |
| IsPrimaryKey          | Determina si la columna es un llave primaria.  |
| IsAutoIncrementing    | Determina si la columna se auto incrementada.  |

## Ejemplo

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