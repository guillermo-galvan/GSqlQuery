using System.Collections.Generic;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Join Info
    /// </summary>
    internal class JoinInfo
    {
        public IEnumerable<PropertyOptions> Columns { get; set; }

        public bool IsMain { get; set; } = false;

        public Queue<JoinModel> Joins { get; set; } = new Queue<JoinModel>();

        public JoinType JoinEnum { get; set; }

        public ClassOptions ClassOptions { get; set; }
    }
}