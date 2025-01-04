using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace GSqlQuery.Runner.Transforms
{
    internal class TransformToByField<T, TDbDataReader> : TransformTo<T, TDbDataReader>
        where T : class
        where TDbDataReader : DbDataReader
    {
        private readonly ConstructorInfo _constructorInfo;

        public TransformToByField(int numColumns) : base(numColumns)
        {
            _constructorInfo = _classOptions.ConstructorInfo;
        }

        public override T CreateEntity(IEnumerable<PropertyValue> propertyValues)
        {
            object result = _constructorInfo.Invoke(null);

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