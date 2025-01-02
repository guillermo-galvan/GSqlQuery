using GSqlQuery.Cache;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Runner.Transforms
{
    internal class JoinClassOptions<TDbDataReader>
            where TDbDataReader : DbDataReader
    {
        private object _class;

        public PropertyOptions PropertyOptions { get; set; }

        public ClassOptions ClassOptions { get; set; }

        public IEnumerable<DataReaderPropertyDetail> PropertyOptionsInEntities { get; set; }

        public MethodInfo MethodInfo { get; set; }

        public object Class
        {
            get { return _class; }
            set
            {
                _class = value;
            }
        }

        public int Position { get; set; }
    }

    internal class JoinTransformTo<T, TDbDataReader> : TransformTo<T, TDbDataReader>
        where T : class
        where TDbDataReader : DbDataReader
    {
        private readonly JoinClassOptions<TDbDataReader>[] _joinClassOptions;
        private readonly ITransformTo<T, TDbDataReader> _transformTo;
        private readonly DatabaseManagementEvents _events;

        public JoinTransformTo(int numColumns, DatabaseManagementEvents events) : base(numColumns)
        {
            int position = 0;
            _events = events;
            _joinClassOptions = _classOptions.PropertyOptions.Where(x => x.Value.PropertyInfo.PropertyType.IsClass).Select(x => new JoinClassOptions<TDbDataReader>()
            {
                PropertyOptions = x.Value,
                ClassOptions = ClassOptionsFactory.GetClassOptions(x.Value.PropertyInfo.PropertyType),
                Position = position++,
            }).ToArray();

            if (!_classOptions.IsConstructorByParam)
            {
                _transformTo = new TransformToByField<T, TDbDataReader>(_classOptions.PropertyOptions.Count());
            }
            else
            {
                _transformTo = new TransformToByConstructor<T, TDbDataReader>(_classOptions.PropertyOptions.Count());
            }
        }

        protected static IEnumerable<DataReaderPropertyDetail> GetPropertiesJoin(ClassOptions classOptions, IEnumerable<PropertyOptions> propertyOptionsColumns, DbDataReader reader)
        {
            return (from pro in classOptions.PropertyOptions
                    join ca in propertyOptionsColumns on pro.Value.ColumnAttribute.Name equals ca.ColumnAttribute.Name into leftJoin
                    from left in leftJoin.DefaultIfEmpty()
                    select
                        new DataReaderPropertyDetail(pro.Value, left != null ? reader.GetOrdinal($"{classOptions.Type.Name}_{pro.Value.ColumnAttribute.Name}") : null))
                        .ToArray();
        }

        protected override IEnumerable<DataReaderPropertyDetail> GetOrdinalPropertiesInEntity(PropertyOptionsCollection propertyOptions, IQuery<T> query, TDbDataReader reader)
        {
            List<DataReaderPropertyDetail> result = [];

            IEnumerable<IGrouping<string, KeyValuePair<string, PropertyOptions>>> columnGroup = query.Columns.GroupBy(x => x.Value.Table.Name);

            foreach (JoinClassOptions<TDbDataReader> item in _joinClassOptions)
            {
                IGrouping<string, KeyValuePair<string, PropertyOptions>> tmpColumns = columnGroup.First(x => x.Key == item.ClassOptions.FormatTableName.Table.Name);
                item.PropertyOptionsInEntities = GetPropertiesJoin(item.ClassOptions, tmpColumns.Select(x => x.Value), reader);
                result.AddRange(item.PropertyOptionsInEntities);

                MethodInfo methodInfo = _events.GetType().GetMethod("GetTransformTo").MakeGenericMethod(item.ClassOptions.Type, reader.GetType());
                item.Class = methodInfo?.Invoke(_events, [item.ClassOptions]);
                item.MethodInfo = item.Class.GetType().GetMethod("CreateEntity");
            }

            return result;
        }

        private void Fill(TDbDataReader reader, IEnumerable<DataReaderPropertyDetail> columns, Dictionary<int, ITypeHandler<TDbDataReader>> typeHandlers, List<T> result)
        {
            List<PropertyValue> propertyValues = [];
            List<PropertyValue> jointPropertyValues = [];

            foreach (JoinClassOptions<TDbDataReader> joinClassOptions in _joinClassOptions)
            {
                foreach (DataReaderPropertyDetail item in joinClassOptions.PropertyOptionsInEntities)
                {
                    if (item.Ordinal.HasValue && !reader.IsDBNull(item.Ordinal.Value))
                    {
                        propertyValues.Add(new PropertyValue(item.Property, typeHandlers[item.Ordinal.Value].GetValue(reader, item)));
                    }
                    else
                    {
                        propertyValues.Add(new PropertyValue(item.Property, item.Property.DefaultValue));
                    }
                }
                object a = joinClassOptions.MethodInfo.Invoke(joinClassOptions.Class, [propertyValues]);
                propertyValues.Clear();
                jointPropertyValues.Add(new PropertyValue(joinClassOptions.PropertyOptions, a));
            }

            T tmp = CreateEntity(jointPropertyValues);
            jointPropertyValues.Clear();
            result.Add(tmp);
        }

        public override IEnumerable<T> Transform(PropertyOptionsCollection propertyOptions, IQuery<T> query, TDbDataReader reader, DatabaseManagementEvents events)
        {
            IEnumerable<DataReaderPropertyDetail> columns = GetOrdinalPropertiesInEntity(propertyOptions, query, reader);
            List<T> result = [];
            Dictionary<int, ITypeHandler<TDbDataReader>> typeHandlers = GetTypeHandlers(columns, events);

            while (reader.Read())
            {
                Fill(reader, columns, typeHandlers, result);
            }

            return result;
        }

        public override async Task<IEnumerable<T>> TransformAsync(PropertyOptionsCollection propertyOptions, IQuery<T> query, TDbDataReader reader, DatabaseManagementEvents events, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            IEnumerable<DataReaderPropertyDetail> columns = GetOrdinalPropertiesInEntity(propertyOptions, query, reader);
            List<T> result = [];
            Dictionary<int, ITypeHandler<TDbDataReader>> typeHandlers = GetTypeHandlers(columns, events);

            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                Fill(reader, columns, typeHandlers, result);
            }

            return result;
        }

        public override T CreateEntity(IEnumerable<PropertyValue> propertyValues)
        {
            return _transformTo.CreateEntity(propertyValues);
        }
    }
}