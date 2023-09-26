# Update

This method generates the `Update` query.

> **Note**
>All queries implement the [IQuery](IQuery.md) interface.

The `Update` query can be performed in the following ways:

## Static methods

```csharp
using GSqlQuery;

IQuery query = City.Update(new DefaultFormats(), x => x.Name, "Adana").Build();

Console.WriteLine("{0}", query.Text);

// output: UPDATE sakila.city SET sakila.city.city=@PU0;
```

```csharp
using GSqlQuery;

IQuery query = City.Update(new DefaultFormats(), x => x.Name, "Adana")
                   .Set(x => x.Last_update, DateTime.Now)
                   .Build();

Console.WriteLine("{0}", query.Text);

// output: UPDATE sakila.city SET sakila.city.city=@PU0,sakila.city.last_update=@PU1;
```

## From an instance of the object

```csharp
using GSqlQuery;

City city = new City
{
    City_id = 1,
    Name = "A Corua (La Corua)",
    Country_id = 87,
    Last_update = DateTime.Now
};

IQuery query = citycity.Update(new DefaultFormats(), x => x.Name)
                       .Set(x => x.Last_update)
                       .Build();

Console.WriteLine("{0}", query.Text);

// output: UPDATE sakila.city SET sakila.city.city=@PU0,sakila.city.last_update=@PU1;
```

```csharp
using GSqlQuery;

City city = new City
{
    City_id = 1,
    Name = "A Corua (La Corua)",
    Country_id = 87,
    Last_update = DateTime.Now
};

IQuery query = city.Update(new DefaultFormats(), x => new { x.City_id, x.Name, x.Country_id, x.Last_update})
                   .Build();

Console.WriteLine("{0}", query.Text);

// output: UPDATE sakila.city SET sakila.city.city_id=@PU0,sakila.city.city=@PU1,sakila.city.country_id=@PU2,sakila.city.last_update=@PU3;
```

> **Note**
>This query can use the [Where](Where.md) method.