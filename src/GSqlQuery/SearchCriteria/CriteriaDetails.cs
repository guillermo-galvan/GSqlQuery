namespace GSqlQuery.SearchCriteria
{
    internal readonly struct CriteriaDetails(string criterion, ParameterDetail[] parameters)
    {
        public ParameterDetail[] Parameters { get; } = parameters;

        public string Criterion { get; } = criterion;
    }
}
