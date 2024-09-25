using System.Collections.Generic;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Join Info
    /// </summary>
    internal class JoinInfo
    {
        public bool IsMain { get; set; } = false;

        public List<JoinModel> Joins { get; set; } = [];

        public JoinType JoinEnum { get; set; }

        public ClassOptions ClassOptions { get; set; }

        public DynamicQuery DynamicQuery { get; set; }

        public JoinInfo(ClassOptions classOptions, bool isMain)
        {
            ClassOptions = classOptions;
            IsMain = isMain;
        }

        public JoinInfo(DynamicQuery dynamicQuery, ClassOptions classOptions, bool isMain) : this(classOptions, isMain)
        {
            DynamicQuery = dynamicQuery;
        }

        public JoinInfo(ClassOptions classOptions, JoinType joinType)
        {
            ClassOptions = classOptions;
            JoinEnum = joinType;
        }

        public JoinInfo(DynamicQuery dynamicQuery, ClassOptions classOptions, JoinType joinType) : this(classOptions, joinType)
        {
            DynamicQuery = dynamicQuery;
        }
    }
}