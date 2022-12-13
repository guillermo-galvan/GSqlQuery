﻿namespace FluentSQL.DatabaseManagementTest.Models
{
    [Table("TableName")]
    internal class Test6 : DatabaseManagement.Entity<Test6>
    {
        [ColumnAttribute("Id", Size = 20)]
        public int Ids { get; set; }

        [ColumnAttribute("Name", Size = 20)]
        public string Names { get; set; }

        [Column("Create")]
        public DateTime Creates { get; set; }

        public bool IsTests { get; set; }

        public Test6()
        { }

        public Test6(int ids, string names, DateTime creates, bool isTests)
        {
            Ids = ids;
            Names = names;
            Creates = creates;
            IsTests = isTests;
        }
    }
}