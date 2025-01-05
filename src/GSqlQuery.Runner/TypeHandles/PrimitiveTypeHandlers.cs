using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
#nullable enable

namespace GSqlQuery.Runner.TypeHandles
{
    internal class ByteTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.GetByte(ordinal);
        }

        public override Task<object> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return Task.FromResult((object)reader.GetByte(ordinal));
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Byte;
        }
    }

    internal class ByteNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : reader.GetByte(ordinal);
        }

        public override async Task<object?> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return await reader.IsDBNullAsync(ordinal, cancellationToken).ConfigureAwait(false) ? null : reader.GetByte(ordinal);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Byte;
        }
    }

    internal class ShortTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.GetInt16(ordinal);
        }

        public override Task<object> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return Task.FromResult((object)reader.GetInt16(ordinal)); 
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Int16;
        }
    }

    internal class ShortNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : reader.GetInt16(ordinal);
        }

        public override async Task<object?> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return await reader.IsDBNullAsync(ordinal, cancellationToken).ConfigureAwait(false) ? null : reader.GetInt16(ordinal);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Int16;
        }
    }

    internal class IntTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.GetInt32(ordinal);
        }

        public override Task<object> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return Task.FromResult((object)reader.GetInt32(ordinal));
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Int32;
        }
    }

    internal class IntNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : reader.GetInt32(ordinal);
        }

        public override async Task<object?> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return await reader.IsDBNullAsync(ordinal,cancellationToken).ConfigureAwait(false) ? null : reader.GetInt32(ordinal);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Int32;
        }
    }

    internal class LongTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.GetInt64(ordinal);
        }

        public override Task<object> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return Task.FromResult((object)reader.GetInt64(ordinal));
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Int64;
        }
    }

    internal class LongNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : reader.GetInt64(ordinal);
        }

        public override async Task<object?> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return await reader.IsDBNullAsync(ordinal, cancellationToken).ConfigureAwait(false) ? null : reader.GetInt64(ordinal);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Int64;
        }
    }

    internal class FloatTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.GetFloat(ordinal);
        }

        public override Task<object> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return Task.FromResult((object)reader.GetFloat(ordinal));
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Single;
        }
    }

    internal class FloatNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : reader.GetFloat(ordinal);
        }

        public override async Task<object?> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return await reader.IsDBNullAsync(ordinal, cancellationToken).ConfigureAwait(false) ? null : reader.GetFloat(ordinal);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Single;
        }
    }

    internal class DoubleTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.GetDouble(ordinal);
        }

        public override Task<object> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return Task.FromResult((object)reader.GetDouble(ordinal));
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Double;
        }
    }

    internal class DoubleNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : reader.GetDouble(ordinal);
        }

        public override async Task<object?> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return await reader.IsDBNullAsync(ordinal, cancellationToken).ConfigureAwait(false) ? null : reader.GetDouble(ordinal);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Double;
        }
    }

    internal class DecimalTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.GetDecimal(ordinal);
        }

        public override Task<object> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return Task.FromResult((object)reader.GetDecimal(ordinal));
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Decimal;
        }
    }

    internal class DecimalNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : reader.GetDecimal(ordinal);
        }

        public override async Task<object?> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return await reader.IsDBNullAsync(ordinal, cancellationToken).ConfigureAwait(false) ? null : reader.GetDecimal(ordinal);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Decimal;
        }
    }

    internal class CharTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.GetChar(ordinal);
        }

        public override Task<object> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return Task.FromResult((object)reader.GetChar(ordinal));
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.StringFixedLength;
        }
    }

    internal class CharNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : reader.GetChar(ordinal);
        }

        public override async Task<object?> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return await reader.IsDBNullAsync(ordinal, cancellationToken).ConfigureAwait(false) ? null : reader.GetChar(ordinal);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.StringFixedLength;
        }
    }

    internal class BoolTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.GetBoolean(ordinal);
        }

        public override Task<object> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return Task.FromResult((object)reader.GetBoolean(ordinal));
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Boolean;
        }
    }

    internal class BoolNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : reader.GetBoolean(ordinal);
        }

        public override async Task<object?> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return await reader.IsDBNullAsync(ordinal, cancellationToken).ConfigureAwait(false) ? null : reader.GetBoolean(ordinal);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Boolean;
        }
    }

    internal class StringNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }

        public override async Task<object?> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return await reader.IsDBNullAsync(ordinal, cancellationToken).ConfigureAwait(false) ? null : reader.GetString(ordinal);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.String;
        }
    }

    internal class DateTimeTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.GetDateTime(ordinal);
        }

        public override Task<object> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return Task.FromResult((object)reader.GetDateTime(ordinal));
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.DateTime;
        }
    }

    internal class DateTimeNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : reader.GetDateTime(ordinal);
        }

        public override async Task<object?> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return await reader.IsDBNullAsync(ordinal, cancellationToken).ConfigureAwait(false) ? null : reader.GetDateTime(ordinal);
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.DateTime;
        }
    }

    internal class CharArrayNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal).ToCharArray();
        }

        public override async Task<object?> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return await reader.IsDBNullAsync(ordinal, cancellationToken).ConfigureAwait(false) ? null : reader.GetString(ordinal).ToCharArray();
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.String;
        }
    }

    internal class ObjectNullableTypeHandler<TDbDataReader> : TypeHandler<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        public override object? GetValue(TDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? null : reader.GetValue(ordinal);
        }

        public override async Task<object?> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return await reader.IsDBNullAsync(ordinal, cancellationToken).ConfigureAwait(false) ? null : reader.GetValue(ordinal);
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

        public override object? GetValue(TDbDataReader reader, int ordinall)
        {
            return reader.IsDBNull(ordinall) ? null : GeneralExtension.ConvertToValue(_type, reader.GetValue(ordinall));
        }

        protected override void SetDataType(IDataParameter dataParameter)
        {
            dataParameter.DbType = DbType.Object;
        }

        public override async Task<object?> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken)
        {
            return await reader.IsDBNullAsync(ordinal, cancellationToken).ConfigureAwait(false) ? null : GeneralExtension.ConvertToValue(_type, reader.GetValue(ordinal));
        }
    }
}