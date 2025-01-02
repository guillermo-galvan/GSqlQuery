using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GSqlQuery
{
    public static class BatchExtension
    {
        private static DataTable CreateTable(ClassOptions classOption)
        {
            DataTable dataTable = new DataTable(classOption.FormatTableName.Table.Name);
            foreach (KeyValuePair<string, PropertyOptions> property in classOption.PropertyOptions)
            {
                DataColumn dataColumn = new DataColumn
                {
                    DataType = Nullable.GetUnderlyingType(property.Value.PropertyInfo.PropertyType) ?? property.Value.PropertyInfo.PropertyType,
                    ColumnName = property.Value.ColumnAttribute.Name,
                    AutoIncrement = property.Value.ColumnAttribute.IsAutoIncrementing
                };
                dataTable.Columns.Add(dataColumn);
            }

            return dataTable;
        }

        public static DataTable FillTable<T>(IEnumerable<T> values)
        {
            if (values == null || !values.Any())
            {
                throw new InvalidOperationException("Sequence contains no elements");
            }

            ClassOptions classOption = ClassOptionsFactory.GetClassOptions(typeof(T));

            DataTable dataTable = CreateTable(classOption);

            foreach (T val in values)
            {
                DataRow row = dataTable.NewRow();

                foreach (KeyValuePair<string, PropertyOptions> property in classOption.PropertyOptions)
                {
                    row[property.Value.ColumnAttribute.Name] = property.Value.PropertyInfo.GetValue(val) ?? DBNull.Value;
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    }
}