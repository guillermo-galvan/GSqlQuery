using System.Collections.Generic;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Join Info
    /// </summary>
    internal class JoinInfo
    {
        public PropertyOptionsCollection Columns { get; set; }

        public bool IsMain { get; set; } = false;

        public Queue<JoinModel> Joins { get; set; } = new Queue<JoinModel>();

        public JoinType JoinEnum { get; set; }

        public ClassOptions ClassOptions { get; set; }

        public JoinInfo(PropertyOptionsCollection columns, ClassOptions classOptions, bool isMain)
        {
            Columns = columns;
            ClassOptions = classOptions;
            IsMain = isMain;
        }

        public JoinInfo(PropertyOptionsCollection columns, ClassOptions classOptions, JoinType joinType)
        {
            Columns = columns;
            ClassOptions = classOptions;
            JoinEnum = joinType;
        }
    }
}