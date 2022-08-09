using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public StatementsCollection StatementsCollection { get; } = new StatementsCollection();
    }

    
}
