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
        public PropertyOptions PropertyOptions { get; set; }

        public ClassOptions ClassOptions { get; set; }

        public IEnumerable<DataReaderPropertyDetail> PropertyOptionsInEntities { get; set; }

        public object TransformToClass { get; set; }

        public MethodInfo GetEntityMethodInfo { get; set; }

        public MethodInfo SetValueMethodInfo { get; set; }
    }

    internal class JoinTransformTo<T, TDbDataReader> : TransformTo<T, TDbDataReader>
        where T : class
        where TDbDataReader : DbDataReader
    {
        private JoinClassOptions<TDbDataReader>[] _joinClassOptions;
        private readonly ITransformTo<T, TDbDataReader> _transformTo;
        private readonly DatabaseManagementEvents _events;

        public JoinTransformTo(int numColumns, DatabaseManagementEvents events) : base(numColumns)
        {
            _events = events;
            _joinClassOptions = _classOptions.PropertyOptions.Where(x => x.Value.PropertyInfo.PropertyType.IsClass).Select(x => new JoinClassOptions<TDbDataReader>()
            {
                PropertyOptions = x.Value,
                ClassOptions = ClassOptionsFactory.GetClassOptions(x.Value.PropertyInfo.PropertyType),
            }).ToArray();

            if (!_classOptions.IsConstructorByParam)
            {
                _transformTo = new TransformToByField<T, TDbDataReader>(_classOptions.PropertyOptions.Count);
            }
            else
            {
                _transformTo = new TransformToByConstructor<T, TDbDataReader>(_classOptions.PropertyOptions.Count);
            }
        }

        protected static IEnumerable<DataReaderPropertyDetail> GetPropertiesJoin(ClassOptions classOptions, IEnumerable<PropertyOptions> propertyOptionsColumns, DbDataReader reader)
        {
            var propertyOptionsDict = propertyOptionsColumns.ToDictionary(ca => ca.ColumnAttribute.Name);
            return classOptions.PropertyOptions
                .Select(pro => new DataReaderPropertyDetail(
                    pro.Value,
                    propertyOptionsDict.TryGetValue(pro.Value.ColumnAttribute.Name, out var ca)
                        ? reader.GetOrdinal($"{classOptions.Type.Name}_{pro.Value.ColumnAttribute.Name}")
                        : null))
                .ToArray();
        }

        private void OrdinalPropertiesInEntity(Dictionary<string, IEnumerable<PropertyOptions>> columnGroup, TDbDataReader reader)
        {
            foreach (JoinClassOptions<TDbDataReader> item in _joinClassOptions)
            {
                if (columnGroup.TryGetValue(item.ClassOptions.FormatTableName.Table.Name, out var tmpColumns))
                {
                    item.PropertyOptionsInEntities = GetPropertiesJoin(item.ClassOptions, tmpColumns, reader);

                    MethodInfo methodInfo = _events.GetType().GetMethod("GetTransformTo").MakeGenericMethod(item.ClassOptions.Type, reader.GetType());
                    item.TransformToClass = methodInfo?.Invoke(_events, [item.ClassOptions]);

                    item.SetValueMethodInfo = item.TransformToClass.GetType().GetMethod("SetValue");
                    item.GetEntityMethodInfo = item.TransformToClass.GetType().GetMethod("GetEntity");
                }
            }

            _joinClassOptions = [.. _joinClassOptions.OrderBy(x => x.PropertyOptionsInEntities.Min(y => y.Ordinal))];
        }

        private void Fill(TDbDataReader reader, DatabaseManagementEvents events, Dictionary<int, ITypeHandler<TDbDataReader>> typeHandlers, List<T> result)
        {
            foreach (JoinClassOptions<TDbDataReader> joinClassOptions in _joinClassOptions)
            {
                foreach (DataReaderPropertyDetail item in joinClassOptions.PropertyOptionsInEntities)
                {
                    int? ordinal = item.Ordinal;
                    object value;

                    if (ordinal.HasValue && !reader.IsDBNull(ordinal.Value))
                    {
                        if (!typeHandlers.TryGetValue(ordinal.Value, out var typeHandler))
                        {
                            typeHandler = events.GetHandler<TDbDataReader>(item.Property.PropertyInfo.PropertyType);
                            typeHandlers[ordinal.Value] = typeHandler;
                        }
                        value = typeHandler.GetValue(reader, ordinal.Value);
                    }
                    else
                    {
                        value = item.Property.DefaultValue;
                    }

                    joinClassOptions.SetValueMethodInfo.Invoke(joinClassOptions.TransformToClass, [item.Property, value]);
                }
                object entity = joinClassOptions.GetEntityMethodInfo.Invoke(joinClassOptions.TransformToClass, []);
                _transformTo.SetValue(joinClassOptions.PropertyOptions, entity);
            }

            T tmp = _transformTo.GetEntity();
            result.Add(tmp);
        }

        public override IEnumerable<T> Transform(PropertyOptionsCollection propertyOptions, IQuery<T> query, TDbDataReader reader, DatabaseManagementEvents events)
        {
            Dictionary<string, IEnumerable<PropertyOptions>> columnGroup = query.Columns.GroupBy(x => x.Value.Table.Name).ToDictionary(g => g.Key, g => g.Select(x => x.Value));
            OrdinalPropertiesInEntity(columnGroup, reader);
            List<T> result = [];
            Dictionary<int, ITypeHandler<TDbDataReader>> typeHandlers = [];

            while (reader.Read())
            {
                Fill(reader, events, typeHandlers, result);
            }

            return result;
        }

        public override async Task<IEnumerable<T>> TransformAsync(PropertyOptionsCollection propertyOptions, IQuery<T> query, TDbDataReader reader, DatabaseManagementEvents events, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Dictionary<string, IEnumerable<PropertyOptions>> columnGroup = query.Columns.GroupBy(x => x.Value.Table.Name).ToDictionary(g => g.Key, g => g.Select(x => x.Value));
            OrdinalPropertiesInEntity(columnGroup, reader);
            List<T> result = [];
            Dictionary<int, ITypeHandler<TDbDataReader>> typeHandlers = [];

            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                Fill(reader, events, typeHandlers, result);
            }

            return result;
        }

        public override void SetValue(PropertyOptions property, object value)
        {
            _transformTo.SetValue(property, value);
        }

        public override T GetEntity()
        {
            return _transformTo.GetEntity();
        }
    }
}