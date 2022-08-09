using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    /// <summary>
    /// Statements 
    /// </summary>
    public interface IStatements
    {
        /// <summary>
        /// Instructions to separate columns or table example "{0}"
        /// </summary>
        string Format { get; }

        /// <summary>
        /// Instructions to format the select, example "SELECT {0} FROM {1};"
        /// </summary>
        string SelectText { get; }

        /// <summary>
        /// Instructions to format the select with where, example "SELECT {0} FROM {1} WHERE {2};"
        /// </summary>
        string SelectWhereText { get; }

        /// <summary>
        /// Instructions to format the insert, example "INSERT INTO {0} ({1}) VALUES ({2});"
        /// </summary>
        string InsertText { get; }

        /// <summary>
        /// Instrucciones para formatear el update, ejemplo "UPDATE {0} SET {1} WHERE {2};"
        /// </summary>
        string UpdateText { get; }

        /// <summary>
        /// Instructions to format the delete, example "DELETE FROM {0} WHERE {1};"
        /// </summary>
        string DeleteWhereText { get; }
    }
}
