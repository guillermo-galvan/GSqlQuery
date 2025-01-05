namespace GSqlQuery
{
    internal sealed class DataReaderPropertyDetail
    {
        public PropertyOptions Property { get; }

        public int? Ordinal { get; }

        public DataReaderPropertyDetail(PropertyOptions propertyOptions, int? ordinal)
        {
            Property = propertyOptions;
            Ordinal = ordinal;
        }
    }
}