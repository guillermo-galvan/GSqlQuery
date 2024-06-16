using GSqlQuery.Extensions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria is not null
    /// </summary>
    /// <param name="table">Table Attribute</param>
    /// <param name="classOptionsTupla">ClassOptionsTupla</param>
    /// <param name="formats">Formats</param>
    internal class IsNotNull(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, string logicalOperator) :
        IsNull(classOptionsTupla, formats, logicalOperator)
    {
        protected override string RelationalOperator => "IS NOT NULL";

        /// <summary>
        /// Initializes a new instance of the IsNotNull class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        public IsNotNull(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats) :
            this(classOptionsTupla, formats, null)
        { }
    }
}