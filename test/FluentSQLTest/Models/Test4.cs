namespace FluentSQLTest.Models
{
    [TableAttribute("Scheme", "TableName")]
    internal class Test4
    {
        [ColumnAttribute("Id", 20, true, true)]
        public int Ids { get; set; }

        [ColumnAttribute("Name", 20)]
        public string Names { get; set; }

        [ColumnAttribute("Create")]
        public DateTime Creates { get; set; }

        public bool IsTests { get; set; }

        public Test4()
        { }

        public Test4(int ids, string names, DateTime creates)
        {
            Ids = ids;
            Names = names;
            Creates = creates;
        }
    }
}
