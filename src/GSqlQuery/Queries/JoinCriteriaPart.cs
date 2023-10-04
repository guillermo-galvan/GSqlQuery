using System.Reflection;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Join Criteria Part
    /// </summary>
    internal class JoinCriteriaPart
    {
        public ColumnAttribute Column { get; set; }

        public TableAttribute Table { get; set; }

        public MemberInfo MemberInfo { get; set; }
    }
}