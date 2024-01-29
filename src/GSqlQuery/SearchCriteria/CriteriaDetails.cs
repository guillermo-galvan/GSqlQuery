namespace GSqlQuery.SearchCriteria
{
    internal readonly struct CriteriaDetails(ParameterDetail[] parameters, string criterion)
    {
        public ParameterDetail[] Parameters { get; } = parameters;

        public string Criterion { get; } = criterion;
    }
}
