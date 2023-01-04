using System;
using System.Collections.Generic;
using System.Data;

namespace GSqlQuery
{
    public static class BulkCopyExtension
    {
        private static DataTable CreateTable(ClassOptions classOption)
        {
            DataTable dataTable = new DataTable(classOption.Table.Name);
            foreach (PropertyOptions property in classOption.PropertyOptions)
            {
                DataColumn dataColumn = new DataColumn();
                dataColumn.DataType = property.PropertyInfo.PropertyType;
                dataColumn.ColumnName = property.ColumnAttribute.Name;
                dataColumn.AutoIncrement = property.ColumnAttribute.IsAutoIncrementing;
                dataTable.Columns.Add(dataColumn);
            }

            return dataTable;
        }

        public static DataTable FillTable<T>(IEnumerable<T> values)
        {
            var classOption = ClassOptionsFactory.GetClassOptions(typeof(T));

            DataTable dataTable = CreateTable(classOption);

            foreach (var val in values)
            {
                var row = dataTable.NewRow();

                foreach (PropertyOptions property in classOption.PropertyOptions)
                {
                    row[property.ColumnAttribute.Name] = property.PropertyInfo.GetValue(val) ?? DBNull.Value;
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
    }
}
