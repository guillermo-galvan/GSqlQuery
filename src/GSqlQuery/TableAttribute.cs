using System;

namespace GSqlQuery
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public sealed class TableAttribute : Attribute
    {
        /// <summary>
        /// Get table name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Get scheme 
        /// </summary>
        public string Scheme { get; private set; } = null;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="name">Table name</param>
        /// <exception cref="ArgumentNullException"></exception>
        public TableAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="scheme">Scheme</param>
        /// <param name="name">Table name</param>
        /// <exception cref="ArgumentNullException"></exception>
        public TableAttribute(string scheme, string name) : this(name)
        {
            if (string.IsNullOrWhiteSpace(scheme))
            {
                throw new ArgumentNullException(nameof(scheme));
            }

            Scheme = scheme;
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Scheme) ? $"Table Name: {Name}" : $"Scheme Name: {Scheme}, Table Name: {Name}";
        }
    }
}