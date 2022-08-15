using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL.Extensions
{
    public static class ColumnAttributeExtension
    {
        /// <summary>
        /// Get the column name with format
        /// </summary>
        /// <param name="column">ColumnAttribute</param>
        /// <param name="tableName">Table name</param>
        /// <param name="statements">IStatements</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetColumnName(this ColumnAttribute column, string tableName, IStatements statements)
        {
            column.NullValidate(ErrorMessages.ParameterNotNull, nameof(column));
            tableName.NullValidate(ErrorMessages.ParameterNotNull, nameof(tableName));
            statements.NullValidate(ErrorMessages.ParameterNotNull, nameof(statements));

            return string.Format(statements.Format, column.Name);//$"{tableName}.{string.Format(statements.Format, column.Name)}";
        }
    }
}
