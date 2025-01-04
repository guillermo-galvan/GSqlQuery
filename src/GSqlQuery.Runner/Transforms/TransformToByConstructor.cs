using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace GSqlQuery.Runner.Transforms
{
    internal class TransformToByConstructor<T, TDbDataReader> : TransformTo<T, TDbDataReader>
        where T : class
        where TDbDataReader : DbDataReader
    {
        private readonly ConstructorInfo _constructorInfo;
        private readonly object[] _fields;

        public TransformToByConstructor(int numColumns) : base(numColumns)
        {
            _constructorInfo = _classOptions.ConstructorInfo;
            _fields = new object[numColumns];
        }

        public override T CreateEntity(IEnumerable<PropertyValue> propertyValues)
        {
            foreach (PropertyValue item in propertyValues)
            {
                _fields[item.Property.PositionConstructor] = item.Value;
            }

            return (T)_constructorInfo.Invoke(_fields);
        }
    }
}