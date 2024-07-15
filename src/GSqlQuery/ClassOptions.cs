using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GSqlQuery
{

    public sealed class PropertyOptionsCollection
    {
        private readonly Dictionary<string, PropertyOptions> _properties;
        private readonly Dictionary<string, string> _lowCase;

        internal PropertyOptionsCollection(IEnumerable<KeyValuePair<string, PropertyOptions>> keyValuePairs)
        {
            _properties = [];
            _lowCase = [];

            foreach (var item in keyValuePairs)
            {
                _properties.Add(item.Key, item.Value);
                _lowCase.Add(item.Key.ToLower(), item.Key);
            }
        }

        internal IEnumerable<string> Keys { get { return _properties.Keys; } }

        internal IEnumerable<PropertyOptions> Values { get { return _properties.Values; } }

        public IEnumerable<KeyValuePair<string, PropertyOptions>> KeyValues { get { return _properties.AsEnumerable(); } }


        internal void Add(string key, PropertyOptions propertyOptions)
        {
            _properties.Add(key, propertyOptions);
        }

        public int Count()
        {
            return _properties.Count;
        }
        public PropertyOptions TyrGetValueToLower(string key)
        {
            if (_lowCase.TryGetValue(key.ToLower(), out string tmpKey))
            {
                return _properties.TryGetValue(tmpKey, out PropertyOptions tmp) ? tmp : null;
            }
            else
            {
                return null;
            }
        }

        public PropertyOptions First()
        {
            return _properties.First().Value;
        }

        public PropertyOptions this[string key]
        {
            get 
            { 
                return _properties.TryGetValue(key, out PropertyOptions tmp) ? tmp : null;
            }
            internal set 
            {
                    _properties[key] = value;
            }
        }
    }

    /// <summary>
    /// Class Options
    /// </summary>
    public sealed class ClassOptions
    {
        private PropertyOptionsCollection _propertyOptions;

        /// <summary>
        /// Get Type
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Get properties
        /// </summary>
        public PropertyOptionsCollection PropertyOptions 
        {
            get
            {
                return _propertyOptions;
            }
        }

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
            _propertyOptions = new PropertyOptionsCollection(GetProperties());
            ConstructorInfo = GetConstructor() ?? throw new Exception("No constructor found");
        }

        private IEnumerable<KeyValuePair<string, PropertyOptions>> GetProperties()
        {
            PropertyInfo[] properties = Type.GetProperties();
            if(properties.Length == 0)
            {
                throw new Exception($"{Type.Name} has no properties");
            }
            return properties.Select(x => new KeyValuePair<string, PropertyOptions>(x.Name, new PropertyOptions(0, x, (Attribute.GetCustomAttribute(x, typeof(ColumnAttribute)) ?? new ColumnAttribute(x.Name)) as ColumnAttribute, Table))).ToList();
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
                else if (parameters.Length > 0 && parameters.Length == _propertyOptions.Count())
                {
                    bool find = true;
                    byte position = 0;
                    var tmp = parameters.Select(param => new { Name = param.Name?.ToUpper(), param.ParameterType }).ToArray();

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var param = tmp[i];
                        PropertyOptions propertyOptions = _propertyOptions.TyrGetValueToLower(param.Name);

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

            IsConstructorByParam = result != null;

            return result ?? ConstructorInfoDefault;
        }

        private TableAttribute GetTableAttribute()
        {
            return (Attribute.GetCustomAttribute(Type, typeof(TableAttribute)) ?? new TableAttribute(Type.Name)) as TableAttribute;
        }
    }
}