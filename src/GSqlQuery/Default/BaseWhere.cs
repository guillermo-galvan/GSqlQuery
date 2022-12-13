using GSqlQuery.Extensions;
using GSqlQuery.Helpers;
using GSqlQuery.Models;
using GSqlQuery.SearchCriteria;

namespace GSqlQuery.Default
{
    /// <summary>
    /// Where
    /// </summary>
    public abstract class BaseWhere<T, TReturn> : ISearchCriteriaBuilder<T, TReturn> where TReturn : IQuery
    {
        protected readonly Queue<ISearchCriteria> _searchCriterias = new();

        public IEnumerable<PropertyOptions> Columns { get; protected set; }

        public BaseWhere()
        {
            Columns = ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions;
        }

        /// <summary>
        /// Add a search criteria
        /// </summary>
        /// <param name="criteria"></param>
        public void Add(ISearchCriteria criteria)
        {
            criteria.NullValidate(ErrorMessages.ParameterNotNull, nameof(criteria));
            _searchCriterias.Enqueue(criteria);
        }

        /// <summary>
        /// Build the criteria
        /// </summary>
        /// <returns>Criteria detail enumerable</returns>
        public virtual IEnumerable<CriteriaDetail> BuildCriteria(IStatements statements)
        {
            return _searchCriterias.Select(x => x.GetCriteria(statements, Columns)).ToArray();
        }

        public abstract TReturn Build();
    }
}
