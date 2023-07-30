using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Extensions
{
    internal static class ColumnAttributeExtension
    {
        /// <summary>
        /// Get the column name with format
        /// </summary>
        /// <param name="column">ColumnAttribute</param>
        /// <param name="tableName">Table name</param>
        /// <param name="statements">IStatements</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal static string GetColumnName(this ColumnAttribute column, string tableName, IStatements statements)
        {
            column.NullValidate(ErrorMessages.ParameterNotNull, nameof(column));
            tableName.NullValidate(ErrorMessages.ParameterNotNull, nameof(tableName));
            statements.NullValidate(ErrorMessages.ParameterNotNull, nameof(statements));

            return statements.IncrudeTableNameInQuery ? $"{tableName}.{string.Format(statements.Format, column.Name)}" : $"{string.Format(statements.Format, column.Name)}";
        }

        internal static PropertyOptions GetPropertyOptions(this ColumnAttribute column, IEnumerable<PropertyOptions> propertyOptions)
        {
            return propertyOptions.First(x => x.ColumnAttribute.Name == column.Name);
        }
    }
}