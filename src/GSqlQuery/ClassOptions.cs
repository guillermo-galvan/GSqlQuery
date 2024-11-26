using GSqlQuery.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GSqlQuery
{
    /// <summary>
    /// Class Options
    /// </summary>
    public sealed class ClassOptions
    {
        /// <summary>
        /// Get Type
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Get properties
        /// </summary>
        public PropertyOptionsCollection PropertyOptions { get; }

        /// <summary>
        /// Get default construtor
        /// </summary>
        public ConstructorInfo ConstructorInfo { get; private set; }

        /// <summary>
        /// Is Constructor By Param
        /// </summary>
        public bool IsConstructorByParam { get; private set; }

        /// <summary>
        /// Get table
        /// </summary>
        public FormatTableNameCollection FormatTableName { get; private set; }

        internal object Entity { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="type">Type</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public ClassOptions(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            FormatTableName = GetTableAttribute();
            PropertyOptions = new PropertyOptionsCollection(GetProperties());
            ConstructorInfo = GetConstructor() ?? throw new Exception("No constructor found");
        }

        private IEnumerable<KeyValuePair<string, PropertyOptions>> GetProperties()
        {
            PropertyInfo[] properties = Type.GetProperties();
            if (properties.Length == 0)
            {
                throw new Exception($"{Type.Name} has no properties");
            }



            return properties.Select(x =>
            {
                ColumnAttribute columnAttribute = (Attribute.GetCustomAttribute(x, typeof(ColumnAttribute)) ?? new ColumnAttribute(x.Name)) as ColumnAttribute;
                FormatColumnNameCollection formatColumnNameCollection = new FormatColumnNameCollection(columnAttribute, FormatTableName);
                PropertyOptions propertyOptions = new PropertyOptions(0, x, (Attribute.GetCustomAttribute(x, typeof(ColumnAttribute)) ?? new ColumnAttribute(x.Name)) as ColumnAttribute, formatColumnNameCollection, FormatTableName.Table);

                return new KeyValuePair<string, PropertyOptions>(x.Name, propertyOptions);
            }).ToList();
        }

        private ConstructorInfo GetConstructor()
        {
            ConstructorInfo[] constructorInfos = Type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (constructorInfos.Length == 0)
            {
                throw new InvalidOperationException();
            }

            ConstructorInfo constructorInfoDefault = null;
            ConstructorInfo result = null;

            foreach (ConstructorInfo item in constructorInfos)
            {
                ParameterInfo[] parameters = item.GetParameters();

                if (parameters.Length == 0)
                {
                    constructorInfoDefault = item;
                    Entity = constructorInfoDefault.Invoke(null);
                }
                else if (parameters.Length > 0 && parameters.Length == PropertyOptions.Count)
                {
                    bool find = true;
                    byte position = 0;
                    var tmp = parameters.Select(param => new { Name = param.Name?.ToUpper(), param.ParameterType }).ToArray();

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var param = tmp[i];
                        PropertyOptions propertyOptions = PropertyOptions[param.Name];

                        if (propertyOptions == null || propertyOptions.PropertyInfo.PropertyType != param.ParameterType)
                        {
                            find = false;
                            break;
                        }
                        else
                        {
                            propertyOptions.PositionConstructor = position++;
                        }
                    }

                    result = find ? item : result;
                }
            }

            Entity = Entity ?? CreateEntity(constructorInfos);
            IsConstructorByParam = result != null;

            return result ?? constructorInfoDefault;
        }

        private FormatTableNameCollection GetTableAttribute()
        {
            TableAttribute tableAttribute = (Attribute.GetCustomAttribute(Type, typeof(TableAttribute)) ?? new TableAttribute(Type.Name)) as TableAttribute;
            return new FormatTableNameCollection(tableAttribute);
        }

        private object CreateEntity(ConstructorInfo[] constructorInfos)
        {
            ConstructorInfo constructorInfo = constructorInfos[0];

            ParameterInfo[] parameters = constructorInfo.GetParameters();
            object[] objects = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo param = parameters[i];
                Type type = param.ParameterType;
                objects[i] = type.IsValueType ? Activator.CreateInstance(type) : null;
            }

            return constructorInfo.Invoke(objects);
        }
    }
}