# TableAttribute

Es un atributo con el cual le podrá poner el nombre a la tabla, para la generación de consultas.

### Propiedades

|         |                                          |
|---------|------------------------------------------|
| Name    | Nombre de la tabla en la base de datos.  |
| Scheme  | Nombre del esquema de la base de datos.  |

## Ejemplo

### Sin esquema

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

### Con esquema

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