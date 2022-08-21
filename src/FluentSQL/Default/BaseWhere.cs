using FluentSQL.Extensions;
using FluentSQL.SearchCriteria;

namespace FluentSQL.Default
{
    /// <summary>
    /// Where
    /// </summary>
    internal abstract class BaseWhere : ISearchCriteriaBuilder
    {
        protected readonly List<ISearchCriteria> _searchCriterias = new();

        /// <summary>
        /// Add a search criteria
        /// </summary>
        /// <param name="criteria"></param>
        public void Add(ISearchCriteria criteria)
        {
            criteria.NullValidate(ErrorMessages.ParameterNotNull, nameof(criteria));
            _searchCriterias.Add(criteria);
        }

        /// <summary>
        /// Build the criteria
        /// </summary>
        /// <returns>Criteria detail enumerable</returns>
        public abstract IEnumerable<CriteriaDetail> BuildCriteria();
    }
}
