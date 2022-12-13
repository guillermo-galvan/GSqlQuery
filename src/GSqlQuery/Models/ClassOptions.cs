using System.Reflection;
using GSqlQuery.Extensions;

namespace GSqlQuery.Models
{
    public sealed class ClassOptions
    {
        public Type Type { get; private set; }

        public IEnumerable<PropertyOptions> PropertyOptions { get; private set; }

        public ConstructorInfo ConstructorInfo { get; private set; }

        public bool IsConstructorByParam { get; private set; }

        public TableAttribute Table { get; private set; }

        public ClassOptions(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            PropertyOptions = GetProperties();
            if (!PropertyOptions.Any())
            {
                throw new Exception($"{Type.Name} has no properties");
            }
            ConstructorInfo = GetConstructor() ?? throw new Exception("No constructor found");
            Table = GetTableAttribute();
        }

        private Queue<PropertyOptions> GetProperties()
        {
            Queue<PropertyOptions> properties = new();

            foreach (PropertyInfo property in Type.GetProperties())
            {
                Attribute[] arrayAttribute = Attribute.GetCustomAttributes(property);

                if (arrayAttribute.FirstOrDefault(x => x is ColumnAttribute) is not ColumnAttribute tmp)
                {
                    tmp = new ColumnAttribute(property.Name);
                }

                properties.Enqueue(new PropertyOptions(0, property, tmp));
            }

            return properties;
        }

        private ConstructorInfo? GetConstructor()
        {
            ConstructorInfo[] constructorInfos = Type.GetConstructors();

            ConstructorInfo? ConstructorInfoDefault = null;
            ConstructorInfo? result = null;

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
                        PropertyOptions? typeparam = PropertyOptions.FirstOrDefault(x => x.PropertyInfo.Name.ToUpper() == param.Name?.ToUpper() &&
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
                return (TableAttribute)arrayAttributeClass.First(x => x is TableAttribute);
            }
            else
            {
                return new TableAttribute(Type.Name);
            }
        }
    }
}
