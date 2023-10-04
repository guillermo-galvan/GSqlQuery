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
        public static string GetTableName(this TableAttribute tableAttribute, IFormats formats)
        {
            tableAttribute.NullValidate(ErrorMessages.ParameterNotNull, nameof(tableAttribute));
            formats.NullValidate(ErrorMessages.ParameterNotNull, nameof(formats));

            return string.IsNullOrWhiteSpace(tableAttribute.Scheme) ? string.Format(formats.Format, tableAttribute.Name) :
                  $"{string.Format(formats.Format, tableAttribute.Scheme)}.{string.Format(formats.Format, tableAttribute.Name)}";
        }
    }
}