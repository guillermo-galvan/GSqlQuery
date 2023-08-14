using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using GSqlQuery.Extensions; 

namespace GSqlQuery.Runner.Transforms
{
    internal class JoinTransformTo<T> : TransformTo<T> where T : class, new()
    {
        private class JoinClassOptions
        {
            public PropertyOptions PropertyOptions { get; set; }

            public ClassOptions ClassOptions { get; set; }

            public IEnumerable<PropertyOptionsInEntity> PropertyOptionsInEntities { get; set; }

            public MethodInfo MethodInfo { get; set; }

            public object TransformTo { get; set; }

            public int Position { get; set; }
        }

        private readonly List<JoinClassOptions> _joinClassOptions;
        private readonly ITransformTo<T> _transformTo;

        public JoinTransformTo(int numColumns) : base(numColumns)
        {
            int position = 0;

            _joinClassOptions = _classOptions.PropertyOptions.Where(x => x.PropertyInfo.PropertyType.IsClass).Select(x => new JoinClassOptions() 
            {
                PropertyOptions = x,
                ClassOptions = ClassOptionsFactory.GetClassOptions(x.PropertyInfo.PropertyType),
                Position = position++,
            }).ToList();

            if (!_classOptions.IsConstructorByParam)
            {
                _transformTo = new TransformToByField<T>(_classOptions.PropertyOptions.Count());
            }
            else
            {
                _transformTo = new TransformToByConstructor<T>(_classOptions.PropertyOptions.Count());
            }
        }

        public override T Generate(IEnumerable<PropertyOptionsInEntity> columns, DbDataReader reader)
        {
            Queue<PropertyOptionsInEntity> tmpCol = new Queue<PropertyOptionsInEntity>();

            foreach (var item in _joinClassOptions)
            {
                object a  = item.MethodInfo.Invoke(item.TransformTo,new object[] { item.PropertyOptionsInEntities, reader });

                tmpCol.Enqueue(new PropertyOptionsInEntity(item.PropertyOptions, item.PropertyOptions.GetType(), a, null));
            }

            return _transformTo.Generate(tmpCol, reader);
        }

        public override IEnumerable<PropertyOptionsInEntity> GetOrdinalPropertiesInEntity(IEnumerable<PropertyOptions> propertyOptions, IQuery<T> query, DbDataReader reader)
        {
            List<PropertyOptionsInEntity> result = new List<PropertyOptionsInEntity>();

            var columnGroup = query.Columns.GroupBy(x => x.TableAttribute.Name);

            foreach (var item in _joinClassOptions)
            {
                var tmpColumns = columnGroup.First(x => x.Key == item.ClassOptions.Table.Name);
                item.PropertyOptionsInEntities = GetPropertiesJoin(item.ClassOptions, tmpColumns, reader);
                result.AddRange(item.PropertyOptionsInEntities);
                
                Type constructor;

                if (!item.ClassOptions.IsConstructorByParam)
                {
                    constructor = typeof(TransformToByField<>).MakeGenericType(item.ClassOptions.Type);
                }
                else
                {
                    constructor = typeof(TransformToByConstructor<>).MakeGenericType(item.ClassOptions.Type);
                }

               item.TransformTo = Activator.CreateInstance(constructor, item.PropertyOptionsInEntities.Count());
               item.MethodInfo = item.TransformTo.GetType().GetMethod("Generate");
            }

            return result;
        }

        protected IEnumerable<PropertyOptionsInEntity> GetPropertiesJoin(ClassOptions classOptions,
            IEnumerable<PropertyOptions> propertyOptionsColumns, DbDataReader reader)
        {
            return (from pro in classOptions.PropertyOptions
                    join ca in propertyOptionsColumns on pro.ColumnAttribute.Name equals ca.ColumnAttribute.Name into leftJoin
                    from left in leftJoin.DefaultIfEmpty()
                    select
                        new PropertyOptionsInEntity(pro,
                        Nullable.GetUnderlyingType(pro.PropertyInfo.PropertyType) ?? pro.PropertyInfo.PropertyType,
                        pro.PropertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(pro.PropertyInfo.PropertyType) : null,
                        left != null ? (int?)reader.GetOrdinal(ColumnAttributeExtension.GetAliasJoin(classOptions.Type.Name, pro.ColumnAttribute.Name)) : null))
                        .ToArray();
        }
    }
}
