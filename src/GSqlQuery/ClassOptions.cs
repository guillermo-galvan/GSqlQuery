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
        public IEnumerable<PropertyOptions> PropertyOptions { get; private set; }

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
        public TableAttribute Table { get; private set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="type">Type</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public ClassOptions(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Table = GetTableAttribute();
            PropertyOptions = GetProperties();
            ConstructorInfo = GetConstructor() ?? throw new Exception("No constructor found");
        }

        private Queue<PropertyOptions> GetProperties()
        {
            PropertyInfo[] properties = Type.GetProperties();
            if(properties.Length == 0)
            {
                throw new Exception($"{Type.Name} has no properties");
            }
            IEnumerable<PropertyOptions> result = properties.Select(x => new PropertyOptions(0, x, (Attribute.GetCustomAttribute(x, typeof(ColumnAttribute)) ?? new ColumnAttribute(x.Name)) as ColumnAttribute, Table));
            return new Queue<PropertyOptions>(result);
        }

        private ConstructorInfo GetConstructor()
        {
            ConstructorInfo[] constructorInfos = Type.GetConstructors();

            ConstructorInfo ConstructorInfoDefault = null;
            ConstructorInfo result = null;

            foreach (ConstructorInfo item in constructorInfos)
            {
                ParameterInfo[] parameters = item.GetParameters();

                if (parameters.Length == 0)
                {
                    ConstructorInfoDefault = item;
                }
                else if (parameters.Length > 0 && parameters.Length == PropertyOptions.Count())
                {
                    bool find = true;
                    byte position = 0;
                    var tmp = parameters.Select(param => new { Name = param.Name?.ToUpper(), param.ParameterType }).ToArray();

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var param = tmp[i];
                        PropertyOptions typeparam = PropertyOptions.FirstOrDefault(x => x.PropertyInfo.Name.Equals(param.Name, StringComparison.CurrentCultureIgnoreCase) &&
                                                                                         x.PropertyInfo.PropertyType == param.ParameterType);

                        if (typeparam == null)
                        {
                            find = false;
                            break;
                        }
                        else
                        {
                            typeparam.PositionConstructor = position++;
                        }
                    }

                    result = find ? item : result;
                }
            }

            IsConstructorByParam = result != null;

            return result ?? ConstructorInfoDefault;
        }

        private TableAttribute GetTableAttribute()
        {
            return (Attribute.GetCustomAttribute(Type, typeof(TableAttribute)) ?? new TableAttribute(Type.Name)) as TableAttribute;
        }
    }
}