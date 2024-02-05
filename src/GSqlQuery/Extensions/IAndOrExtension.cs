using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Extensions
{
    /// <summary>
    /// IAndOr Extension
    /// </summary>
    internal static class IAndOrExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="andOr"></param>
        /// <param name="formats"></param>
        /// <param name="criterias"></param>
        /// <returns></returns>
        internal static string GetCliteria<TReturn>(IAndOr<TReturn> andOr, IFormats formats, ref IEnumerable<CriteriaDetail> criterias) where TReturn : IQuery
        {
            if (andOr != null)
            {
                criterias ??= andOr.BuildCriteria();
                var queryParts = criterias.Select(x => x.QueryPart);
                return string.Join(" ", queryParts);
            }

            return string.Empty;
        }
    }
}