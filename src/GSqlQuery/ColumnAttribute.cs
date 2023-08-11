using System;

namespace GSqlQuery
{
    /// <summary>
    /// Defines how the property will be taken for the query
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class ColumnAttribute : Attribute
    {
        /// <summary>
        /// Column name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Column size
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Defines if the column is a primary key
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// Defines if the column is auto-incrementing
        /// </summary>
        public bool IsAutoIncrementing { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Column name</param>
        public ColumnAttribute(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override string ToString()
        {
            return $"Column Name: {Name}";
        }
    }
}