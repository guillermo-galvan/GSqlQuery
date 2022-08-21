using Microsoft.Extensions.Logging;

namespace FluentSQL
{
    /// <summary>
    /// Options for fluent Sql
    /// </summary>
    public class FluentSQLOptions
    {
        /// <summary>
        /// ILoggerFactory for create logger  
        /// </summary>
        public ILoggerFactory? Logger { get; set; }

        /// <summary>
        /// Statements Collection to add or remove configurations.
        /// </summary>
        public ConnectionCollection ConnectionCollection { get; } = new ConnectionCollection();
    }

    
}
