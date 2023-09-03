using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GSqlQuery.Runner.Extensions
{
    public static class GeneralExtension
    {
        public static IEnumerable<IDataParameter> GetParameters<T, TDbConnection>(this IQuery query,
            IDatabaseManagement<TDbConnection> databaseManagement) where T : class, new()
        {
            List<ParameterDetail> parameters = new List<ParameterDetail>();
            if (query.Criteria != null)
            {
#if NET5_0_OR_GREATER
                return databaseManagement.Events.GetParameter<T>(query.Criteria.Where(x => x.ParameterDetails is not null).SelectMany(x => x.ParameterDetails));
#else
                return databaseManagement.Events.GetParameter<T>(query.Criteria.Where(x => x.ParameterDetails != null).SelectMany(x => x.ParameterDetails));
#endif
            }

            return Enumerable.Empty<IDataParameter>();
        }
    }
}