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
        private ITransformTo<TDbDataReader> _transformTo;

        public PropertyOptions PropertyOptions { get; set; }

        public ClassOptions ClassOptions { get; set; }

        public IEnumerable<PropertyOptionsInEntity> PropertyOptionsInEntities { get; set; }

        public MethodInfo MethodInfo { get; set; }

        public object Class
        {
            get { return _class; }
            set
            {
                _class = value;

                if (_class is ITransformTo<TDbDataReader> tmp)
                {
                    _transformTo = tmp;
                }
            }
        }

        public ITransformTo<TDbDataReader> Transform => _transformTo;

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
            _joinClassOptions = _classOptions.PropertyOptions.Where(x => x.PropertyInfo.PropertyType.IsClass).Select(x => new JoinClassOptions<TDbDataReader>()
            {
                PropertyOptions = x,
                ClassOptions = ClassOptionsFactory.GetClassOptions(x.PropertyInfo.PropertyType),
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

        protected static IEnumerable<PropertyOptionsInEntity> GetPropertiesJoin(ClassOptions classOptions,
            IEnumerable<PropertyOptions> propertyOptionsColumns, DbDataReader reader)
        {
            return (from pro in classOptions.PropertyOptions
                    join ca in propertyOptionsColumns on pro.ColumnAttribute.Name equals ca.ColumnAttribute.Name into leftJoin
                    from left in leftJoin.DefaultIfEmpty()
                    select
                        new PropertyOptionsInEntity(pro,
                        Nullable.GetUnderlyingType(pro.PropertyInfo.PropertyType) ?? pro.PropertyInfo.PropertyType,
                        pro.PropertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(pro.PropertyInfo.PropertyType) : null,
                        left != null ? reader.GetOrdinal($"{classOptions.Type.Name}_{pro.ColumnAttribute.Name}") : null))
                        .ToArray();
        }

        protected override IEnumerable<PropertyOptionsInEntity> GetOrdinalPropertiesInEntity(IEnumerable<PropertyOptions> propertyOptions, IQuery<T> query, TDbDataReader reader)
        {
            List<PropertyOptionsInEntity> result = [];

            IEnumerable<IGrouping<string, PropertyOptions>> columnGroup = query.Columns.GroupBy(x => x.TableAttribute.Name);

            foreach (JoinClassOptions<TDbDataReader> item in _joinClassOptions)
            {
                IGrouping<string, PropertyOptions> tmpColumns = columnGroup.First(x => x.Key == item.ClassOptions.Table.Name);
                item.PropertyOptionsInEntities = GetPropertiesJoin(item.ClassOptions, tmpColumns, reader);
                result.AddRange(item.PropertyOptionsInEntities);

                MethodInfo methodInfo = _events.GetType().GetMethod("GetTransformTo").MakeGenericMethod(item.ClassOptions.Type, reader.GetType());
                item.Class = methodInfo?.Invoke(_events, [item.ClassOptions]);
                item.MethodInfo = item.Class.GetType().GetMethod("CreateEntity");
            }

            return result;
        }

        private void Fill(TDbDataReader reader, Queue<PropertyValue> propertyValues, Queue<PropertyValue> jointPropertyValues, Queue<T> result)
        {
            foreach (JoinClassOptions<TDbDataReader> joinClassOptions in _joinClassOptions)
            {
                foreach (PropertyOptionsInEntity item in joinClassOptions.PropertyOptionsInEntities)
                {
                    if (item.Ordinal.HasValue)
                    {
                        propertyValues.Enqueue(new PropertyValue(item.Property, joinClassOptions.Transform.GetValue(item.Ordinal.Value, reader, item.Type)));
                    }
                    else
                    {
                        propertyValues.Enqueue(new PropertyValue(item.Property, item.DefaultValue));
                    }
                }
                object a = joinClassOptions.MethodInfo.Invoke(joinClassOptions.Class, [propertyValues]);
                propertyValues.Clear();
                jointPropertyValues.Enqueue(new PropertyValue(joinClassOptions.PropertyOptions, a));
            }

            T tmp = CreateEntity(jointPropertyValues);
            jointPropertyValues.Clear();
            result.Enqueue(tmp);
        }

        public override IEnumerable<T> Transform(IEnumerable<PropertyOptions> propertyOptions, IQuery<T> query, TDbDataReader reader)
        {
            _ = GetOrdinalPropertiesInEntity(propertyOptions, query, reader);
            Queue<T> result = new Queue<T>();
            Queue<PropertyValue> propertyValues = new Queue<PropertyValue>();
            Queue<PropertyValue> jointPropertyValues = new Queue<PropertyValue>();

            while (reader.Read())
            {
                Fill(reader, propertyValues, jointPropertyValues, result);
            }

            return result;
        }

        public override async Task<IEnumerable<T>> TransformAsync(IEnumerable<PropertyOptions> propertyOptions, IQuery<T> query, TDbDataReader reader, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _ = GetOrdinalPropertiesInEntity(propertyOptions, query, reader);
            Queue<T> result = new Queue<T>();
            Queue<PropertyValue> propertyValues = new Queue<PropertyValue>();
            Queue<PropertyValue> jointPropertyValues = new Queue<PropertyValue>();

            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                Fill(reader, propertyValues, jointPropertyValues, result);
            }

            return result;
        }

        public override T CreateEntity(IEnumerable<PropertyValue> propertyValues)
        {
            return _transformTo.CreateEntity(propertyValues);
        }
    }
}