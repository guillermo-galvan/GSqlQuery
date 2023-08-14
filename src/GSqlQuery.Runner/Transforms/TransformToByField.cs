using System.Collections.Generic;
using System.Data.Common;

namespace GSqlQuery.Runner.Transforms
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class TransformToByField<T> : TransformTo<T> where T : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="numColumns"></param>
        public TransformToByField(int numColumns) : base(numColumns)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override T Generate(IEnumerable<PropertyOptionsInEntity> columns, DbDataReader reader)
        {
            object result = _classOptions.ConstructorInfo.Invoke(null);

            foreach (var item in columns)
            {
                var value = item.Ordinal.HasValue ? TransformTo.SwitchTypeValue(item.Type, reader.GetValue(item.Ordinal.Value)) : item.ValueDefault;

                if (value != null)
                {
                    item.Property.PropertyInfo.SetValue(result, value);
                }
            }

            return (T)result;
        }
    }
}