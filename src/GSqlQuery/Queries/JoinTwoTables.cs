namespace GSqlQuery
{
    public class Join<T1, T2> where T1 : class, new() where T2 : class, new()
    {
        public T1 Table1 { get; set; }

        public T2 Table2 { get; set; }

        public Join()
        { }

        public Join(T1 table1, T2 table2)
        {
            Table1 = table1;
            Table2 = table2;
        }
    }
}
