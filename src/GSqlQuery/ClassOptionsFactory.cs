using System;
using System.Collections.Concurrent;

namespace GSqlQuery
{
    /// <summary>
    /// Class Options Factory
    /// </summary>
    public sealed class ClassOptionsFactory
    {
        private static readonly ConcurrentDictionary<Type, ClassOptions> _entities = new ConcurrentDictionary<Type, ClassOptions>();

        /// <summary>
        /// Get Class Options
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>Class Options</returns>
        public static ClassOptions GetClassOptions(Type type)
        {
            return _entities.GetOrAdd(type, (model) =>
            {
                return new ClassOptions(model);
            });
        }
    }
}