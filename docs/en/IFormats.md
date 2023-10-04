# IFormats

With this interface you can modify the format of the name of the tables or columns.

### Properties

|                               |                                                                                   |
|-------------------------------|-----------------------------------------------------------------------------------|
| Format                        | Formats the column and table name.                                                |
| ValueAutoIncrementingQuery    | Instruction to obtain the id of a column that is found as an automatic increment  |

### Methods 

|                       |                                 |
|-----------------------|---------------------------------|
| GetColumnName         | Gets the column name            |

The interface is already implemented in the DefaultFormats class.

> **Note**
> If you want to extend the `DefaultFormats` class you can do so just taking into account that the format property must have the format `{0}`.

### Example

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