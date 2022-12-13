using System.Reflection;

namespace FluentSQL.Models
{
    public sealed class PropertyOptions
    {
        public int PositionConstructor { get; internal set; }

        public PropertyInfo PropertyInfo { get; }

        public ColumnAttribute ColumnAttribute { get; }

        public PropertyOptions(int positionObject, PropertyInfo propertyInfo, ColumnAttribute columnAttribute)
        {
            PositionConstructor = positionObject;
            PropertyInfo = propertyInfo;
            ColumnAttribute = columnAttribute;
        }
    }
}
