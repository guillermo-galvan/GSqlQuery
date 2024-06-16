using System.Reflection;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Join Criteria Part
    /// </summary>
    internal class JoinCriteriaPart
    {
        public JoinCriteriaPart(ColumnAttribute columnAttribute, TableAttribute table, MemberInfo memberInfos)
        {
            Column = columnAttribute;
            Table = table;
            MemberInfo = memberInfos;
        }

        public ColumnAttribute Column { get; set; }

        public TableAttribute Table { get; set; }

        public MemberInfo MemberInfo { get; set; }
    }
}