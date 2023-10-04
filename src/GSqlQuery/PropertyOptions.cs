using System.Reflection;

namespace GSqlQuery
{
    /// <summary>
    /// Column Info
    /// </summary>
    public sealed class PropertyOptions
    {
        /// <summary>
        /// Get Position Constructor
        /// </summary>
        public int PositionConstructor { get; internal set; }

        /// <summary>
        /// Get PropertyInfo
        /// </summary>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        /// Get ColumnAttribute
        /// </summary>
        public ColumnAttribute ColumnAttribute { get; }

        /// <summary>
        /// Get TableAttribute
        /// </summary>
        public TableAttribute TableAttribute { get; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="positionObject">Position Constructor</param>
        /// <param name="propertyInfo">PropertyInfo</param>
        /// <param name="columnAttribute">ColumnAttribute</param>
        /// <param name="tableAttribute">TableAttribute</param>
        public PropertyOptions(int positionObject, PropertyInfo propertyInfo, ColumnAttribute columnAttribute, TableAttribute tableAttribute)
        {
            PositionConstructor = positionObject;
            PropertyInfo = propertyInfo;
            ColumnAttribute = columnAttribute;
            TableAttribute = tableAttribute;
        }
    }
}