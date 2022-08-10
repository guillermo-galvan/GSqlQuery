using FluentSQL.Models;
using System.Collections.Concurrent;

namespace FluentSQL.Helpers
{
    internal class ClassOptionsFactory
    {
        private static readonly ConcurrentDictionary<Type, ClassOptions> _entities = new();

        internal static ClassOptions GetClassOptions(Type type)
        {
            return _entities.GetOrAdd(type, (model) => {
                return new ClassOptions(model);
            });
        }
    }
}
