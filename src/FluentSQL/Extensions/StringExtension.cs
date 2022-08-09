using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL.Extensions
{
    internal static class StringExtension
    {
        internal static void NullValidate(this string obj, string message, string paramName)
        {
            if (string.IsNullOrWhiteSpace(obj))
            {
                throw new ArgumentNullException(paramName, message);
            }
        }
    }
}
