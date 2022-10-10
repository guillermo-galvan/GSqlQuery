using System.Reflection;
using FluentSQL.Extensions;

namespace FluentSQL.Models
{
    public class ClassOptions
    {   
        public Type Type { get; private set; }

        public IEnumerable<PropertyOptions> PropertyOptions { get; private set; }

        public ConstructorInfo ConstructorInfo { get; private set; }

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

        private List<PropertyOptions> GetProperties()
        {
            List<PropertyOptions> properties = new();

            foreach (PropertyInfo property in Type.GetProperties())
            {
                Attribute[] arrayAttribute = Attribute.GetCustomAttributes(property);
                ColumnAttribute tmp;

                if (arrayAttribute.Where(x => x is ColumnAttribute).Any())
                {
                    tmp = (ColumnAttribute)arrayAttribute.First(x => x is ColumnAttribute);
                }
                else
                { 
                    tmp = new ColumnAttribute(property.Name);
                }

                properties.Add(new PropertyOptions(0,property, tmp));
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
