using System.Data;

namespace FluentSQL.DatabaseManagement.Extensions
{
    public static class GeneralExtension
    {
        public static IEnumerable<IDataParameter> GetParameters<T, TDbConnection>(this IQuery query,
            IDatabaseManagement<TDbConnection> databaseManagment) where T : class, new()
        {
            Queue<ParameterDetail> parameters = new();
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria.Where(x => x.ParameterDetails is not null))
                {
                    foreach (var item2 in item.ParameterDetails)
                    {
                        parameters.Enqueue(item2);
                    }
                }
            }

            return databaseManagment.Events.GetParameter<T>(parameters);
        }
    }
}
