using FluentSQL.Extensions;
using FluentSQL.Helpers;

namespace FluentSQL.SearchCriteria
{
    public static class GroupExtension
    {
        /// <summary>
        /// Start the group
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <returns>Instance of IWhere</returns>
        public static IWhere<T> BeginGroup<T>(this IWhere<T> where) where T : class, new()
        {
            IAndOr<T> andor = where.GetAndOr();
            var result = new Group<T>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, null, andor);
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
        public static IAndOr<T> CloseGroup<T>(this IAndOr<T> andOr) where T : class, new()
        {
            if (andOr is Group<T> group)
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
        public static IWhere<T> AndBeginGroup<T>(this IAndOr<T> andOr) where T : class, new()
        {
            var result = new Group<T>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, "AND", andOr);
            andOr.Add(result);
            return result;
        }

        /// <summary>
        /// Start the group with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <returns>Instance of IWhere</returns>
        public static IWhere<T> OrBeginGroup<T>(this IAndOr<T> andOr) where T : class, new()
        {
            var result = new Group<T>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, "OR", andOr);
            andOr.Add(result);
            return result;
        }
    }
}
