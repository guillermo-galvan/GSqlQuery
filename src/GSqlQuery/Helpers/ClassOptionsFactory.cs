using GSqlQuery.Models;
using System.Collections.Concurrent;

namespace GSqlQuery.Helpers
{
    public class ClassOptionsFactory
    {
        private static readonly ConcurrentDictionary<Type, ClassOptions> _entities = new();

        public static ClassOptions GetClassOptions(Type type)
        {
            return _entities.GetOrAdd(type, (model) =>
            {
                return new ClassOptions(model);
            });
        }
    }
}
