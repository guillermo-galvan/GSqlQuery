using GSqlQuery.Runner;
using Microsoft.Data.Sqlite;
using System.Data;

namespace GSqlQuery.Sqlite
{
    public class SqliteDatabaseManagmentEvents : DatabaseManagmentEvents
    {
        public override Func<Type, IEnumerable<ParameterDetail>, IEnumerable<IDataParameter>>? OnGetParameter { get; set; } = (type, parametersDetail) =>
        {
            return parametersDetail.Select(x => new SqliteParameter(x.Name, x.Value));
        };
    }
}
