using MySql.Data.MySqlClient;
using System.Data;

namespace FluentSQL.MySql
{
    public class MySqlDatabaseManagmentEvents : DatabaseManagmentEvents
    {
        public override Func<Type, IEnumerable<ParameterDetail>, IEnumerable<IDataParameter>>? OnGetParameter { get; set; } = (type, parametersDetail) =>
        {
            return parametersDetail.Select(x => new MySqlParameter(x.Name, x.Value));
        };
    }
}
