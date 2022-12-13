using GSqlQuery.Extensions;
using GSqlQuery.Helpers;

namespace GSqlQuery.SearchCriteria
{
    public static class GroupExtension
    {
        /// <summary>
        /// Start the group
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <returns>Instance of IWhere</returns>
        public static IWhere<T, TReturn> BeginGroup<T, TReturn>(this IWhere<T, TReturn> where)
            where T : class, new() where TReturn : IQuery
        {
            IAndOr<T, TReturn> andor = where.GetAndOr();
            var result = new Group<T, TReturn>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, null, andor);
            andor.Add(result);
            return result;
        }

        /// <summary>
        /// Close the group
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <returns>Instance of IAndOr</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IAndOr<T, TReturn> CloseGroup<T, TReturn>(this IAndOr<T, TReturn> andOr)
            where T : class, new() where TReturn : IQuery
        {
            if (andOr is Group<T, TReturn> group)
            {
                return group.AndOr;
            }

            throw new InvalidOperationException("Need to start the group to be able to close it");
        }

        /// <summary>
        /// Start the group with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <returns>Instance of IWhere</returns>
        public static IWhere<T, TReturn> AndBeginGroup<T, TReturn>(this IAndOr<T, TReturn> andOr)
            where T : class, new() where TReturn : IQuery
        {
            var result = new Group<T, TReturn>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, "AND", andOr);
            andOr.Add(result);
            return result;
        }

        /// <summary>
        /// Start the group with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <returns>Instance of IWhere</returns>
        public static IWhere<T, TReturn> OrBeginGroup<T, TReturn>(this IAndOr<T, TReturn> andOr)
            where T : class, new() where TReturn : IQuery
        {
            var result = new Group<T, TReturn>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, "OR", andOr);
            andOr.Add(result);
            return result;
        }
    }
}
