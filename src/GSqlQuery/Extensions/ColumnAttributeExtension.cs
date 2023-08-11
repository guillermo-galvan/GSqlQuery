using GSqlQuery.Queries;
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
        internal static string GetColumnName(this ColumnAttribute column, string tableName, IStatements statements, QueryType queryType)
        {
            column.NullValidate(ErrorMessages.ParameterNotNull, nameof(column));
            tableName.NullValidate(ErrorMessages.ParameterNotNull, nameof(tableName));
            statements.NullValidate(ErrorMessages.ParameterNotNull, nameof(statements));

            return statements.GetColumnName(tableName, column, queryType);
        }

        internal static PropertyOptions GetPropertyOptions(this ColumnAttribute column, IEnumerable<PropertyOptions> propertyOptions)
        {
            return propertyOptions.First(x => x.ColumnAttribute.Name == column.Name);
        }

        internal static string GetColumnNameJoin(this ColumnAttribute column, JoinInfo joinInfo, IStatements statements)
        {
            string alias = GetAliasJoinFormat(statements, joinInfo.ClassOptions.Type.Name, column.Name);
            string columnName = statements.GetColumnName(joinInfo.ClassOptions.Table.GetTableName(statements), column, QueryType.Join);
            return $"{columnName} as {alias}";
        }

        internal static string GetAliasJoinFormat(IStatements statements, string tableName, string columnName)
        {
            return string.Format(statements.Format, $"{tableName}_{columnName}");
        }

        internal static string GetAliasJoin(string tableName, string columnName)
        {
            return $"{tableName}_{columnName}";
        }
    }
}