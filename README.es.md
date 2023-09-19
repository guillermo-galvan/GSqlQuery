#    [![en](https://img.shields.io/badge/lang-en-red.svg)](./README.md) [![NuGet](https://img.shields.io/nuget/v/GSqlQuery.svg)](https://www.nuget.org/packages/GSqlQuery)

Una biblioteca para generar consultas SQL para .NET que utiliza expresiones lambda parecidas a las consultas SQL tradicionales.

## Empezar

GSqlQuery se puede instalar utilizando el administrador de [paquetes Nuget](https://www.nuget.org/packages/GSqlQuery) o la `dotnet` CLI

```shell
dotnet add package GSqlQuery --version 1.0.0-alpha
```

[Revise nuestra documentación](./docs/es/index.md) para obtener instrucciones sobre cómo utilizar el paquete.

## Ejemplo

```csharp
using GSqlQuery;

var query = Actor.Select(new Statements())
                 .Where()
                 .Equal(x => x.Actor_id, 1)
                 .AndEqual(x => x.Last_update, DateTime.Now)
                 .Build();
```

## License
Copyright (c) Guillermo Galván. All rights reserved.

Licensed under the [Apache-2.0 license](./LICENSE).