using System;

namespace GSqlQuery.Extensions
{
    /// <summary>
    /// String Extension
    /// </summary>
    internal static class StringExtension
    {
        /// <summary>
        ///  Validate if an string is null
        /// </summary>
        /// <param name="obj">String to evaluate</param>
        /// <param name="message">Error message</param>
        /// <param name="paramName">Parameter name</param>
        /// <exception cref="ArgumentNullException"></exception>
        internal static void NullValidate(this string obj, string message, string paramName)
        {
            if (string.IsNullOrWhiteSpace(obj))
            {
                throw new ArgumentNullException(paramName, message);
            }
        }
    }
}