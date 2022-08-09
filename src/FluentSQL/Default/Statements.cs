using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL.Default
{
    public class Statements : IStatements
    {
        /// <summary>
        /// Instructions to separate columns or table example "{0}"
        /// </summary>
        public virtual string Format => "{0}";        

        /// <summary>
        /// Instructions to format the select, example "SELECT {0} FROM {1};"
        /// </summary>
        public virtual string SelectText => "SELECT {0} FROM {1};";

        /// <summary>
        /// Instructions to format the select with where, example "SELECT {0} FROM {1} WHERE {2};"
        /// </summary>
        public virtual string SelectWhereText => "SELECT {0} FROM {1} WHERE {2};";

        /// <summary>
        /// Instructions to format the insert, example "INSERT INTO {0} ({1}) VALUES ({2});"
        /// </summary>
        public virtual string InsertText => "INSERT INTO {0} ({1}) VALUES ({2});";

        /// <summary>
        /// Instrucciones para formatear el update, ejemplo "UPDATE {0} SET {1} WHERE {2};"
        /// </summary>
        public virtual string UpdateText => "UPDATE {0} SET {1} WHERE {2};";

        /// <summary>
        /// Instructions to format the delete, example "DELETE FROM {0} WHERE {1};"
        /// </summary>
        public virtual string DeleteWhereText => "DELETE FROM {0} WHERE {1};";
    }
}
