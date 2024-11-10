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
            DataTable dataTable = new DataTable(classOption.Table.Name);
            foreach (PropertyOptions property in classOption.PropertyOptions)
            {
                DataColumn dataColumn = new DataColumn
                {
                    DataType = Nullable.GetUnderlyingType(property.PropertyInfo.PropertyType) ?? property.PropertyInfo.PropertyType,
                    ColumnName = property.ColumnAttribute.Name,
                    AutoIncrement = property.ColumnAttribute.IsAutoIncrementing
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
