using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;
using FluentSQL.SearchCriteria;

namespace FluentSQL.Default
{
    /// <summary>
    /// Where
    /// </summary>
    public abstract class BaseWhere : ISearchCriteriaBuilder 
    {
        protected readonly List<ISearchCriteria> _searchCriterias = new();

        public IEnumerable<PropertyOptions> Columns { get; protected set; }

        public BaseWhere(IEnumerable<PropertyOptions> columns)
        {
            Columns = columns;
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
        public virtual IEnumerable<CriteriaDetail> BuildCriteria(IStatements statements)
        {
            return _searchCriterias.Select(x => x.GetCriteria(statements, Columns)).ToArray();
        }
    }

    public abstract class BaseWhere<T> : BaseWhere where T : class, new()
    {
        protected BaseWhere() : base(ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions)
        {
        }
    }
}
