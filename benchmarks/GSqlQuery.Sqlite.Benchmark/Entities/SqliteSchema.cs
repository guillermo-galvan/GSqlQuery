namespace GSqlQuery.Sqlite.Benchmark.Entities
{
    [Table("sqlite_schema")]
    public class SqliteSchema : EntityExecute<SqliteSchema>
    {
        [Column("type")]
        public string Type { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("tbl_name")]
        public string TblName { get; set; }

        public SqliteSchema()
        {

        }

        public SqliteSchema(string type, string name, string tblName)
        {
            Type = type;
            Name = name;
            TblName = tblName;
        }
    }
}
