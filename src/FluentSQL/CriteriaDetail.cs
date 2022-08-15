using FluentSQL.SearchCriteria;

namespace FluentSQL
{
    public class CriteriaDetail
    {
        public string Criterion { get; }

        public object ParameterValue { get; }

        public string ParameterName { get; }

        public ISearchCriteria SearchCriteria { get; }

        public CriteriaDetail(ISearchCriteria searchCriteria, string criterion, string parameterName, object parameterValue)
        {
            SearchCriteria = searchCriteria ?? throw new ArgumentNullException(nameof(searchCriteria));
            Criterion = criterion ?? throw new ArgumentNullException(nameof(criterion));
            ParameterValue = parameterValue;
            ParameterName = parameterName;
        }
    }
}
