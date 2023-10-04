using System.Collections.Generic;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// Search criteria
    /// </summary>
    public interface ISearchCriteria
    {
        /// <summary>
        /// Get Column
        /// </summary>
        ColumnAttribute Column { get; }

        /// <summary>
        /// Get Table
        /// </summary>
        TableAttribute Table { get; }

        /// <summary>
        /// Get Criteria detail
        /// </summary>
        /// <param name="formats">formats</param>
        /// <returns>Details of the criteria</returns>
        CriteriaDetail GetCriteria(IFormats formats, IEnumerable<PropertyOptions> propertyOptions);
    }
}