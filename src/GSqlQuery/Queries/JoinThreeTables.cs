namespace GSqlQuery
{
    public class JoinThreeTables<T1, T2, T3> : JoinTwoTables<T1, T2>
        where T1 : class, new() where T2 : class, new() where T3 : class, new()
    {
        public T3 Table3 { get; set; }

        public JoinThreeTables()
        {

        }

        public JoinThreeTables(JoinTwoTables<T1, T2> joinTwo, T3 table3) : base(joinTwo.Table1, joinTwo.Table2)
        {
            Table3 = table3;
        }
    }
}
