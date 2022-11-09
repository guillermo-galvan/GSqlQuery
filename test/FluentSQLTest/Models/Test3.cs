namespace FluentSQLTest.Models
{
    [TableAttribute("TableName")]
    internal class Test3 : Entity<Test3>
    {
        [ColumnAttribute("Id", 20, true, true)]
        public int Ids { get; set; }

        [ColumnAttribute("Name", 20)]
        public string Names { get; set; }

        [ColumnAttribute("Create")]
        public DateTime Creates { get; set; }
        
        public bool IsTests { get; set; }

        public Test3()
        { }

        public Test3(int ids, string names, DateTime creates, bool isTests)
        {
            Ids = ids;
            Names = names;
            Creates = creates;
            IsTests = isTests;
        }
    }
}
