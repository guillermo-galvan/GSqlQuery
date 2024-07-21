using System.Collections.Generic;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Join Criteria Part
    /// </summary>
    internal class JoinCriteriaPart
    {
        public JoinCriteriaPart(ColumnAttribute columnAttribute, TableAttribute table, KeyValuePair<string, PropertyOptions> keyValue)
        {
            Column = columnAttribute;
            Table = table;
            KeyValue = keyValue;
        }

        public ColumnAttribute Column { get; set; }

        public TableAttribute Table { get; set; }

        public KeyValuePair<string, PropertyOptions> KeyValue { get; set; }
    }
}