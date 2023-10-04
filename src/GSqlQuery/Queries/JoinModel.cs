namespace GSqlQuery.Queries
{
    /// <summary>
    /// Join Model
    /// </summary>
    internal class JoinModel
    {
        public JoinCriteriaPart JoinModel1 { get; set; }

        public JoinCriteriaPart JoinModel2 { get; set; }

        public JoinCriteriaType JoinCriteria { get; set; }

        public string LogicalOperator { get; set; }
    }
}