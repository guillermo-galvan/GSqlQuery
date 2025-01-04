using System;
using System.Data;
using System.Data.Common;
#nullable enable

namespace GSqlQuery.Runner.TypeHandles
{
    internal class ByteTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.GetByte(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Byte;
        }
    }

    internal class ByteNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.IsDBNull(dataReaderPropertyDetail.Ordinal!.Value) ? null : reader.GetByte(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Byte;
        }
    }

    internal class ShortTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.GetInt16(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Int16;
        }
    }

    internal class ShortNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.IsDBNull(dataReaderPropertyDetail.Ordinal!.Value) ? null : reader.GetInt16(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Int16;
        }
    }

    internal class IntTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.GetInt32(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Int32;
        }
    }

    internal class IntNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.IsDBNull(dataReaderPropertyDetail.Ordinal!.Value) ? null : reader.GetInt32(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Int32;
        }
    }

    internal class LongTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.GetInt64(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Int64;
        }
    }

    internal class LongNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.IsDBNull(dataReaderPropertyDetail.Ordinal!.Value) ? null : reader.GetInt64(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Int64;
        }
    }

    internal class FloatTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.GetFloat(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Single;
        }
    }

    internal class FloatNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.IsDBNull(dataReaderPropertyDetail.Ordinal!.Value) ? null : reader.GetFloat(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Single;
        }
    }

    internal class DoubleTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.GetDouble(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Double;
        }
    }

    internal class DoubleNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.IsDBNull(dataReaderPropertyDetail.Ordinal!.Value) ? null : reader.GetDouble(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Double;
        }
    }

    internal class DecimalTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.GetDecimal(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Decimal;
        }
    }

    internal class DecimalNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.IsDBNull(dataReaderPropertyDetail.Ordinal!.Value) ? null : reader.GetDecimal(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Decimal;
        }
    }

    internal class CharTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.GetChar(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.StringFixedLength;
        }
    }

    internal class CharNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.IsDBNull(dataReaderPropertyDetail.Ordinal!.Value) ? null : reader.GetChar(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.StringFixedLength;
        }
    }

    internal class BoolTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.GetBoolean(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Boolean;
        }
    }

    internal class BoolNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.IsDBNull(dataReaderPropertyDetail.Ordinal!.Value) ? null : reader.GetBoolean(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Boolean;
        }
    }

    internal class StringNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.IsDBNull(dataReaderPropertyDetail.Ordinal!.Value) ? null : reader.GetString(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.String;
        }
    }

    internal class DateTimeTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.GetDateTime(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.DateTime;
        }
    }

    internal class DateTimeNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.IsDBNull(dataReaderPropertyDetail.Ordinal!.Value) ? null : reader.GetDateTime(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.DateTime;
        }
    }

    internal class CharArrayNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.IsDBNull(dataReaderPropertyDetail.Ordinal!.Value) ? null : reader.GetString(dataReaderPropertyDetail.Ordinal!.Value).ToCharArray();
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.String;
        }
    }

    internal class ObjectNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.IsDBNull(dataReaderPropertyDetail.Ordinal!.Value) ? null : reader.GetValue(dataReaderPropertyDetail.Ordinal!.Value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Object;
        }
    }

    internal class DefaultNullableTypeHandler<TDbDataReader>(Type type) : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        private readonly Type _type = type;

        public override object? GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail)
        {
            return reader.IsDBNull(dataReaderPropertyDetail.Ordinal!.Value) ? null : GeneralExtension.ConvertToValue(_type, reader.GetValue(dataReaderPropertyDetail.Ordinal!.Value));
        }

        public object GetNotNullValue(object value)
        {
            return GeneralExtension.ConvertToValue(_type, value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Object;
        }
    }
}