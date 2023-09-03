using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace GSqlQuery.Runner.Transforms
{
    internal static class TransformTo 
    {
        public static object SwitchTypeValue(Type type, object value)
        {
            try
            {
                if (value == DBNull.Value || value == null)
                {
                    return null;
                }
                else if (typeof(char[]) == type && value is string tmp)
                {
                    return tmp.ToCharArray();
                }
                else
                {
                    return Convert.ChangeType(value, type);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    internal abstract class TransformTo<T> : ITransformTo<T> where T : class, new()
    {
        protected readonly int _numColumns;
        protected readonly ClassOptions _classOptions;

        public TransformTo(int numColumns)
        {
            _numColumns = numColumns;
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(T));
        }

        protected IEnumerable<PropertyOptionsInEntity> GetProperties(IEnumerable<PropertyOptions> propertyOptions,
            IEnumerable<PropertyOptions> propertyOptionsColumns, DbDataReader reader)
        {
            return (from pro in propertyOptions
                    join ca in propertyOptionsColumns on pro.ColumnAttribute.Name equals ca.ColumnAttribute.Name into leftJoin
                    from left in leftJoin.DefaultIfEmpty()
                    select
                        new PropertyOptionsInEntity(pro,
                        Nullable.GetUnderlyingType(pro.PropertyInfo.PropertyType) ?? pro.PropertyInfo.PropertyType,
                        pro.PropertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(pro.PropertyInfo.PropertyType) : null,
                        left != null ? (int?)reader.GetOrdinal(pro.ColumnAttribute.Name) : null))
                        .ToArray();
        }

        public virtual IEnumerable<PropertyOptionsInEntity> GetOrdinalPropertiesInEntity(IEnumerable<PropertyOptions> propertyOptions, IQuery<T> query, DbDataReader reader)
        {
            return GetProperties(propertyOptions, query.Columns, reader);
        }

        public abstract T Generate(IEnumerable<PropertyOptionsInEntity> columns, DbDataReader reader);

    }
}