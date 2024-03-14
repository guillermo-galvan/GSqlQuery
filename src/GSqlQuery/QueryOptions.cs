using System;

namespace GSqlQuery
{
    /// <summary>
    /// Query Options
    /// </summary>
    /// <param name="formats">IFormats</param>
    public class QueryOptions(IFormats formats)
    {
        /// <summary>
        /// Formats
        /// </summary>
        public IFormats Formats { get; } = formats ?? throw new ArgumentNullException(nameof(formats));
    }
}
