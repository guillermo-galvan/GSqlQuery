using System.Collections.Generic;

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Join Criteria Part
    /// </summary>
    internal class JoinCriteriaPart
    {
        public JoinCriteriaPart(ClassOptions classOptions, KeyValuePair<string, PropertyOptions> keyValue)
        {
            ClassOptions = classOptions;
            KeyValue = keyValue;
        }

        public ClassOptions ClassOptions { get; set; }

        public KeyValuePair<string, PropertyOptions> KeyValue { get; set; }
    }
}