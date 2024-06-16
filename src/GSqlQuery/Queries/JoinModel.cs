namespace GSqlQuery.Queries
{
    /// <summary>
    /// Join Model
    /// </summary>
    internal class JoinModel
    {
        public JoinModel(string logicalOperador, JoinCriteriaPart joinCriteria1, JoinCriteriaType joinCriteriaEnum, JoinCriteriaPart joinCriteria2)
        {
            LogicalOperator = logicalOperador;
            JoinModel1 = joinCriteria1;
            JoinCriteria = joinCriteriaEnum;
            JoinModel2 = joinCriteria2;
        }

        public JoinCriteriaPart JoinModel1 { get; set; }

        public JoinCriteriaPart JoinModel2 { get; set; }

        public JoinCriteriaType JoinCriteria { get; set; }

        public string LogicalOperator { get; set; }
    }
}