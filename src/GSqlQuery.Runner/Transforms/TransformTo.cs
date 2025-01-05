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

        private class PropertyOptionsConfig(PropertyOptions propertyOptions)
        {
            public PropertyOptions PropertyOptions { get; } = propertyOptions;

            public int Ordinal { get; set; } = -1;

            public ITypeHandler<TDbDataReader> TypeHandler { get; set; } = null;
        }

        public virtual IEnumerable<T> Transform(PropertyOptionsCollection propertyOptions, IQuery<T> query, TDbDataReader reader, DatabaseManagementEvents events)
        {
            List<T> result = [];
            Dictionary<string, PropertyOptionsConfig> queryColumns = query.Columns.ToDictionary(c => c.Value.ColumnAttribute.Name, c => new PropertyOptionsConfig(c.Value));

            while (reader.Read())
            {
                foreach (KeyValuePair<string, PropertyOptions> propertyOption in propertyOptions)
                {
                    // Intentar obtener la columna correspondiente en la consulta
                    if (queryColumns.TryGetValue(propertyOption.Value.ColumnAttribute.Name, out PropertyOptionsConfig propertyOptionConfig))
                    {
                        if (propertyOptionConfig.Ordinal == -1)
                        {
                            // Obtener el índice ordinal de la columna en el DbDataReader
                            propertyOptionConfig.Ordinal = reader.GetOrdinal(propertyOption.Value.ColumnAttribute.Name);
                        }

                        propertyOptionConfig.TypeHandler ??= events.GetHandler<TDbDataReader>(propertyOptionConfig.PropertyOptions.PropertyInfo.PropertyType);

                        SetValue(propertyOptionConfig.PropertyOptions, propertyOptionConfig.TypeHandler.GetValue(reader, propertyOptionConfig.Ordinal));
                    }
                }

                result.Add(GetEntity());
            }

            return result;
        }

        public async virtual Task<IEnumerable<T>> TransformAsync(PropertyOptionsCollection propertyOptions, IQuery<T> query, TDbDataReader reader, DatabaseManagementEvents events, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            List<T> result = [];
            Dictionary<string, PropertyOptionsConfig> queryColumns = query.Columns.ToDictionary(c => c.Value.ColumnAttribute.Name, c => new PropertyOptionsConfig(c.Value));

            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                foreach (KeyValuePair<string, PropertyOptions> propertyOption in propertyOptions)
                {
                    // Intentar obtener la columna correspondiente en la consulta
                    if (queryColumns.TryGetValue(propertyOption.Value.ColumnAttribute.Name, out PropertyOptionsConfig propertyOptionConfig))
                    {
                        if (propertyOptionConfig.Ordinal == -1)
                        {
                            // Obtener el índice ordinal de la columna en el DbDataReader
                            propertyOptionConfig.Ordinal = reader.GetOrdinal(propertyOption.Value.ColumnAttribute.Name);
                        }

                        propertyOptionConfig.TypeHandler ??= events.GetHandler<TDbDataReader>(propertyOptionConfig.PropertyOptions.PropertyInfo.PropertyType);

                        SetValue(propertyOptionConfig.PropertyOptions, await propertyOptionConfig.TypeHandler.GetValueAsync(reader, propertyOptionConfig.Ordinal, cancellationToken).ConfigureAwait(false));
                    }
                }

                result.Add(GetEntity());
            }

            return result;
        }

        public abstract void SetValue(PropertyOptions property, object value);

        public abstract T GetEntity();
    }
}