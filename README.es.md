# GSqlQuery [![en](https://img.shields.io/badge/lang-en-red.svg)](./README.md) [![NuGet](https://img.shields.io/nuget/v/GSqlQuery.svg)](https://www.nuget.org/packages/GSqlQuery)

Una biblioteca para generar consultas Sql para .NET a partir modelos o clases que represente la base de datos, la biblioteca utiliza expresiones lambda parecidas a las consultas SQL tradicionales.

## Empezar

GSqlQuery se puede instalar utilizando el administrador de [paquetes Nuget](https://www.nuget.org/packages/GSqlQuery) o la `dotnet` CLI

```shell
dotnet add package GSqlQuery --version 1.1.0-beta2
```

[Revise nuestra documentación](./docs/es/Config.md) para obtener instrucciones sobre cómo utilizar el paquete.

## Ejemplo

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

## Colaborar

GSqlQuery es mantenido activamente por [Guillermo Galván](https://github.com/guillermo-galvan). Las contribuciones son bienvenidas y se pueden enviar mediante pull request.

## Licencia
Copyright (c) Guillermo Galván. All rights reserved.

Licensed under the [Apache-2.0 license](./LICENSE).