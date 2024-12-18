using GSqlQuery.Cache;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Runner.Transforms
{
    public static class TransformTo
    {
        internal static Type charType = typeof(char[]);

        public static object SwitchTypeValue(Type type, object value)
        {
            if (value == DBNull.Value || value == null)
            {
                return null;
            }
            else if (charType == type && value is string tmp)
            {
                return tmp.ToCharArray();
            }
            else
            {
                return Convert.ChangeType(value, type);
            }
        }
    }

    public abstract class TransformTo<T, TDbDataReader>(int numColumns) : ITransformTo<T, TDbDataReader>
        where T : class
        where TDbDataReader : DbDataReader
    {
        protected readonly int _numColumns = numColumns;
        protected readonly ClassOptions _classOptions = ClassOptionsFactory.GetClassOptions(typeof(T));

        protected virtual IEnumerable<PropertyOptionsInEntity> GetOrdinalPropertiesInEntity(PropertyOptionsCollection propertyOptions, IQuery<T> query, TDbDataReader reader)
        {
            return (from pro in propertyOptions
                    join ca in query.Columns on pro.Value.ColumnAttribute.Name equals ca.Value.ColumnAttribute.Name into leftJoin
                    from left in leftJoin.DefaultIfEmpty()
                    select
                        new PropertyOptionsInEntity(pro.Value, pro.Value.Type, pro.Value.ValueDefault, left.Value != null ? reader.GetOrdinal(pro.Value.ColumnAttribute.Name) : null)).ToArray();
        }

        private void Fill(TDbDataReader reader, IEnumerable<PropertyOptionsInEntity> columns, List<PropertyValue> propertyValues, List<T> result)
        {
            foreach (PropertyOptionsInEntity item in columns)
            {
                if (item.Ordinal.HasValue)
                {
                    propertyValues.Add(new PropertyValue(item.Property, GetValue(item.Ordinal.Value, reader, item.Type)));
                }
                else
                {
                    propertyValues.Add(new PropertyValue(item.Property, item.DefaultValue));
                }
            }

            T tmp = CreateEntity(propertyValues);
            propertyValues.Clear();
            result.Add(tmp);
        }

        public virtual object GetValue(int ordinal, TDbDataReader reader, Type propertyType)
        {
            return TransformTo.SwitchTypeValue(propertyType, reader.GetValue(ordinal));
        }

        public virtual IEnumerable<T> Transform(PropertyOptionsCollection propertyOptions, IQuery<T> query, TDbDataReader reader)
        {
            IEnumerable<PropertyOptionsInEntity> columns = GetOrdinalPropertiesInEntity(propertyOptions, query, reader);
            List<T> result = [];
            List<PropertyValue> propertyValues = [];

            while (reader.Read())
            {
                Fill(reader, columns, propertyValues, result);
            }

            return result;
        }

        public async virtual Task<IEnumerable<T>> TransformAsync(PropertyOptionsCollection propertyOptions, IQuery<T> query, TDbDataReader reader, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            IEnumerable<PropertyOptionsInEntity> columns = GetOrdinalPropertiesInEntity(propertyOptions, query, reader);
            List<T> result = [];
            List<PropertyValue> propertyValues = [];

            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                Fill(reader, columns, propertyValues, result);
            }

            return result;
        }

        public abstract T CreateEntity(IEnumerable<PropertyValue> propertyValues);
    }
}