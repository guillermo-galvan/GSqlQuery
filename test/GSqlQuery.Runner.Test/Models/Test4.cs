using System;

namespace GSqlQuery.Runner.Test.Models
{
    [Table("Scheme", "TableName")]
    internal class Test4
    {
        [Column("Id", Size = 20, IsAutoIncrementing = true, IsPrimaryKey = true)]
        public int Ids { get; set; }

        [Column("Name", Size = 20)]
        public string Names { get; set; }

        [Column("Create")]
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