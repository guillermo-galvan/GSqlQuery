using System;

namespace GSqlQuery.Extensions
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