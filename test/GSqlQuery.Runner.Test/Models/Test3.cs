using System;

namespace GSqlQuery.Runner.Test.Models
{
    [Table("Test3")]
    internal class Test3 : EntityExecute<Test3>
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