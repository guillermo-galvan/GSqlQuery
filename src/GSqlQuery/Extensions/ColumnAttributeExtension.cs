using GSqlQuery.Queries;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Extensions
{
    /// <summary>
    /// ColumnAttribute Extension
    /// </summary>
    internal static class ColumnAttributeExtension
    {
        /// <summary>
        /// Get the column name with format
        /// </summary>
        /// <param name="column">Contains the property information</param>
        /// <param name="tableName">Table name</param>
        /// <param name="formats">Formats for the column.</param>
        /// <param name="queryType">Query type</param>
        /// <returns>Column name</returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static string GetColumnName(this ColumnAttribute column, string tableName, IFormats formats, QueryType queryType)
        {
            column.NullValidate(ErrorMessages.ParameterNotNull, nameof(column));
            tableName.NullValidate(ErrorMessages.ParameterNotNull, nameof(tableName));
            formats.NullValidate(ErrorMessages.ParameterNotNull, nameof(formats));

            return formats.GetColumnName(tableName, column, queryType);
        }

        /// <summary>
        /// Find the properties that receive in the column parameter.
        /// </summary>
        /// <param name="column">Contains the property information</param>
        /// <param name="propertyOptions">List of property options</param>
        /// <returns>Property options</returns>
        internal static PropertyOptions GetPropertyOptions(this ColumnAttribute column, IEnumerable<PropertyOptions> propertyOptions)
        {
            return propertyOptions.First(x => x.ColumnAttribute.Name == column.Name);
        }

        /// <summary>
        /// Retrieves table name and alias for join query
        /// </summary>
        /// <param name="column">Contains the property information</param>
        /// <param name="joinInfo">Contains the information for the join query</param>
        /// <param name="formats">Formats for the column.</param>
        /// <returns>Column name for join query</returns>
        internal static string GetColumnNameJoin(this ColumnAttribute column, JoinInfo joinInfo, IFormats formats)
        {
            string alias = string.Format(formats.Format, $"{joinInfo.ClassOptions.Type.Name}_{column.Name}");
            string columnName = formats.GetColumnName(joinInfo.ClassOptions.Table.GetTableName(formats), column, QueryType.Join);
            return $"{columnName} as {alias}";
        }
    }
}