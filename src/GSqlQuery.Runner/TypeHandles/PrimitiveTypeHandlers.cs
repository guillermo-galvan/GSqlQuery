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

    internal class ByteNullableTypeHandler<TDbDataReader> : NullableTypeHandler<byte, TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override byte GetNotNullValue(object value)
        {
            return (byte)value;
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

    internal class ShortNullableTypeHandler<TDbDataReader> : NullableTypeHandler<short, TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override short GetNotNullValue(object value)
        {
            return (short)value;
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

    internal class IntNullableTypeHandler<TDbDataReader> : NullableTypeHandler<int, TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override int GetNotNullValue(object value)
        {
            return (int)value;
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

    internal class LongNullableTypeHandler<TDbDataReader> : NullableTypeHandler<long, TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override long GetNotNullValue(object value)
        {
            return (long)value;
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

    internal class FloatNullableTypeHandler<TDbDataReader> : NullableTypeHandler<float, TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override float GetNotNullValue(object value)
        {
            return (float)value;
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

    internal class DoubleNullableTypeHandler<TDbDataReader> : NullableTypeHandler<double, TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override double GetNotNullValue(object value)
        {
            return (double)value;
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

    internal class CharNullableTypeHandler<TDbDataReader> : NullableTypeHandler<char, TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override char GetNotNullValue(object value)
        {
            return (char)value;
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

    internal class StringNullableTypeHandler<TDbDataReader> : NullableTypeHandler<string, TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override string GetNotNullValue(object value)
        {
            return (string)value;
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

    internal class CharArrayNullableTypeHandler<TDbDataReader> : NullableTypeHandler<char[], TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override char[] GetNotNullValue(object value)
        {
            string tmp = (string)value;
            return tmp.ToCharArray();
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.String;
        }
    }

    internal class ObjectNullableTypeHandler<TDbDataReader> : NullableTypeHandler<object, TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetNotNullValue(object value)
        {
            return value;
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Object;
        }
    }

    internal class DefaultNullableTypeHandler<TDbDataReader>(Type type) : NullableTypeHandler<object, TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        private readonly Type _type = type;

        public override object GetNotNullValue(object value)
        {
            return GeneralExtension.ConvertToValue(_type, value);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Object;
        }
    }
}