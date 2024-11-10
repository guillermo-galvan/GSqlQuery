using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Runner
{

    public interface ITransformTo<TDbDataReader>
        where TDbDataReader : DbDataReader
    {
        object GetValue(int ordinal, TDbDataReader reader, Type propertyType);
    }

    public interface ITransformTo<T, TDbDataReader> : ITransformTo<TDbDataReader>
        where T : class
        where TDbDataReader : DbDataReader
    {
        T CreateEntity(IEnumerable<PropertyValue> propertyValues);

        IEnumerable<T> Transform(IEnumerable<PropertyOptions> propertyOptions, IQuery<T> query, TDbDataReader reader);

        Task<IEnumerable<T>> TransformAsync(IEnumerable<PropertyOptions> propertyOptions, IQuery<T> query, TDbDataReader reader, CancellationToken cancellationToken = default);
    }
}