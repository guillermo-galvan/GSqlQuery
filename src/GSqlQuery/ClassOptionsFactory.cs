using System;
using System.Collections.Concurrent;

namespace GSqlQuery
{
    public sealed class ClassOptionsFactory
    {
        private static readonly ConcurrentDictionary<Type, ClassOptions> _entities = new ConcurrentDictionary<Type, ClassOptions>();

        public static ClassOptions GetClassOptions(Type type)
        {
            return _entities.GetOrAdd(type, (model) =>
            {
                return new ClassOptions(model);
            });
        }
    }
}