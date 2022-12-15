namespace GSqlQuery.Runner.Transforms
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class TransformToByConstructor<T> : TransformTo<T>
    {
        private readonly object?[] _fields;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numColumns"></param>
        public TransformToByConstructor(int numColumns) : base(numColumns)
        {
            _fields = new object[numColumns];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override T Generate()
        {
            return (T)_classOptions.ConstructorInfo.Invoke(_fields);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public override void SetValue(int position, string propertyName, object? value)
        {
            _fields[position] = value;
        }
    }
}
