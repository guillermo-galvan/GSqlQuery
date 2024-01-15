# GSqlQuery [![es](https://img.shields.io/badge/lang-es-red.svg)](./README.es.md) [![NuGet](https://img.shields.io/nuget/v/GSqlQuery.svg)](https://www.nuget.org/packages/GSqlQuery)

A library for generating SQL queries for .NET from models or classes that represent the database. The library uses lambda expressions similar to traditional SQL queries.

## Get Started

GSqlQuery can be installed using the [Nuget packages manager](https://www.nuget.org/packages/GSqlQuery) or the `dotnet` CLI.

```shell
dotnet add package GSqlQuery --version 1.0.0
```

[Check our documentation](./docs/en/Config.md) for instructions on how to use the package.

## Example

```csharp
using GSqlQuery;

IQuery query = Entity<Film>.Select(new DefaultFormats())
                           .Where()
                           .Equal(x => x.FilmId, 1)
                           .AndEqual(x => x.LastUpdate, DateTime.Now)
                           .Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT Film.FilmId,Film.Title,Film.Description,Film.ReleaseYear,Film.LanguageId,Film.OriginalLanguageId,Film.RentalDuration,Film.RentalRate,Film.Length,Film.ReplacementCost,Film.Rating,Film.SpecialFeatures,Film.LastUpdate FROM Film WHERE Film.FilmId = @PE0 AND Film.LastUpdate = @PE1;
```

## Contributors

GSqlQuery is actively maintained by [Guillermo Galván](https://github.com/guillermo-galvan). Contributions are welcome and can be submitted using pull request.

## License
Copyright (c) Guillermo Galván. All rights reserved.

Licensed under the [Apache-2.0 license](./LICENSE).