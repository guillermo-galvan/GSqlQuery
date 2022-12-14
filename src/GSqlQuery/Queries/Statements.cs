namespace GSqlQuery
{
    public class Statements : IStatements
    {
        /// <summary>
        /// Instructions to separate columns or table example "{0}"
        /// </summary>
        public virtual string Format => "{0}";

        /// <summary>
        /// Instructions to format the select, example "SELECT {0} FROM {1};"
        /// </summary>
        public virtual string Select => "SELECT {0} FROM {1};";

        /// <summary>
        /// Instructions to format the select with where, example "SELECT {0} FROM {1} WHERE {2};"
        /// </summary>
        public virtual string SelectWhere => "SELECT {0} FROM {1} WHERE {2};";

        /// <summary>
        /// Instructions to format the insert, example "INSERT INTO {0} ({1}) VALUES ({2});"
        /// </summary>
        public virtual string Insert => "INSERT INTO {0} ({1}) VALUES ({2});";

        /// <summary>
        /// Instructions to format the update, example "UPDATE {0} SET {1};"
        /// </summary>
        public virtual string Update => "UPDATE {0} SET {1};";

        /// <summary>
        /// Instructions to format the update, example "UPDATE {0} SET {1} WHERE {2};"
        /// </summary>
        public virtual string UpdateWhere => "UPDATE {0} SET {1} WHERE {2};";

        /// <summary>
        /// Instructions to format the delete, example "DELETE FROM {0} WHERE {1};"
        /// </summary>
        public virtual string DeleteWhere => "DELETE FROM {0} WHERE {1};";

        /// <summary>
        /// Instructions to format the delete, example "DELETE FROM {0};"
        /// </summary>
        public virtual string Delete => "DELETE FROM {0};";

        /// <summary>
        /// 
        /// </summary>
        public virtual string ValueAutoIncrementingQuery => "";

        public virtual string SelectOrderBy => "SELECT {0} FROM {1} ORDER BY {2};";

        public virtual string SelectWhereOrderBy => "SELECT {0} FROM {1} WHERE {2} ORDER BY {3};";

        public virtual bool IncrudeTableNameInQuery => true;
    }
}
