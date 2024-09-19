using System;

namespace GSqlQuery.Queries
{
    internal class DynamicQuery(Type entity, Type properties)
    {
        public Type Entity { get; } = entity;

        public Type Properties { get; } = properties;
    }
}