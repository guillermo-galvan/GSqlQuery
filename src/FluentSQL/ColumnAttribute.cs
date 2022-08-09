using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    /// <summary>
    /// Defines how the property will be taken for the query
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// Column name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Column size
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Defines if the column is a primary key
        /// </summary>
        public bool IsPrimaryKey { get; private set; }

        /// <summary>
        /// Defines if the column is auto-incrementing
        /// </summary>
        public bool IsAutoIncrementing { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Column name</param>
        public ColumnAttribute(string name) :
            this(name, 0, false, false)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Column name</param>
        /// <param name="size">Column size</param>
        public ColumnAttribute(string name, int size) :
            this(name, size, false, false)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Column name</param>
        /// <param name="size">Column size</param>
        /// <param name="isPrimaryKey">Defines if the column is a primary key</param>
        public ColumnAttribute(string name, int size, bool isPrimaryKey) :
            this(name, size, isPrimaryKey, false)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Column name</param>
        /// <param name="size">Column size</param>
        /// <param name="isPrimaryKey">Defines if the column is a primary key</param>
        /// <param name="isIdentity">Defines if the column is auto-incrementing</param>
        public ColumnAttribute(string name, int size, bool isPrimaryKey, bool isAutoIncrementing)
        {
            Name = name;
            Size = size;
            IsPrimaryKey = isPrimaryKey;
            IsAutoIncrementing = isAutoIncrementing;
        }
    }
}
