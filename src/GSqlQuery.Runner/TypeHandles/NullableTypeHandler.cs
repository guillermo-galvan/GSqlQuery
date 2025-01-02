using System;
using System.Data.Common;

namespace GSqlQuery.Runner.TypeHandles
{
    public abstract class NullableTypeHandler<T, TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            object value = reader.GetValue(dataReaderPropertyDetail.Ordinal.Value);
            return value is DBNull ? default : GetNotNullValue(value);
        }

        public abstract T GetNotNullValue(object value);
    }
}
