using GSqlQuery.Cache;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Runner
{
    public interface ITransformTo<T, TDbDataReader>
        where T : class
        where TDbDataReader : DbDataReader
    {
        T CreateEntity(IEnumerable<PropertyValue> propertyValues);

        IEnumerable<T> Transform(PropertyOptionsCollection propertyOptions, IQuery<T> query, TDbDataReader reader, DatabaseManagementEvents events);

        Task<IEnumerable<T>> TransformAsync(PropertyOptionsCollection propertyOptions, IQuery<T> query, TDbDataReader reader, DatabaseManagementEvents events, CancellationToken cancellationToken = default);
    }
}