namespace FluentSQL
{
    /// <summary>
    /// Statements 
    /// </summary>
    public interface IStatements
    {
        /// <summary>
        /// Instructions to separate columns or table example "{0}"
        /// </summary>
        string Format { get; }

        /// <summary>
        /// Instructions to format the select, example "SELECT {0} FROM {1};"
        /// </summary>
        string Select { get; }

        /// <summary>
        /// Instructions to format the select with where, example "SELECT {0} FROM {1} WHERE {2};"
        /// </summary>
        string SelectWhere { get; }

        /// <summary>
        /// Instructions to format the insert, example "INSERT INTO {0} ({1}) VALUES ({2});"
        /// </summary>
        string Insert { get; }

        /// <summary>
        /// Instructions to format the update, example "UPDATE {0} SET {1};"
        /// </summary>
        string Update { get; }

        /// <summary>
        /// Instructions to format the update, example "UPDATE {0} SET {1} WHERE {2};"
        /// </summary>
        string UpdateWhere { get; }

        /// <summary>
        /// Instructions to format the delete, example "DELETE FROM {0};"
        /// </summary>
        string Delete { get; }

        /// <summary>
        /// Instructions to format the delete, example "DELETE FROM {0} WHERE {1};"
        /// </summary>
        string DeleteWhere { get; }

        /// <summary>
        /// 
        /// </summary>
        string ValueAutoIncrementingQuery { get; }

        /// <summary>
        /// Instructions to format the select, example "SELECT {0} FROM {1} WHERE {2} ORDER BY {3};"
        /// </summary>
        string SelectWhereOrderBy { get; }

        /// <summary>
        /// Instructions to format the select, example "SELECT {0} FROM {1} ORDER BY {2};"
        /// </summary>
        string SelectOrderBy { get; }

        bool IncrudeTableNameInQuery { get; }
    }
}
