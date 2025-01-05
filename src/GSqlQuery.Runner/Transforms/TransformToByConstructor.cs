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

        public override T GetEntity()
        {
            return (T)_constructorInfo.Invoke(_fields);
        }

        public override void SetValue(PropertyOptions property, object value)
        {
            _fields[property.PositionConstructor] = value;
        }
    }
}