namespace FluentSQL.Default
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class TransformToByField<T> : TransformTo<T>
    {
        private readonly (string propertyName, object? Value)[] _values;
        int _position = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numColumns"></param>
        public TransformToByField(int numColumns) : base(numColumns)
        {
            _values = new (string propertyName, object? Value)[numColumns];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override T Generate()
        {
            object result = _classOptions.ConstructorInfo.Invoke(null);

            foreach (var item in _classOptions.PropertyOptions)
            {
                var value = _values.First(x => x.propertyName == item.PropertyInfo.Name).Value;
                if (value != null)
                {
                    item.PropertyInfo.SetValue(result, value);
                }
            }
            _position = 0;
            return (T)result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public override void SetValue(int position, string propertyName, object? value)
        {
            _values[_position++] = (propertyName, value);
        }
    }
}
