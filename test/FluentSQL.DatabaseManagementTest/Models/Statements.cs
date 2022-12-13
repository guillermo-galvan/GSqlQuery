﻿namespace FluentSQL.DatabaseManagementTest.Models
{
    internal class Statements : IStatements
    {
        public string Format => "[{0}]";

        public string Select => "SELECT {0} FROM {1};";

        public string SelectWhere => "SELECT {0} FROM {1} WHERE {2};";

        public string Insert => "INSERT INTO {0} ({1}) VALUES ({2});";

        public string Update => "UPDATE {0} SET {1};";

        public string UpdateWhere => "UPDATE {0} SET {1} WHERE {2};";

        public string Delete => "DELETE FROM {0};";

        public string DeleteWhere => "DELETE FROM {0} WHERE {1};";

        public string ValueAutoIncrementingQuery => "SELECT SCOPE_IDENTITY();";

        public string SelectWhereOrderBy => "SELECT {0} FROM {1} WHERE {2} ORDER BY {3};";

        public string SelectOrderBy => "SELECT {0} FROM {1} ORDER BY {2};";

        public bool IncrudeTableNameInQuery => true;
    }
}