namespace GSqlQuery.Extensions
{
    internal class ClassOptionsTupla<T>
    {
        public ClassOptions ClassOptions { get; set; }

        public T MemberInfo { get; set; }

        public ClassOptionsTupla(ClassOptions classOptions, T memberInfo)
        {
            ClassOptions = classOptions;
            MemberInfo = memberInfo;
        }
    }
}
