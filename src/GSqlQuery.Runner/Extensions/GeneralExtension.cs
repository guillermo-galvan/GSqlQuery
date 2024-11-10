using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GSqlQuery.Runner
{
    public static class GeneralExtension
    {
        public static IEnumerable<IDataParameter> GetParameters<T, TDbConnection>(IQuery query,
            IDatabaseManagement<TDbConnection> databaseManagement) where T : class
        {
            if (query.Criteria != null)
            {
                IEnumerable<ParameterDetail> parameterDetails = query.Criteria.Where(x => x.ParameterDetails != null).SelectMany(x => x.ParameterDetails);
                return databaseManagement.Events.GetParameter<T>(parameterDetails);
            }

            return Enumerable.Empty<IDataParameter>();
        }
    }
}