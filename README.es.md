# GSqlQuery [![en](https://img.shields.io/badge/lang-en-red.svg)](./README.md) [![NuGet](https://img.shields.io/nuget/v/GSqlQuery.svg)](https://www.nuget.org/packages/GSqlQuery)

Una biblioteca para generar consultas SQL para .NET que utiliza expresiones lambda parecidas a las consultas SQL tradicionales.

## Empezar

GSqlQuery se puede instalar utilizando el administrador de [paquetes Nuget](https://www.nuget.org/packages/GSqlQuery) o la `dotnet` CLI

```shell
dotnet add package GSqlQuery --version 1.0.0-alpha
```

[Revise nuestra documentación](./docs/GSqlQuery/es/index.md) para obtener instrucciones sobre cómo utilizar el paquete.

## Ejemplo

```csharp
[Theory]
[ClassData(typeof(Select_Test3_TestData3))]
public void Should_return_the_query_with_where(IStatements statements, string queryText)
{
    var query = Test3.Select(statements, x => new { x.Ids, x.Names, x.Creates }).Where().Equal(x => x.IsTests, true).AndEqual(x => x.Ids, 12).Build();
    Assert.NotNull(query);
    Assert.NotEmpty(query.Text);

    string result = query.Text;
    if (query.Criteria != null)
    {
        foreach (var item in query.Criteria)
        {
            result = item.ParameterDetails.ParameterReplace(result);
        }
    }

    Assert.Equal(queryText, result);
}
```

## License
Copyright (c) guillermo-galvan. All rights reserved.

Licensed under the [Apache-2.0 license](./LICENSE).