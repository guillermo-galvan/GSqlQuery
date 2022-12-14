using GSqlQuery.Runner;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GSqlQuery.SQLServer
{
    public class SqlServerDatabaseManagmentEvents : DatabaseManagmentEvents
    {
        public override Func<Type, IEnumerable<ParameterDetail>, IEnumerable<IDataParameter>>? OnGetParameter { get; set; } = (type, parametersDetail) =>
        {
            return parametersDetail.Select(x => new SqlParameter(x.Name, x.Value));
        };
    }
}
