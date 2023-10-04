# Entity

Con la clase entity usted puede generar las consultas de los modelos y clases.

### Métodos

|         |                                         |
|---------|-----------------------------------------|
| Insert  | Método para generar la consulta insert  |
| Update  | Método para generar la consulta update  |

### Métodos estáticos

|         |                                         |
|---------|-----------------------------------------|
| Select  | Método para generar la consulta select  |
| Insert  | Método para generar la consulta insert  |
| Update  | Método para generar la consulta update  |
| Delete  | Método para generar la consulta delete  |


## Ejemplos

### Métodos estáticos

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

### Extendiendo de la clase

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