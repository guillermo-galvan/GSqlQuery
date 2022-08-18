namespace FluentSQLTest.Models
{
    internal class Statements : IStatements
    {
        public string Format => "[{0}]";

        public string Select => "SELECT {0} FROM {1};";

        public string SelectWhere => "SELECT {0} FROM {1} WHERE {2};";

        public string Insert => "INSERT INTO {0} ({1}) VALUES ({2});";

        public string Update => "UPDATE {0} SET {1};";

        public string UpdateWhere => "UPDATE {0} SET {1} WHERE {2};";

        public string DeleteWhere => "DELETE FROM {0} WHERE {1};";
    }
}
