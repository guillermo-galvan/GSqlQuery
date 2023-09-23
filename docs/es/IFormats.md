# IFormats

Con esta interfaz se puede modificar el formato del nombre de la tablas o columnas.

### Propiedades

|                               |                                                                                            |
|-------------------------------|--------------------------------------------------------------------------------------------|
| Format                        | Da el formato al nombre de la columna y tabla                                              |
| ValueAutoIncrementingQuery    | instrucción para obtener el id de una columna que se encuentre como incremento automático  |


### Métodos 

|                       |                                 |
|-----------------------|---------------------------------|
| GetColumnName         | Obtiene el nombre de la columna |

La interfaz ya se encuentra implementada en la clase DefaultFormats.

> **Note**
> Si usted desea extender de la clase `DefaultFormats` lo puede realizar nada más tome en cuenta que la propiedad format debe de tener el formato `{0}`.

### Ejemplo

```csharp
using GSqlQuery;

public class ModifyStatements : DefaultFormats
{
    public override string Format => "[{0}]";;
}

IQuery query = Entity<Film>.Select(new ModifyDefaultFormats())
                           .Where()
                           .Equal(x => x.FilmId, 1)
                           .AndEqual(x => x.LastUpdate, DateTime.Now)
                           .Build();

Console.WriteLine("{0}", query.Text);

// output: SELECT [Film].[FilmId],[Film].[Title],[Film].[Description],[Film].[ReleaseYear],[Film].[LanguageId],[Film].[OriginalLanguageId],[Film].[RentalDuration],[Film].[RentalRate],[Film].[Length],[Film].[ReplacementCost],[Film].[Rating],[Film].[SpecialFeatures],[Film].[LastUpdate] FROM [Film] WHERE [Film].[FilmId] = @PE0 AND [Film].[LastUpdate] = @PE1;
```