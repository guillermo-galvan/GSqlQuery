using FluentSQL.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
