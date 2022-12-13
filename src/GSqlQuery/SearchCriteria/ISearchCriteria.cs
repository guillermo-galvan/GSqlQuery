using GSqlQuery.Models;

namespace GSqlQuery.SearchCriteria
{
    /// <summary>
    /// 
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
        /// <param name="statements">Statements</param>
        /// <returns>Details of the criteria</returns>
        CriteriaDetail GetCriteria(IStatements statements, IEnumerable<PropertyOptions> propertyOptions);
    }
}
