using System.Collections.Generic;

namespace GSqlQuery.Runner.Test.Extensions
{
    internal static class TestExtension
    {
        public static string ParameterReplace(this IEnumerable<ParameterDetail> parameterDetails, string query, string newName = "@Param")
        {
            foreach (var param in parameterDetails)
            {
                query = query.Replace(param.Name, newName);
            }

            return query;
        }

        public static string ParameterReplace(this CriteriaDetail criteriaDetail, string newName = "@Param")
        {
            return criteriaDetail.ParameterDetails.ParameterReplace(criteriaDetail.QueryPart, newName);

        }
    }
}