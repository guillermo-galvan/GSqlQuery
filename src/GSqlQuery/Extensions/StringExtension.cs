using System;

namespace GSqlQuery.Extensions
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
