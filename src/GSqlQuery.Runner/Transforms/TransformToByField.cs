using System.Data.Common;
using System.Reflection;

namespace GSqlQuery.Runner.Transforms
{
    internal class TransformToByField<T, TDbDataReader> : TransformTo<T, TDbDataReader>
        where T : class
        where TDbDataReader : DbDataReader
    {
        private readonly ConstructorInfo _constructorInfo;
        private object _entity;

        public TransformToByField(int numColumns) : base(numColumns)
        {
            _constructorInfo = _classOptions.ConstructorInfo;
            _entity = _constructorInfo.Invoke(null);
        }

        public override T GetEntity()
        {
            T result = (T)_entity;
            _entity = _constructorInfo.Invoke(null);
            return result;
        }

        public override void SetValue(PropertyOptions property, object value)
        {
            property.PropertyInfo.SetValue(_entity, value);
        }
    }
}