# Insert

This method generates the `Insert` query.

> **Note**
>All queries implement the [IQuery](IQuery.md) interface.

# Example

## Static methods
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

IQuery query = City.Insert(new DefaultFormats()).Build();

Console.WriteLine("{0}", query.Text);

// output: INSERT INTO sakila.city (sakila.city.city,sakila.city.country_id,sakila.city.last_update) VALUES (@PI0,@PI1,@PI2);
```