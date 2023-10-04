namespace GSqlQuery.Extensions
{
    /// <summary>
    /// Generic class for internal use
    /// </summary>
    /// <typeparam name="T">The type of the MemberInfo property</typeparam>
    internal class ClassOptionsTupla<T>
    {
        /// <summary>
        /// Class to which MemberInfo belongs
        /// </summary>
        public ClassOptions ClassOptions { get; set; }

        /// <summary>
        /// MemberInfo 
        /// </summary>
        public T MemberInfo { get; set; }

        /// <summary>
        /// Generic class for internal use
        /// </summary>
        /// <param name="classOptions">Class to which MemberInfo belongs</param>
        /// <param name="memberInfo">MemberInfo</param>
        public ClassOptionsTupla(ClassOptions classOptions, T memberInfo)
        {
            ClassOptions = classOptions;
            MemberInfo = memberInfo;
        }
    }
}