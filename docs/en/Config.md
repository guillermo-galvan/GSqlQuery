# Setting

In this section you will find an explanation of how to use the package.

## Formats

The package has an interface [IFormats](IFormats.md) with which you can edit the name format of tables or columns.

## Name of tables and columns

By default the name of the table is taken from the name of the class and the name of the columns is taken from the name of the properties.

```csharp
using GSqlQuery;

public class City
{
    public long CityId { get; set; }

    public string Name { get; set; }

    public long CountryId { get; set; }

    public DateTime LastUpdate { get; set; }
}

IQuery query = Entity<City>.Select(new DefaultFormats()).Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT City.CityId,City.Name,City.CountryId,City.LastUpdate FROM City;
```

We can change this behavior with the attributes [TableAttribute](TableAttribute.md) and [ColumnAttribute](ColumnAttribute.md).

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
    public TimeSpan Last_update { get; set; }
}

IQuery query = Entity<City>.Select(new DefaultFormats()).Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT sakila.city.city_id,sakila.city.city,sakila.city.country_id,sakila.city.last_update FROM sakila.city;
```

## Entity Class

To simplify writing the code a little you can use the [Entity](Entity.md) class.

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
    public TimeSpan Last_update { get; set; }
}

IQuery query = City.Select(new DefaultFormats()).Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT sakila.city.city_id,sakila.city.city,sakila.city.country_id,sakila.city.last_update FROM sakila.city;
```

# Queries

Once we have configured our modules or classes we can generate the queries:

> **Note**
>All queries implement the [IQuery](IQuery.md) interface.

- [Insert](Insert.md)
- [Update](Update.md)
- [Select](Select.md)
- [Delete](Delete.md)
- [Count](Count.md)
- [Join](Join.md) 

## Insert

```csharp
using GSqlQuery;

IQuery query = City.Insert(new DefaultFormats(), new City
{
    City_id = 1,
    Name = "A Corua (La Corua)",
    Country_id = 87,
    Last_update = DateTime.Now
}).Build();

Console.WriteLine("{0}", query.Text);

// output: INSERT INTO sakila.city (sakila.city.city,sakila.city.country_id,sakila.city.last_update) VALUES (@PI0,@PI1,@PI2);
```

## Update

```csharp
using GSqlQuery;

IQuery query = City.Update(new DefaultFormats(), x => x.Name, "Abha")
                   .Where()
                   .Equal(x => x.City_id, 1)
                   .Build();

Console.WriteLine("{0}", query.Text);

// output: UPDATE sakila.city SET sakila.city.city=@PU0 WHERE sakila.city.city_id = @PE4;
```

## Select

```csharp
using GSqlQuery;

IQuery query = City.Select(new DefaultFormats())
                   .Where()
                   .Equal(x => x.City_id, 1)
                   .Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT sakila.city.city_id,sakila.city.city,sakila.city.country_id,sakila.city.last_update FROM sakila.city WHERE sakila.city.city_id = @PE4;
```
## Delete

```csharp
using GSqlQuery;

IQuery query = City.Delete(new DefaultFormats())
                   .Where()
                   .Equal(x => x.City_id, 1)
                   .Build();

Console.WriteLine("{0}", query.Text);

// output: DELETE FROM sakila.city WHERE sakila.city.city_id = @PE4;
```
## Count

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

## Join

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
> **Note**
>The `Join` query can only be performed with 3 tables, if you need to perform a `Join` with more tables it is recommended to use a view.