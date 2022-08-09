using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL.Extensions
{
    internal static class ObjectExtension
    {
        internal static void NullValidate(this object obj, string message, string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName, message);
            }
        }
    }
}
