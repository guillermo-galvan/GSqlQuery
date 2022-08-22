using FluentSQL.Extensions;
using FluentSQL.Models;
using FluentSQL.SearchCriteria;

namespace FluentSQL.Default
{
    /// <summary>
    /// Where
    /// </summary>
    internal abstract class BaseWhere : ISearchCriteriaBuilder 
    {
        protected readonly List<ISearchCriteria> _searchCriterias = new();

        protected readonly ClassOptions _classOptions;

        public BaseWhere(ClassOptions classOptions)
        {
            _classOptions = classOptions;
        }

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
