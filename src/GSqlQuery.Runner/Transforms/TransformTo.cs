using GSqlQuery.Cache;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Runner
{
    public abstract class TransformTo<T, TDbDataReader>(int numColumns) : ITransformTo<T, TDbDataReader>
        where T : class
        where TDbDataReader : DbDataReader
    {
        protected readonly int _numColumns = numColumns;
        protected readonly ClassOptions _classOptions = ClassOptionsFactory.GetClassOptions(typeof(T));

        protected virtual IEnumerable<DataReaderPropertyDetail> GetOrdinalPropertiesInEntity(PropertyOptionsCollection propertyOptions, IQuery<T> query, TDbDataReader reader)
        {
            return (from pro in propertyOptions
                    join ca in query.Columns on pro.Value.ColumnAttribute.Name equals ca.Value.ColumnAttribute.Name into leftJoin
                    from left in leftJoin.DefaultIfEmpty()
                    select
                        new DataReaderPropertyDetail(pro.Value, left.Value != null ? reader.GetOrdinal(pro.Value.ColumnAttribute.Name) : null)).ToArray();
        }

        private void Fill(TDbDataReader reader, IEnumerable<DataReaderPropertyDetail> columns, DatabaseManagementEvents events, Dictionary<int, ITypeHandler<TDbDataReader>> typeHandlers, List<T> result)
        {
            List<PropertyValue> propertyValues = [];
            foreach (DataReaderPropertyDetail item in columns)
            {
                if (item.Ordinal.HasValue)
                {
                    if (!typeHandlers.TryGetValue(item.Ordinal.Value, out ITypeHandler<TDbDataReader> typeHandlersTmp))
                    {
                        typeHandlersTmp = events.GetHandler<TDbDataReader>(item.Property.PropertyInfo.PropertyType);
                        typeHandlers.Add(item.Ordinal.Value, typeHandlersTmp);
                    }

                    propertyValues.Add(new PropertyValue(item.Property, typeHandlersTmp.GetValue(reader, item)));
                }
                else
                {
                    propertyValues.Add(new PropertyValue(item.Property, item.Property.DefaultValue));
                }
            }

            T tmp = CreateEntity(propertyValues);
            result.Add(tmp);
        }

        public virtual IEnumerable<T> Transform(PropertyOptionsCollection propertyOptions, IQuery<T> query, TDbDataReader reader, DatabaseManagementEvents events)
        {
            IEnumerable<DataReaderPropertyDetail> columns = GetOrdinalPropertiesInEntity(propertyOptions, query, reader);
            List<T> result = [];
            Dictionary<int, ITypeHandler<TDbDataReader>> typeHandlers = [];

            while (reader.Read())
            {
                Fill(reader, columns, events, typeHandlers, result);
            }

            return result;
        }

        public async virtual Task<IEnumerable<T>> TransformAsync(PropertyOptionsCollection propertyOptions, IQuery<T> query, TDbDataReader reader, DatabaseManagementEvents events, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            IEnumerable<DataReaderPropertyDetail> columns = GetOrdinalPropertiesInEntity(propertyOptions, query, reader);
            List<T> result = [];
            Dictionary<int, ITypeHandler<TDbDataReader>> typeHandlers = [];

            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                Fill(reader, columns, events, typeHandlers, result);
            }

            return result;
        }

        public abstract T CreateEntity(IEnumerable<PropertyValue> propertyValues);
    }
}