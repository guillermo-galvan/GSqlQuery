# Insert

Este método genera la consulta `Insert`.

> **Note**
>Todas las consultas implementan la interfaz [IQuery](IQuery.md).

# Ejemplos

## Métodos estáticos
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

## A partir de una instancia del objecto
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