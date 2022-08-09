using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    /// <summary>
    /// Managmente for fluent sql 
    /// </summary>
    public static class FluentSQLManagement
    {
        internal static FluentSQLOptions _options = new();

        public static FluentSQLOptions Options => _options;

        /// <summary>
        /// Set options for fluent sql 
        /// </summary>
        /// <param name="options">options</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetOptions(FluentSQLOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }
    }
}
