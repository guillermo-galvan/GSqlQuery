using System;

namespace GSqlQuery.Extensions
{
    /// <summary>
    ///  Class for internal use
    /// </summary>
    internal static class ObjectExtension
    {
        /// <summary>
        /// Validate if an object is null
        /// </summary>
        /// <param name="obj">Object to evaluate</param>
        /// <param name="message">Error message</param>
        /// <param name="paramName">Parameter name</param>
        /// <exception cref="ArgumentNullException"></exception>
        internal static void NullValidate(this object obj, string message, string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName, message);
            }
        }
    }
}