using System;

namespace GSqlQuery.Runner
{
    public class PropertyOptionsInEntity
    {
        public PropertyOptions Property { get; }

        public Type Type { get; }

        public object ValueDefault { get; }

        public int? Ordinal { get; }

        public PropertyOptionsInEntity(PropertyOptions propertyOptions, Type type, object valueDefault, int? ordinal)
        {
            Property = propertyOptions;
            Type = type;
            ValueDefault = valueDefault;
            Ordinal = ordinal;
        }
    }
}
