using System;
using System.Collections.Concurrent;
using System.Data.Common;
#nullable enable

namespace GSqlQuery.Runner.TypeHandles
{
    public sealed class TypeHandleCollection<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        private readonly ConcurrentDictionary<Type, ITypeHandler<TDbDataReader>> _typeHandles;

        public int Count => _typeHandles.Count;

        public static TypeHandleCollection<TDbDataReader> Instance { get; } = new TypeHandleCollection<TDbDataReader>();

        private TypeHandleCollection()
        {
            _typeHandles = new ConcurrentDictionary<Type, ITypeHandler<TDbDataReader>>();

            _typeHandles.TryAdd(typeof(byte), new ByteTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(byte?), new ByteNullableTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(short), new ShortTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(short?), new ShortNullableTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(int), new IntTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(int?), new IntNullableTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(long), new LongTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(long?), new LongNullableTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(float), new FloatTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(float?), new FloatNullableTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(double), new DoubleTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(double?), new DoubleNullableTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(decimal), new DecimalTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(decimal?), new DecimalNullableTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(char), new CharTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(char?), new CharNullableTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(bool), new BoolTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(bool?), new BoolNullableTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(string), new StringNullableTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(DateTime), new DateTimeTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(DateTime?), new DateTimeNullableTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(char[]), new CharArrayNullableTypeHandler<TDbDataReader>());
            _typeHandles.TryAdd(typeof(object), new ObjectNullableTypeHandler<TDbDataReader>());
        }

        public bool TryAdd(Type type, ITypeHandler<TDbDataReader> typeHandler)
        {
            return _typeHandles.TryAdd(type, typeHandler);
        }

        public void Add(Type type, ITypeHandler<TDbDataReader> typeHandler)
        {
            _typeHandles[type] = typeHandler;
        }

        public bool Contains(Type type)
        {
            return _typeHandles.ContainsKey(type);
        }

        public ITypeHandler<TDbDataReader>? GetTypeHandler(Type type)
        {
            return _typeHandles.TryGetValue(type, out var typeHandler) ? typeHandler : null;
        }

        public ITypeHandler<TDbDataReader>? Remove(Type type)
        {
            _typeHandles.TryRemove(type, out ITypeHandler<TDbDataReader>? result);
            return result;
        }
    }
}
