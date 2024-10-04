namespace GSqlQuery.Queries
{
    /// <summary>
    /// Format for sentences
    /// </summary>
    internal class ConstFormat
    {
        internal const string SELECT = "SELECT {0} FROM {1};";

        internal const string SELECTWHERE = "SELECT {0} FROM {1} WHERE {2};";

        internal const string INSERT = "INSERT INTO {0} ({1}) VALUES ({2});";

        internal const string UPDATE = "UPDATE {0} SET {1};";

        internal const string UPDATEWHERE = "UPDATE {0} SET {1} WHERE {2};";

        internal const string DELETEWHERE = "DELETE FROM {0} WHERE {1};";

        internal const string DELETE = "DELETE FROM {0};";

        internal const string SELECTORDERBY = "SELECT {0} FROM {1} ORDER BY {2};";

        internal const string SELECTWHEREORDERBY = "SELECT {0} FROM {1} WHERE {2} ORDER BY {3};";

        internal const string JOIN = "JOIN {0} ON {1}";

        internal const string JOINSELECT = "SELECT {0} FROM {1} {2};";

        internal const string JOINSELECTWHERE = "SELECT {0} FROM {1} {2} WHERE {3};";

        internal const string JOINSELECTORDERBY = "{0} ORDER BY {1};";
    }
}