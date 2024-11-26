using System.Collections.Generic;

namespace GSqlQuery.Runner.Test.Extensions
{
    internal static class TestExtension
    {
        public static string ParameterReplace(this IEnumerable<ParameterDetail> parameterDetails, string query, string newName = "@Param")
        {
            foreach (var param in parameterDetails)
            {
                query = query?.Replace(param.Name, newName);
            }

            return query;
        }

        public static string ParameterReplace(this CriteriaDetailCollection criteriaDetail, string newName = "@Param")
        {
            string result = string.Empty;
            result += criteriaDetail.Values?.ParameterReplace(criteriaDetail.QueryPart, newName);
            return result;
        }

        public static string ParameterReplaceInQuery(this CriteriaDetailCollection criteriaDetail, string query)
        {
            return criteriaDetail.Values?.ParameterReplace(query, "@Param");
        }
    }
}