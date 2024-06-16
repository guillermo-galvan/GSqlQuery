namespace GSqlQuery.Extensions
{
    /// <summary>
    /// Table Attribute Extension
    /// </summary>
    public static class TableAttributeExtension
    {
        /// <summary>
        /// Get the table name with format
        /// </summary>
        /// <param name="tableAttribute">tableAttribute</param>
        /// <param name="formats">Formats for the table.</param>
        /// <returns>Table Name</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetTableName(TableAttribute tableAttribute, IFormats formats)
        {
            string tableName = formats.Format.Replace("{0}", tableAttribute.Name);

            if (string.IsNullOrWhiteSpace(tableAttribute.Scheme))
            {
                return tableName;
            }

            string schema = formats.Format.Replace("{0}", tableAttribute.Scheme) + ".";
            return schema + tableName;
        }
    }
}