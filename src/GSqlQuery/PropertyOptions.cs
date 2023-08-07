using System.Reflection;

namespace GSqlQuery
{
    public sealed class PropertyOptions
    {
        public int PositionConstructor { get; internal set; }

        public PropertyInfo PropertyInfo { get; }

        public ColumnAttribute ColumnAttribute { get; }

        public TableAttribute TableAttribute { get; }

        public PropertyOptions(int positionObject, PropertyInfo propertyInfo, ColumnAttribute columnAttribute, TableAttribute tableAttribute)
        {
            PositionConstructor = positionObject;
            PropertyInfo = propertyInfo;
            ColumnAttribute = columnAttribute;
            TableAttribute = tableAttribute;
        }
    }
}