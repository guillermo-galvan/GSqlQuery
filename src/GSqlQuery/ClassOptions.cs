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
            if (!PropertyOptions.Any())
            {
                throw new Exception($"{Type.Name} has no properties");
            }
            ConstructorInfo = GetConstructor() ?? throw new Exception("No constructor found");
        }

        private Queue<PropertyOptions> GetProperties()
        {
            Queue<PropertyOptions> properties = new Queue<PropertyOptions>();

            foreach (PropertyInfo property in Type.GetProperties())
            {
                Attribute[] arrayAttribute = Attribute.GetCustomAttributes(property);
#if NET5_0_OR_GREATER
                if (arrayAttribute.FirstOrDefault(x => x is ColumnAttribute) is not ColumnAttribute tmp)
                {
                    tmp = new ColumnAttribute(property.Name);
                }
#else
                ColumnAttribute tmp = (arrayAttribute.FirstOrDefault(x => x is ColumnAttribute) as ColumnAttribute) ?? new ColumnAttribute(property.Name);
#endif

                properties.Enqueue(new PropertyOptions(0, property, tmp, Table));
            }

            return properties;
        }

        private ConstructorInfo GetConstructor()
        {
            ConstructorInfo[] constructorInfos = Type.GetConstructors();

            ConstructorInfo ConstructorInfoDefault = null;
            ConstructorInfo result = null;

            foreach (ConstructorInfo item in constructorInfos)
            {
                ParameterInfo[] parameters = item.GetParameters();

                if (parameters.Length > 0 && parameters.Length == PropertyOptions.Count())
                {
                    bool find = true;
                    byte position = 0;
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        ParameterInfo param = parameters[i];
                        PropertyOptions typeparam = PropertyOptions.FirstOrDefault(x => x.PropertyInfo.Name.ToUpper() == param.Name?.ToUpper() &&
                                                                                         x.PropertyInfo.PropertyType == param.ParameterType);

                        if (typeparam == null || param.ParameterType != typeparam.PropertyInfo.PropertyType)
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
                else if (parameters.Length == 0)
                {
                    ConstructorInfoDefault = item;
                }
            }

            IsConstructorByParam = result != null;

            return result ?? ConstructorInfoDefault;
        }

        private TableAttribute GetTableAttribute()
        {
            Attribute[] arrayAttributeClass = Attribute.GetCustomAttributes(Type);

            if (arrayAttributeClass.Any(x => x is TableAttribute))
            {
                return arrayAttributeClass.First(x => x is TableAttribute) as TableAttribute;
            }
            else
            {
                return new TableAttribute(Type.Name);
            }
        }
    }
}