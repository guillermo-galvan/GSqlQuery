namespace GSqlQuery.Test.Models
{
    [Table("TableName")]
    internal class Test3 : Entity<Test3>
    {
        [Column("Id", Size = 20, IsAutoIncrementing = true, IsPrimaryKey = true)]
        public int Ids { get; set; }

        [Column("Name", Size = 20)]
        public string Names { get; set; }

        [Column("Create")]
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
