using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITransformTo<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        void SetValue(int position, string propertyName, object? value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        T Generate();
    }
}
