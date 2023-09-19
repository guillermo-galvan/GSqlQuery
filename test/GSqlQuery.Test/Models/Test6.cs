using System;

namespace GSqlQuery.Test.Models
{
    [Table("TableName")]
    internal class Test6 : Entity<Test6>
    {
        [Column("Id", Size = 20)]
        public int Ids { get; set; }

        [Column("Name", Size = 20)]
        public string Names { get; set; }

        [Column("Create")]
        public DateTime Creates { get; set; }

        public bool IsTests { get; set; }
    }
}