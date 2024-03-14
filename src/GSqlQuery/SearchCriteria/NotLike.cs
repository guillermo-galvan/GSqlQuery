using GSqlQuery.Extensions;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria not like (NOT LIKE)
    /// </summary>
    /// <param name="classOptionsTupla">ClassOptionsTupla</param>
    /// <param name="formats">Formats</param>
    /// <param name="value">Equality value</param>
    /// <param name="logicalOperator">Logical Operator</param>
    internal class NotLike(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, string value, string logicalOperator) :
        Like(classOptionsTupla, formats, value, logicalOperator)
    {
        protected override string RelationalOperator => "NOT LIKE";

        protected override string ParameterPrefix => "PNL";

        /// <summary>
        /// Initializes a new instance of the NotLike class.
        /// </summary>
        /// <param name="classOptionsTupla">ClassOptionsTupla</param>
        /// <param name="formats">Formats</param>
        /// <param name="value">Equality value</param>
        public NotLike(ClassOptionsTupla<ColumnAttribute> classOptionsTupla, IFormats formats, string value) :
            this(classOptionsTupla, formats, value, null)
        { }
    }
}