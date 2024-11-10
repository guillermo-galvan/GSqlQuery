using System.Collections.Generic;
using System.Data.Common;

namespace GSqlQuery.Runner.Transforms
{
    internal class TransformToByConstructor<T, TDbDataReader>(int numColumns) : TransformTo<T, TDbDataReader>(numColumns)
        where T : class
        where TDbDataReader : DbDataReader
    {
        public override T CreateEntity(IEnumerable<PropertyValue> propertyValues)
        {
            object[] fields = new object[_numColumns];

            foreach (PropertyValue item in propertyValues)
            {
                fields[item.Property.PositionConstructor] = item.Value;
            }

            return (T)_classOptions.ConstructorInfo.Invoke(fields);
        }
    }
}