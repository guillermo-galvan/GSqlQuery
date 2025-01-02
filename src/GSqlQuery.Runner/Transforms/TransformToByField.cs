using System.Collections.Generic;
using System.Data.Common;

namespace GSqlQuery.Runner.Transforms
{
    internal class TransformToByField<T, TDbDataReader>(int numColumns) : TransformTo<T, TDbDataReader>(numColumns)
        where T : class
        where TDbDataReader : DbDataReader
    {
        public override T CreateEntity(IEnumerable<PropertyValue> propertyValues)
        {
            object result = _classOptions.ConstructorInfo.Invoke(null);

            foreach (PropertyValue item in propertyValues)
            {
                if (item.Value != null)
                {
                    item.Property.PropertyInfo.SetValue(result, item.Value);
                }
            }

            return (T)result;
        }
    }
}