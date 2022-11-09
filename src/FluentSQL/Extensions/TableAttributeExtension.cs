using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL.Extensions
{
    public static class TableAttributeExtension
    {
        public static string GetTableName(this TableAttribute tableAttribute, IStatements statements)
        {
            tableAttribute.NullValidate(ErrorMessages.ParameterNotNull, nameof(tableAttribute));
            statements.NullValidate(ErrorMessages.ParameterNotNull, nameof(statements));

            return string.IsNullOrWhiteSpace(tableAttribute.Scheme) ? string.Format(statements.Format, tableAttribute.Name) :
                  $"{string.Format(statements.Format, tableAttribute.Scheme)}.{string.Format(statements.Format, tableAttribute.Name)}";
        }
    }
}
