namespace GSqlQuery.Runner.Transforms
{
    internal abstract class TransformTo<T> : ITransformTo<T>
    {
        protected readonly int _numColumns;
        protected readonly ClassOptions _classOptions;

        public TransformTo(int numColumns)
        {
            _numColumns = numColumns;
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(T));
        }

        public abstract T Generate();

        public abstract void SetValue(int position, string propertyName, object value);
    }
}