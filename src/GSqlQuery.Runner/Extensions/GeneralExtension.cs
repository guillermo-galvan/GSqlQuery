using GSqlQuery.Runner;
using System.Data;

namespace GSqlQuery.Runner.Extensions
{
    public static class GeneralExtension
    {
        public static IEnumerable<IDataParameter> GetParameters<T, TDbConnection>(this IQuery query,
            IDatabaseManagement<TDbConnection> databaseManagment) where T : class, new()
        {
            List<ParameterDetail> parameters = new();
            if (query.Criteria != null)
            {
                return databaseManagment.Events.GetParameter<T>(query.Criteria.Where(x => x.ParameterDetails is not null).SelectMany(x => x.ParameterDetails));
            }

            return Enumerable.Empty<IDataParameter>();
        }
    }
}
