namespace GSqlQuery
{
    public class PropertyValue(PropertyOptions property, object value)
    {
        public PropertyOptions Property { get; } = property;

        public object Value { get; } = value;
    }
}
