using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string TableName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Scheme { get; private set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public TableAttribute( string tableName) 
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException(nameof(tableName));
            }

            TableName = tableName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheme"></param>
        /// <param name="tableName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public TableAttribute(string scheme, string tableName) : this(tableName)
        {
            if (string.IsNullOrWhiteSpace(scheme))
            {
                throw new ArgumentNullException(nameof(scheme));
            }

            Scheme = scheme;
        }
    }
}
